#!/bin/bash

cd WebApi
docker build -f Dockerfile -t webapi .

cd ../LoadGenerator
docker build -f Dockerfile -t loadgenerator .