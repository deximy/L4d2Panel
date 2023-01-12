import {defineStore as DefineStore} from "pinia";
import {ref} from "vue";

export const UseStore = DefineStore(
    "store",
    () => {
        const endpoint = ref("");
        const component = ref("");
        const is_server_running = ref(false);
        const selected_file_name = ref("");
        const selected_file_type = ref("");
        const current_path = ref("./");
        return {
            endpoint,
            component,
            is_server_running,
            selected_file_name,
            selected_file_type,
            current_path
        }
    }
);

