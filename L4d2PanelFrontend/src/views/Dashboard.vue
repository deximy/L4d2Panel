<script lang="ts" setup>
import {NLayout, NLayoutHeader, NLayoutContent, NLayoutFooter, NLayoutSider} from "naive-ui/es";
import {NMessageProvider} from "naive-ui/es";

import Logo from "../components/Logo.vue";
import SwitchEdition from "../components/SwitchEdition.vue";
import SideMenu from "../components/SideMenu.vue";

import {ExecCommand} from "../api";
</script>

<template>
    <n-layout class="dashboard" content-style="display: flex; flex-direction: column;">
        <n-layout-header class="header" :bordered="true">
            <logo />
            <n-message-provider>
                <switch-edition />
            </n-message-provider>
        </n-layout-header>
        <n-layout class="content" :has-sider="true">
            <n-layout-sider>
                <side-menu />
            </n-layout-sider>
            <n-layout-content>
                <router-view @OnCommitCommand="ExecCommand"/>
            </n-layout-content>
        </n-layout>
        <n-layout-footer class="footer">
            <span style="color: #66CCFF">这里好像应该写点什么，但我懒得想了。</span>
        </n-layout-footer>
    </n-layout>
</template>

<style scoped>
.dashboard
{
    height: 100%;
}

.header
{
    height: 64px;
    display: flex;
    justify-content: space-between;
    padding: 0 30px;
}

.footer
{
    display: flex;
    justify-content: center;
}
</style>
