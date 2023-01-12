<script lang="ts" setup>
import {NUpload, NUploadTrigger, UploadCustomRequestOptions} from "naive-ui/es";
import {NDataTable, DataTableColumn} from "naive-ui/es";
import {NIcon} from "naive-ui/es";
import {NDropdown, DropdownOption} from "naive-ui/es";
import {NModal} from "naive-ui/es";
import {NInput} from "naive-ui/es";

import {h, nextTick, ref, watch} from "vue";
import {storeToRefs as StoreToRefs} from "pinia";

import {CreateDirectory, Delete, GetFolderList, UploadFile} from "../api";
import {UseStore} from "../Store";
import * as p from "path-browserify";

interface RowData
{
    key: string | number,
    file_type: string,
    file_name: string,
}

const store = UseStore();

const columns: Array<DataTableColumn<RowData>> = [
    {
        key: "file_type",
        fixed: "left",
        width: "0",
        className: "file_type_icon",
        render: (row: RowData) => {
            if (row.file_type === "directory")
            {
                return h(
                    "div",
                    {
                        class: "file_type_icon_wrapper",
                    },
                    h(
                        NIcon,
                        {

                        },
                        {
                            default: () => {
                                return [
                                    h(
                                        "svg",
                                        {
                                            "xmlns": "http://www.w3.org/2000/svg",
                                            "xmlns:xlink": "http://www.w3.org/1999/xlink",
                                            "viewBox": "0 0 24 24",
                                        },
                                        h(
                                            "path",
                                            {
                                                "d": "M10.59 4.59C10.21 4.21 9.7 4 9.17 4H4c-1.1 0-1.99.9-1.99 2L2 18c0 1.1.9 2 2 2h16c1.1 0 2-.9 2-2V8c0-1.1-.9-2-2-2h-8l-1.41-1.41z",
                                                "fill": "currentColor",
                                            }
                                        )
                                    )
                                ]
                            }
                        }
                    )
                );
            }
            else
            {
                return h(
                    "div",
                    {
                        class: "file_type_icon_wrapper",
                    },
                    h(
                        NIcon,
                        {

                        },
                        {
                            default: () => {
                                return [
                                    h(
                                        "svg",
                                        {
                                            "xmlns": "http://www.w3.org/2000/svg",
                                            "xmlns:xlink": "http://www.w3.org/1999/xlink",
                                            "viewBox": "0 0 24 24",
                                        },
                                        h(
                                            "path",
                                            {
                                                "d": "M6 2c-1.1 0-1.99.9-1.99 2L4 20c0 1.1.89 2 1.99 2H18c1.1 0 2-.9 2-2V8l-6-6H6zm7 7V3.5L18.5 9H13z",
                                                "fill": "currentColor",
                                            }
                                        )
                                    )
                                ]
                            }
                        }
                    )
                );
            }
        }
    },
    {
        title: "文件名",
        key: "file_name",
        className: "file_name",
    }
];
const data = ref<Array<object>>([]);
const row_props = (row: RowData) => {
    return {
        onDblclick: (_: MouseEvent) => {
            if (row.file_type === "directory")
            {
                EnterDirectory(row.file_name);
            }
            else if (row.file_type === "file")
            {
                OpenFile(row.file_name);
            }
            else
            {
                console.error("Unknown file type!");
            }
        },
        onContextmenu: (e: MouseEvent) => {
            e.preventDefault();
            show_dropdown.value = false;
            store.selected_file_type = row.file_type;
            store.selected_file_name = row.file_name;
            x.value = e.clientX;
            y.value = e.clientY;
            nextTick().then(
                () => {
                    show_dropdown.value = true;
                }
            );
        },
    };
};

let {current_path} = StoreToRefs(store);
const RefreshDirectory = async (path: string) => {
    let folder_content = await GetFolderList(path);
    if (folder_content === undefined)
    {
        console.log("Empty response!");
        return;
    }

    data.value.length = 0;
    data.value.push(
        {
            key: "directory_" + "..",
            file_type: "directory",
            file_name: "..",
        }
    );
    for (let i of folder_content.directories)
    {
        data.value.push(
            {
                key: "directory_" + i,
                file_type: "directory",
                file_name: i,
            }
        );
    }
    for (let i of folder_content.files)
    {
        data.value.push(
            {
                key: "file_" + i,
                file_type: "file",
                file_name: i,
            }
        );
    }
};
const EnterDirectory = async (path: string) => {
    current_path.value = p.join(current_path.value, path);
    if (current_path.value.startsWith(".."))
    {
        current_path.value = "./";
    }
};
const OpenFile = async (path: string) => {
    console.log("Try to open file: " + path);
};
const Mkdir = () => {
    show_modal.value = true;
};
const DeleteDirectory = async (directory_name: string) => {
    await Delete(p.join(current_path.value, directory_name));
    await RefreshDirectory(current_path.value);
};
const DeleteFile = async (file_name: string) => {
    await Delete(p.join(current_path.value, file_name));
    await RefreshDirectory(current_path.value);
};

