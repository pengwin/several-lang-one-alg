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


| Lang |Time | Memory | Normal | Average | Bad | Worst | 
| --- | ---- |  ---- |  ---- |  ---- |  ---- |  ---- | 
| C++ | 0:13.82 |4668 |1648/2000 |257/2000 |75/2000 |117080 for 102 |
| Go | 0:27.34 |8196 |1648/2000 |257/2000 |75/2000 |117080 for 102 |
| .NET | 0:24.79 |115656 |1648/2000 |257/2000 |75/2000 |117080 for 102 |
| JS | 0:23.61 |69288 |1648/2000 |250/2000 |82/2000 |117080 for 102 |
| Rust | 0:13.20 |4096 |1648/2000 |257/2000 |75/2000 |117080 for 102 |


- **Time** - Elapsed (wall clock) time (h:mm:ss or m:ss) 
- **Memory** - Maximum resident set size (kbytes) 
- **Normal** - Number of calls to DFS function compared to number of graph nodes (calls<=n) 
- **Average** - Number of calls to DFS function compared to number of graph nodes (n< calls <3n) 
- **Bad** - Number of calls to DFS function compared to number of graph nodes (calls > 3n) 
- **Worst** - The worst number of calls to DFS function and number of graph nodes 

