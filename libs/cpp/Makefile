OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/cpp
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/cpp-wasm.js
DEBUG_DIR=./bin/debug
DEBUG_BIN=$(DEBUG_DIR)/cpp

.PHONY: all build wasm copy build-debug
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	g++ -O3 -march=native main.cpp -o $(OUTPUT_BIN)

# -s MALLOC=emmalloc \
#-s ALLOW_MEMORY_GROWTH \
#-s STRICT=1 \
# -fsanitize=address 
# --no-entry \
#-g3 -gsource-map -s SAFE_HEAP=1 -s SAFE_HEAP_LOG=1 --source-map-base 
# -s INITIAL_MEMORY=512MB \

wasm:
	mkdir -p $(OUT_DIR_WASM)
	emcc -O3 \
	--bind \
	-s ALLOW_MEMORY_GROWTH=1 \
    -s MODULARIZE=1 \
    -s EXPORT_ES6=1 \
	-std=c++20 \
	-o $(OUTPUT_WASM) main_wasm.cpp
	
	

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