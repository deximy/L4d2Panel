#!/bin/sh
set -e

if_docker_exist() {
  if !command -v docker >/dev/null 2>&1; then
    return 1
  fi
  return 0
}

install_docker() {
  curl -fsSL https://get.docker.com -o get-docker.sh
  sudo sh get-docker.sh
  rm get-docker.sh
}

print_docker_info() {
  echo "当前 docker 版本为：$(docker --version | awk '{print $3}' | tr -d ',') docker-compose 版本为：$(docker compose version | awk '{print $4}' | tr -d ',')"
}

get_install_dir() {
  read install_dir
  if [ -z "$install_dir" ]; then
    install_dir="$(pwd)"
  fi
  echo "$install_dir"
}

prepare_install_dir() {
  if [ ! -d "$1" ]; then
    read -p "$1 不存在，是否创建？[Y/n] " create_dir
    if [ "$create_dir" = "Y" ] || [ "$create_dir" = "y" ] || [ -z "$create_dir" ]; then
      mkdir -p "$1"
    else
      echo "安装目录不存在，退出安装程序。"
      exit 1
    fi
  else
    if [ "$(ls -A "$1")" ]; then
      read -p "$1 不为空，是否继续？[Y/n] " continue_install
      if [ "$continue_install" != "Y" ] && [ "$continue_install" != "y" ] && [ -n "$continue_install" ]; then
        echo "退出安装程序。"
        exit 1
      fi
    fi
  fi

  mkdir -p "$1/addons" "$1/cfg" "$1/models" "$1/sound" "$1/scripts"
}

generate_config_file() {
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
        device: $1/addons
        o: bind
    cfg:
      driver: local
      driver_opts:
        type: none
        device: $1/cfg
        o: bind
    models:
      driver: local
      driver_opts:
        type: none
        device: $1/models
        o: bind
    sound:
      driver: local
      driver_opts:
        type: none
        device: $1/sound
        o: bind
    scripts:
      driver: local
      driver_opts:
        type: none
        device: $1/scripts
        o: bind
  " > $1/docker-compose.yml

}

install_l4d2panel() {
  echo "正在检测是否已安装基础组件: docker"
  if ! if_docker_exist; then
    echo "未安装基础组件 docker，正在安装……"
    install_docker
    echo "基础组件 docker 安装完成。"
  else
    echo "基础组件 docker 已安装。"
  fi
  
  print_docker_info
  
  echo -n "请输入安装目录(默认为当前目录): "
  install_dir=$(get_install_dir)
  echo "安装目录为：$install_dir"

  prepare_install_dir $install_dir

  echo "正在生成配置文件……"
  generate_config_file $install_dir
  echo "配置文件生成完成。"

}

echo "开始安装……"
install_l4d2panel
echo "安装完成。"
