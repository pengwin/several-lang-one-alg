OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/dotnet
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/dotnet

.PHONY: all build wasm copy wasm wasm-aot wasm-no-aot
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	dotnet publish -c Release -r linux-x64 --self-contained false -o $(OUTPUT_BIN) ./SquareSumsCli/SquareSumsCli.csproj

wasm: wasm-dir wasm-aot wasm-no-aot

wasm-dir:
	mkdir -p $(OUT_DIR_WASM)

wasm-aot:
	dotnet publish -c Release -property:AOT=True -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj

wasm-no-aot:
	dotnet publish -c Release -property:AOT=False -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj

#view on https://www.speedscope.app/
profile:
	dotnet build
	mkdir -p ./profiler
	dotnet-trace collect --format speedscope --output ./profiler/speedscope.trace -- ./SquareSumsCli/bin/Debug/net6.0/SquareSumsCli
	speedscope ./profiler/speedscope.speedscope.json