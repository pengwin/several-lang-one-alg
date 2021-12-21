OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/dotnet
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/dotnet
OUTPUT_WASM_UNO=$(OUT_DIR_WASM)/dotnet-uno

.PHONY: all build wasm copy wasm wasm-aot wasm-no-aot wasm-uno clear-uno
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	dotnet publish -c Release -r linux-x64 --self-contained false -o $(OUTPUT_BIN) ./SquareSumsCli/SquareSumsCli.csproj

wasm: wasm-dir wasm-aot wasm-no-aot wasm-uno

wasm-dir:
	mkdir -p $(OUT_DIR_WASM)

wasm-aot:
	dotnet publish -c Release -property:AOT=True -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj

wasm-no-aot:
	dotnet publish -c Release -property:AOT=False -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj

clear-uno:
	rm -rf $(OUTPUT_WASM_UNO)

wasm-uno: clear-uno
	dotnet publish -c Release -v d -o $(OUTPUT_WASM_UNO) ./SquareSumsUno.Wasm/SquareSumsUno.Wasm.csproj

#view on https://www.speedscope.app/
profile:
	dotnet build
	mkdir -p ./profiler
	dotnet-trace collect --format speedscope --output ./profiler/speedscope.trace -- ./SquareSumsCli/bin/Debug/net6.0/SquareSumsCli
	speedscope ./profiler/speedscope.speedscope.json