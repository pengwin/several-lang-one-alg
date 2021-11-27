.PHONY: all build wasm go-bin cpp-bin dotnet-bin js-bin
all: build

SHELL = /bin/bash
TIME=/usr/bin/time --verbose # use gnu-time, not bash builtin time

METRICS_DIR=./metrics

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
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/go.txt
	$(TIME) ./bin/go 2>&1 | tee -a $(METRICS_DIR)/go.txt

time-cpp-bin: cpp-bin
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/cpp.txt
	$(TIME) ./bin/cpp 2>&1 | tee -a $(METRICS_DIR)/cpp.txt

time-js-bin: js-bin
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/js.txt
	$(TIME) node ./bin/nodejs/index-cli.js 2>&1 | tee -a $(METRICS_DIR)/js.txt

time-dotnet-bin: dotnet-bin
	mkdir -p $(METRICS_DIR)
	echo '' > ./metrics/dotnet.txt
	$(TIME) ./bin/dotnet/SquareSumsCli 2>&1 | tee -a $(METRICS_DIR)/dotnet.txt

time-bin: time-dotnet-bin time-go-bin time-js-bin time-cpp-bin

md-table: time-bin
	node ./tools/graphs/src/index.js $(METRICS_DIR) > ./bin/results.md

readme: md-table
	cat ./docs/README.header.md > ./README.md
	cat ./bin/results.md >> ./README.md

