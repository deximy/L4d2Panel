import * as SignalR from "@microsoft/signalr";
import * as tus from "tus-js-client";

import {UseStore} from "./Store";

const RunServer = async (additional_params: string) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/processes";
    let uri = "/processes";
    await fetch(
        uri,
        {
            method: "POST",
            headers: new Headers(
                {
                    "Content-Type": "application/json"
                }
            ),
            body: `"${additional_params}"`,
        }
    );
};
const ShutdownServer = async () => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/processes";
    let uri = "/processes";
    await fetch(
        uri,
        {
            method: "PATCH",
        }
    );
};
const ExecCommand = async (command: string) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/processes/command";
    let uri = "/processes/command";

    let body = new FormData();
    body.append("command", command);
    await fetch(
        uri,
        {
            method: "POST",
            body: body,
        }
    );
};
const IsServerRunning = async (): Promise<boolean> => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/processes/";
    let uri = "/processes/";
    let id = await (
        await fetch(
            uri,
            {
                method: "GET"
            }
        )
    ).json();

    if (id.length == 0)
    {
        return false;
    }

    uri += id[0];
    let log = await (
        await fetch(
            uri,
            {
                method: "GET"
            }
        )
    ).json();

    return log.exit_time === null;
};
setInterval(
    () => {
        IsServerRunning().then(value => UseStore().is_server_running = value);
    },
    1000
)

const GetSignalRConnection = async () => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/hub";
    let uri = "/hub";
    return new SignalR.HubConnectionBuilder().withUrl(uri).build();
};

const GetFolderList = async (path: string) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/files/" + path;
    let uri = "/files/" + path;
    return await (await fetch(uri)).json();
};
const CreateDirectory = async (path: string) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/files/" + path;
    let uri = "/files/" + path;
    return await fetch(
        uri,
        {
            method: "POST"
        }
    );
};
const UploadFile = async (
    file: File,
    location: string,
    OnFinish: () => void,
    OnError: () => void
) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/upload";
    let uri = "/upload";
    let tus_client = new tus.Upload(
        file,
        {
            endpoint: uri,
            retryDelays: [0, 3000, 5000, 10000, 20000],
            metadata: {
                name: file.name,
                contentType: file.type || "application/octet-stream",
                emptyMetaKey: "",
                location: location,
            },
            onError: function(error) {
                console.log("Failed because: " + error)
                OnError();
            },
            onProgress: function(bytesUploaded, bytesTotal) {
                let percentage = (bytesUploaded / bytesTotal * 100).toFixed(2)
                console.log((tus_client.file as File).name, bytesUploaded, bytesTotal, percentage + "%")
            },
            onSuccess: function() {
                console.log("Succeed. Download %s from %s", (tus_client.file as File).name, tus_client.url);
                OnFinish();
            }
        }
    );
    tus_client.findPreviousUploads().then(function (previousUploads) {
        // Found previous uploads so we select the first one.
        if (previousUploads.length) {
            tus_client.resumeFromPreviousUpload(previousUploads[0])
        }

        // Start the upload
        tus_client.start()
    })

};
const Delete = async (path: string) => {
    const store = UseStore();
    // let uri = "http://" + store.endpoint + "/files/" + path;
    let uri = "/files/" + path;
    return await fetch(
        uri,
        {
            method: "DELETE"
        }
    );
};

export {RunServer, ShutdownServer, ExecCommand, GetSignalRConnection, IsServerRunning};
export {GetFolderList, CreateDirectory, UploadFile, Delete};
