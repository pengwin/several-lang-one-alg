## Intoduction

Solution for [cata](https://www.codewars.com/kata/5a667236145c462103000091) from codewars implemented in different languages.

**Warning** The 


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
go version: go1.17.2 linux/amd64
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

### Results


