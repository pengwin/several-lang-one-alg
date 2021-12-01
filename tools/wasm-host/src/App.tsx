import React, { useState } from 'react';

const anyWindow = () => window as any;

function createWasmMap() {
  const map = new Map<string, { loader: () => Promise<void>, runner: () => Promise<void> }>();

  map.set('dotNet', {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = 'dotnet_wasm';
      if (document.getElementById(scriptId)) {
        resolve();
      }
      const script = document.createElement('script');
      script.id = scriptId;
      script.src = "/_framework/blazor.webassembly.js";
      script.setAttribute('autostart', 'false');
      script.async = false;
      script.addEventListener('load', () => {
        anyWindow().Blazor.start().then(() => {
          resolve();
        });
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      anyWindow().DotNet.invokeMethodAsync('SquareSumsWasm', 'FullSquareSums', 500, 1000)
        .then((data: any) => {
          console.log(data);
          resolve();
        });
    })
  });

  map.set('golang', {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = 'golang_wasm';
      if (document.getElementById(scriptId)) {
        resolve();
      }
      const script = document.createElement('script');
      script.id = scriptId;
      script.src = "/go-wasm_exec.js";
      script.async = false;
      script.addEventListener('load', () => {
        const go = new (anyWindow().Go)();
        WebAssembly.instantiateStreaming(fetch("/go-main.wasm"), go.importObject).then((result) => {
          go.run(result.instance);
          resolve();
        });
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      /*const r = window.SquareSumsRow(649);
      console.log(r);*/
      anyWindow().fullSquareSums(2, 2000);
      resolve();
    })
  });

  map.set('cpp', {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = 'cpp_wasm';
      if (document.getElementById(scriptId)) {
        resolve();
      }

      anyWindow().Module = {
        onRuntimeInitialized: function () {
          resolve();
        }
      };

      const script = document.createElement('script');
      script.id = scriptId;
      script.src = "/cpp-wasm.js";
      script.async = false;
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      anyWindow().Module._FullSquareSums(2, 2000);
      resolve();
    })
  });

  map.set('rust', {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = 'rust_wasm';
      if (document.getElementById(scriptId)) {
        resolve();
      }
      const script = document.createElement('script');
      script.id = scriptId;
      script.src = "/rust-wasm-entry.js";
      script.setAttribute('autostart', 'false');
      script.type = 'module';
      script.addEventListener('load', () => {
        anyWindow().init_rust_wasm().then((m: unknown) => {
          anyWindow().rust_square_sums = (m as any).square_sums;
          resolve();
        }).catch((e: unknown) => console.error(e));
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      const res = anyWindow().rust_square_sums(2,2000);
      resolve(res);
    })
  });

  return map;
}

const wasmMap = createWasmMap();


function App() {
  const [state, setState] = useState<{ wasmLoaded: any }>({ wasmLoaded: {} });

  const loadWasm = (key: string) => wasmMap.get(key)?.loader().then(() => {
    const wasmLoaded = { ...state.wasmLoaded };
    wasmLoaded[key] = true;
    setState({ ...state, wasmLoaded });
  });

  const runWasm = (key: string) => {
    console.time(`calc ${key}`);
    wasmMap.get(key)?.runner().then(() => console.timeEnd(`calc ${key}`)).catch(() => console.timeEnd(`calc ${key}`));
  }

  const buttons = [];
  for (let key of wasmMap.keys()) {
    buttons.push(<div key={key}>
      <p>{key}</p>
      {!state.wasmLoaded[key]
        ? <button onClick={() => loadWasm(key)}>Load WASM</button>
        : <button onClick={() => runWasm(key)}>Run WASM</button>
      }
    </div>);
  }

  return (
    <div>
      {buttons}
    </div>
  );
}

export default App;
