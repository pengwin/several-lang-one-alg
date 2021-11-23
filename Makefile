.PHONY: all build wasm
all: build

build:
	make -C libs/go build
	make -C libs/cpp build
	make -C libs/dotnet build
	make -C libs/js build

time: build
	./time.sh