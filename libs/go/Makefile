GOROOT=$(shell go env GOROOT)
OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/go
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/go-main.wasm
OUTPUT_WASM_EXEC=$(OUT_DIR_WASM)/go-wasm_exec.js

.PHONY: all build wasm
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	go build -o $(OUTPUT_BIN) cmd/cli/main.go 

wasm:
	mkdir -p $(OUT_DIR_WASM)
	GOOS=js GOARCH=wasm go build -o $(OUTPUT_WASM) cmd/wasm/main.go 
	cp $(GOROOT)/misc/wasm/wasm_exec.js $(OUTPUT_WASM_EXEC)

pprof-svg:
	GOGC=off go test -bench=BenchmarkSimpleOneShot -cpuprofile cpu.out ./tests
	go tool pprof -web tests.test cpu.out > cpu.svg

pprof:
	GOGC=off go test -bench=BenchmarkSimpleOneShot -cpuprofile cpu.out ./tests
	go tool pprof tests.test cpu.out