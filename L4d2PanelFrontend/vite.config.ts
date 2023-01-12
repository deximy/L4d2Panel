import {defineConfig} from "vite";
import vue from "@vitejs/plugin-vue";

export default defineConfig(
    {
        plugins: [
            vue()
        ],
        build: {
            outDir: "../L4d2PanelBackend/L4d2PanelBackend.API/wwwroot"
        }
    }
);
