## Intoduction

Solution for [cata](https://www.codewars.com/kata/5a667236145c462103000091) from codewars implemented in different languages.

## Requirements

OS: Linux
Installed developments tools: GCC(11+), Rust (1.5+), Nodejs (14+), Golang, Dotnet (6+), Emscripten (3+), GNU Time, Bash, Make
TDB Docker build 

## Test run results

### Machine/OS parameters:

```
AMD Ryzen 7 3700X 8-Core Processor
32 GB RAM
Gentoo Linux 5.10.76
```

### Compilation

#### C++

Compiler: g++ (Gentoo 11.2.0 p1) 11.2.0

```
g++ -O3 -march=native main.cpp -o $(OUTPUT_BIN)
```

#### Golang
go version: go1.17.5 linux/amd64
```
go build -o $(OUTPUT_BIN) cmd/cli/main.go 
```

#### .NET

.NET version: 6.0.100
```
dotnet publish -c Release -r linux-x64 --self-contained false -o $(OUTPUT_BIN)
```

#### JS
Typescript: 4.5.2

Node version: v14.15.5
```
tsc --outDir ../../bin/nodejs --target es2016 --module none --excludeFiles **/*.test.ts
node bin/nodejs/index-cli.js
```

#### Rust
Rustc version 1.56.1
```
cargo build --release
```
Release profile
```
[profile.release]
codegen-units = 1
panic = "abort"
opt-level = 3
overflow-checks = false
```

### Results


