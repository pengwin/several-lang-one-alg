OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/rust
OUT_DIR_WASM=../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/cpp-wasm.html
DEBUG_DIR=./bin/debug
DEBUG_BIN=$(DEBUG_DIR)/cpp

.PHONY: all build wasm 
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	RUSTFLAGS="-C target-cpu=native" cargo build --release
	cp -v ./target/release/cli $(OUTPUT_BIN)
