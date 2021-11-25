OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/cpp
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/cpp-wasm.html

.PHONY: all build wasm copy
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	g++ -O3 -march=native main.cpp -o $(OUTPUT_BIN)

wasm:
	mkdir -p $(OUT_DIR_WASM)
	em++ -O3 main_wasm.cpp -s WASM=1 -o $(OUTPUT_WASM)

debug-run:
	g++ main.cpp -g -o $(OUTPUT_BIN)
	gdb $(OUTPUT_BIN)
