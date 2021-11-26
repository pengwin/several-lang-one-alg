.PHONY: all build wasm go-bin cpp-bin dotnet-bin js-bin
all: build

SHELL = /bin/bash
TIME=/usr/bin/time --verbose # use gnu-time, not bash builtin time

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
	$(TIME) ./bin/go

time-cpp-bin: cpp-bin
	$(TIME) ./bin/cpp

time-js-bin: js-bin
	$(TIME) node ./bin/nodejs/index-cli.js

time-dotnet-bin: dotnet-bin
	$(TIME) ./bin/dotnet/SquareSumsCli
