OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/dotnet
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/dotnet

.PHONY: all build wasm copy
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	dotnet publish -c Release -r linux-x64 --self-contained false -o $(OUTPUT_BIN)

wasm:
	mkdir -p $(OUT_DIR_WASM)
	em++ -O3 main_wasm.cpp -s WASM=1 -o $(OUTPUT_WASM)


