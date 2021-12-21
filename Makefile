.PHONY: all build wasm go-bin cpp-bin dotnet-bin js-bin rust-bin go-wasm cpp-wasm dotnet-wasm rust-wasm build-plotter histograms
all: build

SHELL = /bin/bash
TIME=/usr/bin/time --verbose # use gnu-time, not bash builtin time

METRICS_DIR=./metrics
METRICS_JSON=./metrics/metrics.json
WASM_METRICS_JSON=./metrics/metrics-wasm.json
HISTOGRAMS_PATH=./docs/histograms

FROM=2
TO=2000

build: go-bin cpp-bin dotnet-bin js-bin

wasm: go-wasm cpp-wasm dotnet-wasm rust-wasm

wasm-copy: wasm
	mkdir -p ./tools/wasm-host/public/dotnet-aot/
	mkdir -p ./tools/wasm-host/public/dotnet-no-aot/
	mkdir -p ./tools/wasm-host/public/dotnet-uno/
	cp -rv ./wasm/dotnet/wwwroot/dotnet-aot/_framework ./tools/wasm-host/public/dotnet-aot/_framework
	cp -rv ./wasm/dotnet/wwwroot/dotnet-no-aot/_framework ./tools/wasm-host/public/dotnet-no-aot/_framework
	cp -v ./wasm/cpp-wasm.js ./tools/wasm-host/public/cpp-wasm.js
	cp -v ./wasm/cpp-wasm.wasm ./tools/wasm-host/public/cpp-wasm.wasm
	cp -v ./wasm/go-main.wasm ./tools/wasm-host/public/go-main.wasm
	cp -v ./wasm/go-wasm_exec.js ./tools/wasm-host/public/go-wasm_exec.js 
	cp -rv ./wasm/rust-wasm ./tools/wasm-host/public
	cp -rv ./wasm/dotnet-uno/embedded.js ./tools/wasm-host/public/dotnet-uno/embedded.js
	cp -rv ./wasm/dotnet-uno/package_* ./tools/wasm-host/public/dotnet-uno
	
go-bin:
	make -C libs/go build

go-wasm:
	make -C libs/go wasm

cpp-bin:
	make -C libs/cpp build

cpp-wasm:
	make -C libs/cpp wasm

dotnet-bin:
	make -C libs/dotnet build

dotnet-wasm:
	make -C libs/dotnet wasm

js-bin:
	make -C libs/js build

rust-bin:
	make -C libs/rust build

rust-wasm:
	make -C libs/rust wasm

time-go-bin: go-bin
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/go.txt
	$(TIME) ./bin/go $(FROM) $(TO) 2>&1 | tee -a $(METRICS_DIR)/go.txt

time-cpp-bin: cpp-bin
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/cpp.txt
	$(TIME) ./bin/cpp $(FROM) $(TO) 2>&1 | tee -a $(METRICS_DIR)/cpp.txt

time-js-bin: js-bin
	mkdir -p $(METRICS_DIR)
	echo '' > $(METRICS_DIR)/js.txt
	$(TIME) node ./bin/nodejs/index-cli.js $(FROM) $(TO) 2>&1 | tee -a $(METRICS_DIR)/js.txt

time-dotnet-bin: dotnet-bin
	mkdir -p $(METRICS_DIR)
	echo '' > ./metrics/dotnet.txt
	$(TIME) ./bin/dotnet/SquareSumsCli $(FROM) $(TO)  2>&1 | tee -a $(METRICS_DIR)/dotnet.txt

time-rust-bin: rust-bin
	mkdir -p $(METRICS_DIR)
	echo '' > ./metrics/rust.txt
	$(TIME) ./bin/rust $(FROM) $(TO) 2>&1 | tee -a $(METRICS_DIR)/rust.txt

time-bin: time-dotnet-bin time-go-bin time-js-bin time-cpp-bin time-rust-bin

md-table: time-bin
	node ./tools/graphs/src/index.js $(METRICS_DIR) $(METRICS_JSON) > ./bin/results.md

md-wasm-table: wasm-copy
	node ./tools/wasm-collector/src/index.js $(WASM_METRICS_JSON) ./bin/results-wasm.md

build-plotter:
	make -C ./tools/plotter build

plotter: build-plotter
	mkdir -p $(HISTOGRAMS_PATH)
	./bin/plotter $(METRICS_JSON) $(WASM_METRICS_JSON) $(HISTOGRAMS_PATH)

readme: md-table md-wasm-table plotter
	cat ./docs/README.header.md > ./README.md
	cat ./bin/results.md >> ./README.md
	cat ./docs/README.binary-diagrams.md >> ./README.md
	cat ./docs/README.wasm.header.md >> ./README.md
	cat ./bin/results-wasm.md >> ./README.md
	cat ./docs/README.wasm-diagrams.md >> ./README.md

