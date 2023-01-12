import {createApp as CreateApp} from "vue";
import {createRouter as CreateRouter, RouteLocationNormalized} from "vue-router";
import {createWebHashHistory as CreateWebHashHistory} from "vue-router";
import {createPinia as CreatePinia} from "pinia";

import App from "./App.vue";
import Dashboard from "./views/Dashboard.vue";
import Terminal from "./components/Terminal.vue";
import FileManager from "./components/FileManager.vue";

import {UseStore} from "./Store";

const routes = [
    {
        path: "/:endpoint?",
        component: Dashboard,
        beforeEnter: (to: RouteLocationNormalized) => {
            if (typeof to.params.endpoint !== "string")
            {
                console.error(to.params.endpoint);
                throw new Error("Unable to resolve endpoint.");
            }
            store.endpoint = to.params.endpoint;
        },
        children: [
            {
                path: "terminal",
                name: "terminal",
                component: Terminal,
                beforeEnter: () => {
                    store.component = "terminal";
                },
            },
            {
                path: "file",
                name: "file",
                component: FileManager,
                beforeEnter: () => {
                    store.component = "file";
                },
            },
            {
                path: ":path(.*)*",
                redirect: {
                    name: "terminal"
                },
            }
        ],
    },
    /*{
        path: "/terminal",
        name: "terminal",
        component: Terminal,
        beforeEnter: () => {
            store.component = "terminal";
        }
    },
    {
        path: "/file",
        name: "file",
        component: FileManager,
        beforeEnter: () => {
            store.component = "file";
        },
    },
    {
        path: "/:path(.*)*",
        redirect: {
            name: "terminal"
        },
    }*/
];
const router = CreateRouter(
    {
        history: CreateWebHashHistory(),
        routes: routes
    }
);

const pinia = CreatePinia();

const app = CreateApp(App);
app.use(router);
app.use(pinia);
app.mount('#app');

const store = UseStore();
