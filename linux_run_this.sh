#!/bin/bash

mkdir -p addons cfg models sound scripts

echo "services:
  l4d2:
    image: deximy/l4d2panel
    container_name: l4d2
    ports:
      - 27015:27015
      - 27015:27015/udp
      - 8888:80
    volumes:
      - addons:/l4d2/left4dead2/addons
      - cfg:/l4d2/left4dead2/cfg
      - models:/l4d2/left4dead2/models
      - sound:/l4d2/left4dead2/sound
      - scripts:/l4d2/left4dead2/scripts
volumes:
  addons:
    driver: local
    driver_opts:
      type: none
      device: $(pwd)/addons
      o: bind
  cfg:
    driver: local
    driver_opts:
      type: none
      device: $(pwd)/cfg
      o: bind
  models:
    driver: local
    driver_opts:
      type: none
      device: $(pwd)/models
      o: bind
  sound:
    driver: local
    driver_opts:
      type: none
      device: $(pwd)/sound
      o: bind
  scripts:
    driver: local
    driver_opts:
      type: none
      device: $(pwd)/scripts
      o: bind
" > docker-compose.yml
