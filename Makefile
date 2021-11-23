SHELL := /bin/bash

.PHONY: all build wasm
all: build

build:
	make -C libs/go build
	make -C libs/cpp build
	make -C libs/dotnet build

time: build
	./time.sh