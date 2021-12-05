## WASM: Test run results

Results collected by nodejs script which uses puppeteer 

### Machine/OS parameters:

```
AMD Ryzen 7 3700X 8-Core Processor
32 GB RAM
Gentoo Linux 5.10.76
Browser: HeadlessChrome/97.0.4691.0
Puppeteer: 12.0.1
```

### Compilation

#### C++

Compiler: emcc (Emscripten gcc/clang-like replacement + linker emulating GNU ld) 3.0.0

```
emcc -O3 \
	--bind \
	-s ALLOW_MEMORY_GROWTH=1 \
    -s MODULARIZE=1 \
    -s EXPORT_ES6=1 \
	-std=c++20 \
	-o $(OUTPUT_WASM) main_wasm.cpp
```

#### Golang
go version: go1.17.2 linux/amd64
```
GOOS=js GOARCH=wasm go build -o $(OUTPUT_WASM) cmd/wasm/main.go 
```

#### .NET

.NET version: 6.0.100
```
dotnet publish -c Release -property:AOT=True -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj
dotnet publish -c Release -property:AOT=False -o $(OUTPUT_WASM) ./SquareSumsWasm/SquareSumsWasm.csproj
```

#### JS
Typescript: 4.5.2
```
tsc --target es2016 --module es6 --declaration --outDir ./dist
```

#### Rust
Rustc version 1.56.1
Wasm-pack version 0.10.1
```
wasm-pack build --release --target web --out-dir $(OUTPUT_WASM) rust-wasm
```
Release profile
```
[profile.release]
codegen-units = 1
panic = "abort"
opt-level = 3
overflow-checks = false
[package.metadata.wasm-pack.profile.release]
wasm-opt = ['-O4']
```
### Results

Tested on **HeadlessChrome/97.0.4691.0**




