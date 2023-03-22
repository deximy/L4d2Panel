#!/bin/sh

read -p "请输入安装目录（默认为当前目录）: " install_dir
if [ -z "$install_dir" ]; then
  install_dir="$(pwd)"
fi

if [ ! -d "$install_dir" ]; then
  read -p "$install_dir 不存在，是否创建？[Y/n] " create_dir
  if [ "$create_dir" == "Y" ] || [ "$create_dir" == "y" ] || [ -z "$create_dir" ]; then
    mkdir -p "$install_dir"
  else
    echo "安装目录不存在，退出安装程序。"
    exit 1
  fi
else
  if [ "$(ls -A "$install_dir")" ]; then
    read -p "$install_dir 不为空，是否继续？[Y/n] " continue_install
    if [ "$continue_install" != "Y" ] && [ "$continue_install" != "y" ] && [ -n "$continue_install" ]; then
      echo "退出安装程序。"
      exit 1
    fi
  fi
fi

echo "安装目录为：$install_dir"
mkdir -p "$install_dir/addons" "$install_dir/cfg" "$install_dir/models" "$install_dir/sound" "$install_dir/scripts"

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
      device: $install_dir/addons
      o: bind
  cfg:
    driver: local
    driver_opts:
      type: none
      device: $install_dir/cfg
      o: bind
  models:
    driver: local
    driver_opts:
      type: none
      device: $install_dir/models
      o: bind
  sound:
    driver: local
    driver_opts:
      type: none
      device: $install_dir/sound
      o: bind
  scripts:
    driver: local
    driver_opts:
      type: none
      device: $install_dir/scripts
      o: bind
" > docker-compose.yml
