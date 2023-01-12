<script lang="ts" setup>
import {NPopselect} from "naive-ui";
import {NIcon} from "naive-ui/es";
import {useMessage as UseMessage} from "naive-ui/es";

import {computed, ref} from "vue";

const current_edition = ref("professional");
const options = [
    {
        label: "标准版",
        value: "standard",
        disabled: true
    },
    {
        label: "高级版",
        value: "premium",
        disabled: true
    },
    {
        label: "专业版",
        value: "professional"
    }
];
const OnUpdateEdition = (
    value: string | number | Array<string | number> | null
) => {
    if (value !== "professional")
    {
        message_box.error("别看了不支持这个");
        return;
    }

    current_edition.value = value;
};

const current_edition_label = computed(
    () => {
        for (let i of options)
        {
            if (i.value === current_edition.value)
            {
                return i.label;
            }
        }
    }
);

const message_box = UseMessage();
</script>

<template>
    <div class="switch_edition">
        <n-popselect
            v-model:value="current_edition"
            :options="options"
            size="small"
            @update:value="OnUpdateEdition"
        >
            <div class="switcher">
                <span>{{ current_edition_label }}</span>
                <n-icon>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        xmlns:xlink="http://www.w3.org/1999/xlink"
                        viewBox="0 0 320 512"
                    >
                        <path d="M143 352.3L7 216.3c-9.4-9.4-9.4-24.6 0-33.9l22.6-22.6c9.4-9.4 24.6-9.4 33.9 0l96.4 96.4l96.4-96.4c9.4-9.4 24.6-9.4 33.9 0l22.6 22.6c9.4 9.4 9.4 24.6 0 33.9l-136 136c-9.2 9.4-24.4 9.4-33.8 0z" fill="currentColor"></path>
                    </svg>
                </n-icon>
            </div>
        </n-popselect>
    </div>
</template>

<style scoped>
.switch_edition
{
    display: flex;
    align-items: center;
}
.switcher
{
    display: flex;
    align-items: center;

}
</style>
