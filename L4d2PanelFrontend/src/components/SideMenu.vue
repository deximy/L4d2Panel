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
]

const is_server_running = ref(false);
const loading = ref(false);
watch(
    () => UseStore().is_server_running,
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
            @click="RunServer(); loading = true;"
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
