# L4d2PanelBackend

## Getting started

### Installing

#### Building from source code

```sh
git clone https://github.com/deximy/L4d2PanelBackend.git
cd L4d2PanelBackend
docker build -t l4d2panel -f L4d2PanelBackend.API/Dockerfile .
```

#### ......OR installing from Docker Hub

```sh
docker pull deximy/l4d2panel
```

### Run container

*Just a sample. Any docker command is available.*

```sh
docker run -td -p <panel-port>:80 -p <game-port>:27015 -p <game-port>:27015/udp l4d2panel -f /dev/null
```

+ panel-port: The port used to access **the PANEL**. Frontend will require this port for access API. You can use any port you like.
+ game-port: The port used to access **the GAME**. When you join game you will need this port. The default value is 27015.
