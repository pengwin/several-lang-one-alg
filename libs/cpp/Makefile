OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/cpp
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/cpp-wasm.html
DEBUG_DIR=./bin/debug
DEBUG_BIN=$(DEBUG_DIR)/cpp

.PHONY: all build wasm copy build-debug
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	g++ -O3 -march=native main.cpp -o $(OUTPUT_BIN)

wasm:
	mkdir -p $(OUT_DIR_WASM)
	em++ -O3 main_wasm.cpp -std=c++20 -s WASM=1 -fsanitize=address -s ALLOW_MEMORY_GROWTH -s ASSERTIONS=2 -o $(OUTPUT_WASM)

build-debug:
	mkdir -p $(DEBUG_DIR)
	g++ main.cpp -g -o $(DEBUG_BIN)

run-debug: build-debug
	gdb $(DEBUG_BIN)

valgrind: build-debug
	valgrind --leak-check=full \
         --show-leak-kinds=all \
         --track-origins=yes \
         --verbose \
         ${DEBUG_BIN}