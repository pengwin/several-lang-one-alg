.PHONY: all build wasm go-bin cpp-bin dotnet-bin js-bin
all: build

SHELL = /bin/bash

build: go-bin cpp-bin dotnet-bin js-bin
	
go-bin:
	make -C libs/go build

cpp-bin:
	make -C libs/cpp build

dotnet-bin:
	make -C libs/dotnet build

js-bin:
	make -C libs/js build

time-go-bin: go-bin
	time ./bin/go

time-cpp-bin: cpp-bin
	time ./bin/cpp

time-js-bin: js-bin
	time node ./bin/nodejs/index-cli.js

time-dotnet-bin: dotnet-bin
	time ./bin/dotnet/SquareSumsCli
