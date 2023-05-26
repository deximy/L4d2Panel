<script setup lang="ts">
import {NInputGroup} from "naive-ui";
import {NInputGroupLabel} from "naive-ui";
import {NInput} from "naive-ui";

import {UseStore} from "../Store";
const store = UseStore();
</script>

<template>
    <div class="setup-params">
        <n-input-group style="align-items: center;">
            <span style="white-space: nowrap;">请输入额外参数：</span>
            <n-input-group-label style="white-space: nowrap;">
                +map c2m1 -ip 0.0.0.0 -port 27015
            </n-input-group-label>
            <n-input v-model:value="store.additional_params"/>
        </n-input-group>
    </div>
</template>

<style scoped>
.setup-params {
    width: 90%;
    margin-left: 5%;
    margin-right: 5%;
    margin-top: 5%;
}
</style>
