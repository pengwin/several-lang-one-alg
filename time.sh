#!/bin/bash

echo 'Executing go'
time ./bin/go

echo 'Executing C++'
time ./bin/cpp

echo 'Executing .NET'
time ./bin/dotnet/SquareSumsCli

echo 'Executing Node.js'
time node ./bin/nodejs/index-cli.js
