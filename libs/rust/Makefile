OUT_DIR_BIN=../../bin
OUTPUT_BIN=$(OUT_DIR_BIN)/rust
OUT_DIR_WASM=../../../wasm
OUTPUT_WASM=$(OUT_DIR_WASM)/rust-wasm

.PHONY: all build wasm pprof pprof-svg
all: build

build:
	mkdir -p $(OUT_DIR_BIN)
	RUSTFLAGS="-C target-cpu=native" cargo build --release
	cp -v ./target/release/cli $(OUTPUT_BIN)

wasm:
	wasm-pack build --release --target web --out-dir $(OUTPUT_WASM) rust-wasm

pprof-svg:
	mkdir -p ./pprof
	cargo run --bin pprof-alg -- ./pprof/profiling.pb
	go tool pprof -web ./pprof/profiling.pb

pprof:
	mkdir -p ./pprof
	cargo run --bin pprof-alg -- ./pprof/profiling.pb
	go tool pprof ./pprof/profiling.pb
