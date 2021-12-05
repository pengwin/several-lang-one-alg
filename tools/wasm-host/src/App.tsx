import React, { useState } from 'react';

declare var window: any;

let cppApi = {
  _FullSquareSums(from: number, to: number) {

  }
};

let jsApi = {
  fullSquareSums(from: number, to: number) {
  }
}

window.wasmRunner = window.wasmRunner || { from: 2, to: 2000 };

function getDotnetDefinition(prefix: string) {
  return {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = `${prefix}_wasm`;
      if (document.getElementById(scriptId)) {
        resolve();
      }
      const script = document.createElement('script');
      script.id = scriptId;
      script.src = `/${prefix}/_framework/blazor.webassembly.js`;
      script.setAttribute('autostart', 'false');
      script.async = false;
      script.addEventListener('load', async () => {
        await window.Blazor.start({
          loadBootResource(type: string, name: string, defaultUri: string, integrity: string) {
            return defaultUri.replace('_framework', `/${prefix}/_framework`)
          }
        });
        resolve();
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      window.DotNet.invokeMethodAsync('SquareSumsWasm', 'FullSquareSums', window.wasmRunner.from, window.wasmRunner.to)
        .then((data: any) => {
          console.log(data);
          resolve();
        });
    })
  };
}

function createWasmMap() {
  const map = new Map<string, { loader: () => Promise<void>, runner: () => Promise<void> }>();

  map.set('dotnet_aot', getDotnetDefinition('dotnet-aot'));

  map.set('dotnet_no_aot', getDotnetDefinition('dotnet-no-aot'));

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
        const go = new (window.Go)();
        WebAssembly.instantiateStreaming(fetch("/go-main.wasm"), go.importObject).then((result) => {
          go.run(result.instance);
          resolve();
        });
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      window.fullSquareSums(window.wasmRunner.from, window.wasmRunner.to);
      resolve();
    })
  });

  map.set('cpp', {
    loader: () => new Promise<void>((resolve, reject) => {
      const scriptId = 'cpp_wasm';
      if (document.getElementById(scriptId)) {
        resolve();
      }
      const script = document.createElement('script');
      script.id = scriptId;
      script.src = "/cpp-wasm-entry.js";
      script.type = 'module';
      script.addEventListener('load', () => {
        const instance = window.cppWasmModule({
          onRuntimeInitialized() {
            instance.then((api: unknown) => {
              cppApi = api as any;
              resolve();
            });
          }
        });
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      cppApi._FullSquareSums(window.wasmRunner.from, window.wasmRunner.to);
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
      script.type = 'module';
      script.addEventListener('load', () => {
        window.init_rust_wasm().then((m: unknown) => {
          window.rust_square_sums = (m as any).square_sums;
          resolve();
        }).catch((e: unknown) => console.error(e));
      });
      document.body.appendChild(script);
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      const res = window.rust_square_sums(window.wasmRunner.from, window.wasmRunner.to);
      resolve(res);
    })
  });

  map.set('js', {
    loader: () => new Promise<void>(async (resolve, reject) => {
      const { fullSquareSums } = await import('square-sums/dist/index-web');
      jsApi = {
        fullSquareSums
      };
      resolve();
    }),
    runner: () => new Promise<void>((resolve, reject) => {
      jsApi.fullSquareSums(window.wasmRunner.from, window.wasmRunner.to);
      resolve();
    })
  });

  return map;
}

const wasmMap = createWasmMap();

interface State {
  status: string;
  result: number;
  wasmLoaded: any
}


function App() {
  const [state, setState] = useState<State>({ status: '', result: 0, wasmLoaded: {} });

  const loadWasm = (key: string) => wasmMap.get(key)?.loader().then(() => {
    const wasmLoaded = { ...state.wasmLoaded };
    wasmLoaded[key] = true;
    setState({ ...state, wasmLoaded });
  });

  const runWasm = (key: string) => {
    setState({...state, status: `Calculating ${key} from: ${window.wasmRunner.from} to: ${window.wasmRunner.to}`});
    console.time(`calc ${key}`);

    function endCalc(startTime: number) {
      console.timeEnd(`calc ${key}`);
      const end = performance.now();
      const result = (end - startTime)/1000.0;
      console.log(result);
      setState({...state, status: `Result for ${key}`, result});
    }

    setTimeout(() => {
      const start = performance.now();
      wasmMap.get(key)?.runner().then(() => endCalc(start)).catch(() => endCalc(start));
    });
    
  }

  const buttons = [];
  for (let key of wasmMap.keys()) {
    buttons.push(<div key={key}>
      <p>{key}</p>
      {!state.wasmLoaded[key]
        ? <button id={key+'_load'} onClick={() => loadWasm(key)}>Load WASM</button>
        : <button id={key+'_run'} onClick={() => runWasm(key)}>Run WASM</button>
      }
    </div>);
  }

  return (
    <div>
      <div>
        <h3>{state.status}</h3>
        {state.result > 0 ? <div id="result">{state.result.toFixed(2)} s</div> : null}
      </div>
      <div>
        {buttons}
      </div>
    </div>
  );
}

export default App;
