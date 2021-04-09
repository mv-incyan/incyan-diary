#! /bin/bash

SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" > /dev/null && pwd )

DOCKER_COMPOSE_FILE=docker-compose.yml

echo "${SCRIPT_DIR}/${DOCKER_COMPOSE_FILE}";

docker-compose -f "${SCRIPT_DIR}/${DOCKER_COMPOSE_FILE}" up --build --force
