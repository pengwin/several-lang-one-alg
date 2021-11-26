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
	mkdir -p ./metrics
	echo '' > ./metrics/go.txt
	$(TIME) ./bin/go 2>&1 | tee -a ./metrics/go.txt

time-cpp-bin: cpp-bin
	mkdir -p ./metrics
	echo '' > ./metrics/cpp.txt
	$(TIME) ./bin/cpp 2>&1 | tee -a ./metrics/cpp.txt

time-js-bin: js-bin
	mkdir -p ./metrics
	echo '' > ./metrics/js.txt
	$(TIME) node ./bin/nodejs/index-cli.js 2>&1 | tee -a ./metrics/js.txt

time-dotnet-bin: dotnet-bin
	mkdir -p ./metrics
	echo '' > ./metrics/dotnet.txt
	$(TIME) ./bin/dotnet/SquareSumsCli 2>&1 | tee -a ./metrics/dotnet.txt

time-bin: time-dotnet-bin time-go-bin time-js-bin time-cpp-bin

