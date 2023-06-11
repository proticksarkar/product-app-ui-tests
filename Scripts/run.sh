#!/usr/bin/env sh

set -e
set -x

project="e2etests"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=2000

docker-compose -p "$project" build

docker-compose -p "$project" up

docker-compose -p "$project" up -d product_api product_webapp sql_db node-docker selenium-hub

docker-compose -p "$project" up --no-deps product_test

exit_code=$(docker inspect product_test -f '{{.State.ExitCode}}')

if [$exit_code -eq 0]; then
    exit $exit_code
else
    echo "Test failed"
fi