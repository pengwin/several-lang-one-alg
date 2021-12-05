OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/plotter
OUT_DIR_WASM=../../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/rust-wasm

.PHONY: all build
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	RUSTFLAGS="-C target-cpu=native" cargo build --release
	cp -v ./target/release/plotter $(OUTPUT_BIN)