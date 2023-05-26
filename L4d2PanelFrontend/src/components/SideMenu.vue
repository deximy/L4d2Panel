<script lang="ts" setup>
import {NMenu} from "naive-ui";
import {NIcon} from "naive-ui/es";
import {NButton} from "naive-ui/es";
import {NDivider} from "naive-ui/es";

import {h, ref, watch} from "vue";
import {useRoute as UseRoute, useRouter as UseRouter} from "vue-router";

import {RunServer, ShutdownServer} from "../api";
import {UseStore} from "../Store";

const router = UseRouter();
const route = UseRoute();
const store = UseStore();

const HandleRunServer = () => {
    loading.value = true;
    RunServer("+map c2m1 -ip 0.0.0.0 -port 27015 " + store.additional_params);
};

const current_option = ref(route.name?.toString());
watch(
    current_option,
    value => {
        let target_uri = "";
        if (route.params.endpoint !== "")
        {
            target_uri += `/${route.params.endpoint}`;
        }
        target_uri += `/${String(value)}`;
        router.push(target_uri);
    }
);
const options = [
    {
        key: "file",
        label: "文件管理",
        icon: () => {
            return h(
                NIcon,
                null,
                {
                    default: () => {
                        return [
                            h(
                                "svg",
                                {
                                    "xmlns": "http://www.w3.org/2000/svg",
                                    "xmlns:xlink": "http://www.w3.org/1999/xlink",
                                    "viewBox": "0 0 32 32",
                                },
                                h(
                                    "path",
                                    {
                                        "d": "M11.17 6l3.42 3.41l.58.59H28v16H4V6h7.17m0-2H4a2 2 0 0 0-2 2v20a2 2 0 0 0 2 2h24a2 2 0 0 0 2-2V10a2 2 0 0 0-2-2H16l-3.41-3.41A2 2 0 0 0 11.17 4z",
                                        "fill": "currentColor",
                                    }
                                )
                            )
                        ]
                    }
                }
            )
        }
    },
    // {
    //     key: "addon",
    //     label: "地图",
    //     children: [
    //         {
    //             key: "upload_addon",
    //             label: "上传地图",
    //         },
    //         {
    //             key: "view_addon",
    //             label: "查看地图",
    //         },
    //     ]
    // },
    {
        key: "terminal",
        label: "控制台",
        icon: () => {
            return h(
                NIcon,
                null,
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
                                    "g",
                                    {
                                        "fill": "none",
                                        "stroke": "currentColor",
                                        "stroke-width": "2",
                                        "stroke-linecap": "round",
                                        "stroke-linejoin": "round",
                                    },
                                    [
                                        h(
                                            "path",
                                            {
                                                "d": "M5 7l5 5l-5 5"
                                            }
                                        ),
                                        h(
                                            "path",
                                            {
                                                "d": "M12 19h7"
                                            }
                                        )
                                    ]
                                )
                            )
                        ]
                    }
                }
            );
        }
    },
    {
        key: "setup",
        label: "设置启动参数",
        icon: () => {
            return h(
                NIcon,
                null,
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
                                    "g",
                                    {
                                        "fill": "none",
                                    },
                                    [
                                        h(
                                            "path",
                                            {
                                                "fill": "currentColor",
                                                "d": "M12.013 2.25c.734.008 1.465.093 2.181.253a.75.75 0 0 1 .582.649l.17 1.527a1.384 1.384 0 0 0 1.928 1.116l1.4-.615a.75.75 0 0 1 .85.174a9.793 9.793 0 0 1 2.204 3.792a.75.75 0 0 1-.271.825l-1.242.916a1.38 1.38 0 0 0 .001 2.226l1.243.915a.75.75 0 0 1 .271.826a9.798 9.798 0 0 1-2.203 3.792a.75.75 0 0 1-.849.175l-1.406-.617a1.38 1.38 0 0 0-1.927 1.114l-.169 1.526a.75.75 0 0 1-.572.647a9.518 9.518 0 0 1-4.405 0a.75.75 0 0 1-.572-.647l-.17-1.524a1.382 1.382 0 0 0-1.924-1.11l-1.407.616a.75.75 0 0 1-.849-.175a9.798 9.798 0 0 1-2.203-3.796a.75.75 0 0 1 .271-.826l1.244-.916a1.38 1.38 0 0 0 0-2.226l-1.243-.914a.75.75 0 0 1-.272-.826a9.793 9.793 0 0 1 2.205-3.792a.75.75 0 0 1 .849-.174l1.4.615a1.387 1.387 0 0 0 1.93-1.118l.17-1.526a.75.75 0 0 1 .583-.65c.718-.159 1.45-.243 2.202-.252zm0 1.5a9.135 9.135 0 0 0-1.355.117l-.109.977A2.886 2.886 0 0 1 6.525 7.17l-.898-.394a8.293 8.293 0 0 0-1.348 2.317l.798.587a2.881 2.881 0 0 1 0 4.643l-.798.588c.32.842.775 1.626 1.347 2.322l.906-.397a2.882 2.882 0 0 1 4.017 2.318l.108.984c.89.15 1.799.15 2.689 0l.108-.984a2.88 2.88 0 0 1 4.02-2.322l.904.396a8.299 8.299 0 0 0 1.347-2.318l-.798-.588a2.88 2.88 0 0 1 0-4.643l.796-.587a8.293 8.293 0 0 0-1.348-2.317l-.896.393a2.884 2.884 0 0 1-4.023-2.324l-.11-.976a8.99 8.99 0 0 0-1.333-.117zM12 8.25a3.75 3.75 0 1 1 0 7.5a3.75 3.75 0 0 1 0-7.5zm0 1.5a2.25 2.25 0 1 0 0 4.5a2.25 2.25 0 0 0 0-4.5z"
                                            }
                                        ),
                                    ]
                                )
                            )
                        ]
                    }
                }
            );
        }
    },
]

const is_server_running = ref(false);
const loading = ref(false);
watch(
    () => store.is_server_running,
    value => {
        is_server_running.value = value;
        loading.value = false;
    }
);
</script>

<template>
    <div class="side_menu">
        <n-button
            class="control_button"
            strong
            secondary
            type="primary"
            v-show="!is_server_running"
            :loading="loading"
            @click="HandleRunServer"
        >
            启动
        </n-button>
        <n-button
            class="control_button"
            strong
            secondary
            type="error"
            v-show="is_server_running"
            :loading="loading"
            @click="ShutdownServer(); loading = true;"
        >
            停止
        </n-button>
        <n-divider />
        <n-menu
            :options="options"
            v-model:value="current_option"
        />
    </div>

</template>

<style scoped>
.side_menu
{
    display: flex;
    flex-direction: column;
}

.control_button
{
    width: 150px;
    margin: 24px auto 0;
}
</style>