watch(current_path, value => {RefreshDirectory(value)});
RefreshDirectory(current_path.value);

const x = ref(0);
const y = ref(0);
const options: DropdownOption[] = [
    {
        label: "刷新",
        key: "refresh",
    },
    {
        label: "新建文件夹",
        key: "mkdir",
    },
    {
        label: "上传文件",
        key: "upload_file",
    },
    {
        label: "上传文件夹",
        key: "upload_directory",
    },
    {
        label: () => h("span", { style: { color: "red" } }, "删除"),
        key: "delete",
    }
];
const show_dropdown = ref(false);
const HandleClickOutSideOfDropdown = () => {
    show_dropdown.value = false;
};
const HandleSelect = (BeginUpload: () => void, key: string) => {
    show_dropdown.value = false;

    if (key === "refresh")
    {
        RefreshDirectory(current_path.value);
    }
    else if (key === "mkdir")
    {
        Mkdir();
    }
    else if (key === "upload_file")
    {
        let list = document.getElementsByClassName("n-upload-file-input");
        if (list.length == 0)
        {
            throw new RangeError("Couldn't find file input!");
        }
        list[0].removeAttribute("webkitdirectory");
        BeginUpload();
    }
    else if (key === "upload_directory")
    {
        let list = document.getElementsByClassName("n-upload-file-input");
        if (list.length == 0)
        {
            throw new RangeError("Couldn't find file input!");
        }
        list[0].setAttribute("webkitdirectory", "webkitdirectory");
        BeginUpload();
    }
    else if (key === "delete")
    {
        if (store.selected_file_type === "directory")
        {
            DeleteDirectory(store.selected_file_name);
        }
        else if (store.selected_file_type === "file")
        {
            DeleteFile(store.selected_file_name);
        }
        else
        {
            console.error("Unknown file type:" + store.selected_file_type);
        }
    }
    else
    {
        console.error("Unknown key: " + key);
    }
};

const CustomUploadFile = (
    {
        file: file_info,
        onFinish,
        onError,
    }: UploadCustomRequestOptions
) => {
    let file = file_info.file as File;
    if (file.webkitRelativePath != "" && file.webkitRelativePath != undefined)
    {
        // upload directory
        let target_path = file.webkitRelativePath.substring(0, file.webkitRelativePath.lastIndexOf('/'));
        let full_path = p.join(current_path.value, target_path);
        CreateDirectory(full_path)
            .then(
                () => {
                    UploadFile(file, full_path, onFinish, onError).then(() => RefreshDirectory(current_path.value));
                }
            );
    }
    else
    {
        // upload file(s)
        UploadFile(file, current_path.value, onFinish, onError).then(() => RefreshDirectory(current_path.value));
    }
};

const show_modal = ref(false);
const HandleMkdirConfirm = () => {
    if (new_folder_name.value == null)
    {
        return;
    }
    CreateDirectory(
        p.join(current_path.value, new_folder_name.value)
    ).then(
        () => RefreshDirectory(current_path.value)
    );
    new_folder_name.value = null;
};
const new_folder_name = ref<string | null>(null);
</script>

<template>
    <div class="file_manager">
        <n-data-table
            class="file_list"
            :bordered="false"
            :columns="columns"
            :data="data"
            :row-props="row_props"
        />
        <n-upload abstract :multiple="true" :custom-request="CustomUploadFile">
            <n-upload-trigger abstract #="{handleClick: HandleClick}">
                <n-dropdown
                    placement="bottom-start"
                    trigger="manual"
                    :x="x"
                    :y="y"
                    :options="options"
                    :show="show_dropdown"
                    @clickoutside="HandleClickOutSideOfDropdown"
                    @select="HandleSelect(HandleClick, $event)"
                />
            </n-upload-trigger>
        </n-upload>
        <n-modal
            v-model:show="show_modal"
            preset="dialog"
            title="新建文件夹"
            :show-icon="false"
            negative-text="取消"
            positive-text="确认"
            :on-positive-click="HandleMkdirConfirm"
        >
            <n-input
                v-model:value="new_folder_name"
                type="text"
                placeholder="文件夹名"
            />
        </n-modal>
    </div>
</template>

<style scoped>
.file_manager
{
    height: 100%;
}
.file_list
{
    height: 100%;
}
:deep(.file_type_icon)
{
    padding-right: 0;
}
/*:deep(.file_name)*/
/*{*/
/*    padding-left: 0;*/
/*}*/
:deep(.file_type_icon_wrapper)
{
    display: flex;
}
</style>
