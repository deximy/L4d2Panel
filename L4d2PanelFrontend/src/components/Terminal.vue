<script lang="ts" setup>
import {NSpin} from "naive-ui";

import {ref, onMounted, computed, nextTick, onUnmounted} from "vue";

import {Terminal} from "xterm";
import "xterm/css/xterm.css";
import "xterm/lib/xterm.js";
import {FitAddon} from "xterm-addon-fit";

import {GetSignalRConnection} from "../api";
import {UseStore} from "../Store";

const emits = defineEmits<
    {
        (e: "OnCommitCommand", command: string): void
    }
>();

const terminal_element =  ref<HTMLElement>();
let buffer = "";
let history_command = new Array<string>();

const terminal = new Terminal(
    {
        rendererType: "canvas",
        convertEol: true,
        disableStdin: false,
        cursorBlink: true,
        scrollback: 0,
    }
);
const fit_addon = new FitAddon();
terminal.loadAddon(fit_addon);
terminal.onKey(
    (key_event) => {
        const dom_event = key_event.domEvent;
        const printable = !dom_event.altKey && !dom_event.ctrlKey && !dom_event.metaKey;
        console.log(dom_event.code, terminal.buffer);
        if (dom_event.code === "Enter" || dom_event.code === "NumpadEnter")
        {
            if (buffer.replace(/^\s+|\s+$/g, '').length != 0)
            {
                emits("OnCommitCommand", buffer);
                history_command.push(buffer);
                for (let i of buffer)
                {
                    terminal.write("\b \b");
                }
                buffer = "";
            }
        }
        else if (dom_event.code === "Backspace")
        {
            terminal.write("\b \b");
            buffer = buffer.slice(0, buffer.length - 1);
        }
        else if (dom_event.code === "ArrowLeft")
        {
            terminal.write(key_event.key);
        }
        else if (dom_event.code === "ArrowRight")
        {
            if (terminal.buffer.active.cursorX < buffer.length)
            {
                terminal.write(key_event.key);
            }
        }
        else if (printable)
        {
            terminal.write(key_event.key);
            buffer += key_event.key;
        }
    }
);
// terminal.onData(
//     (data) => {
//         if (data.length > 1)
//         {
//             terminal.write(data);
//         }
//     }
// );

GetSignalRConnection().then(
    (connection) => {
        connection
            .on(
                "ReceiveMessage",
                message => {
                    terminal.writeln(message);
                }
            );
        connection
            ?.start()
            .then(
                () => {

                }
            )
            .catch(
                (e) => {
                    console.log(e);
                }
            );
    }
);

let observer = new ResizeObserver(
    () => {
        nextTick().then(
            () => {
                fit_addon.fit();
            }
        );
    }
);

onMounted(
    () => {
        if (terminal_element.value === undefined)
        {
            throw new Error("Cannot find terminal element!");
        }

        terminal.open(terminal_element.value);

        // setInterval(
        //     () => {
        //         fit_addon.fit();
        //         // (terminal as any)._core.viewport._refresh();
        //     },
        //     500
        // );

        observer.observe(terminal_element.value as Element);
    }
);
onUnmounted(
    () => {
        observer.disconnect();
    }
);

const is_server_running = computed(() => UseStore().is_server_running);
</script>

<template>
    <n-spin class="loading" :show="!is_server_running" stroke="#66CCFF">
        <div class="terminal" ref="terminal_element"/>
    </n-spin>

</template>

<style lang="scss" scoped>
.terminal
{
    height: 100%;
    width: 100%;
}

.loading
{
    height: 100%;
    width: 100%;

}

.mask
{
    height: 100%;
    width: 100%;
    position: fixed;
    z-index: 100;
    background: rgba(255, 255, 255, 0.7);
}
</style>
<style>
.n-spin-content
{
    height: 100%;
}
.xterm-viewport
{
    width: initial !important;
}
</style>
