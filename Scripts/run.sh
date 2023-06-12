#!/usr/bin/env sh

set -e
set -x

project="e2etests"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker-compose -p "$project" build

mkdir -m 777 reports

docker-compose -p "$project" up -d product_api product_webapp sql_db chrome edge firefox chrome_video edge_video firefox_video selenium-hub

docker-compose -p "$project" up --no-deps product_test

docker cp producttest:/src/ProductUIAutomationBDDTests/LivingDoc.html ./reports
echo "SpecFlow Living Doc Report is copied to ./reports"
ls -l ./reports

exit_code=$(docker inspect producttest --format='{{.State.ExitCode}}')

if [ $exit_code = 0 ]; then 
    exit $exit_code
else
    echo "Test failed"
fi