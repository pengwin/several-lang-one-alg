const fs = require('fs');
const puppeteer = require('puppeteer');

function pause(duration) {
  return new Promise((resolve) => {
    setTimeout(() => resolve(), duration);
  });
}

async function testLang(lang) {
  const browser = await puppeteer.launch({
    headless: true,
    devtools: false
  });
  const version = await browser.version();

  console.log(`Testing ${lang} on ${version}`);

  const incognito = await browser.createIncognitoBrowserContext();
  const page = await incognito.newPage();
  page
    //.on('console', message => console.log(`${message.type().substr(0, 3).toUpperCase()} ${message.text()}`))
    .on('pageerror', ({ message }) => console.error(message))
    .on('requestfailed', request => console.error(`${request.failure().errorText} ${request.url()}`));
  await page.setCacheEnabled(false);
  await page._client.send('Network.enable', { // increase size of cache for dev tools to get correct response length for dotnet.wasm
    maxResourceBufferSize: 1024 * 1204 * 100,
    maxTotalBufferSize: 1024 * 1204 * 200,
  });
  await page.goto('http://localhost:3000');

  function hasElement(elementId) {
    return page.evaluate((id) => {
      const el = document.getElementById(id);
      return !!el;
    }, elementId);
  }

  function hasElementWithWait(elementId, maxAttempts, timeoutMs) {

    const timeouts = [];

    function cancelTimeouts() {
      for (let t of timeouts) {
        clearTimeout(t);
      }
    }

    return new Promise(async (resolve, reject) => {
      let resolved = false;
      const isResolved = () => resolved;
      const setResolved = (val) => resolved = val;
      let attempts = 0;
      for (let i = 0; i < maxAttempts; i++) {
        const timeoutId = setTimeout(async () => {
          if (isResolved()) {
            return;
          }
          const el = await hasElement(elementId);
          //console.log(`Attempts for ${elementId} ${attempts}/${maxAttempts} found: ${el}`)
          if (el) {
            setResolved(true);
            resolve(true);
            cancelTimeouts();
          } else {
            attempts++;
            if (attempts >= maxAttempts) {
              resolve(false);
            }
          }
        }, i * timeoutMs);
        timeouts.push(timeoutId);
      }
    });
  }

  let metrics = await page.metrics();
  const heapBefore = metrics.JSHeapTotalSize;

  const loadBtnId = `${lang}_load`;

  if (!await hasElementWithWait(loadBtnId, 5, 100)) {
    console.error('no load button');
    return;
  }

  let networkMetrics = {
    size: 0,
  };

  page.on('response', async response => {
    //console.log(`Response : ${response.url()} ${response.status()}`);
    response.buffer().then((buf) => {
      networkMetrics.size += buf.length;
    }).catch((e) => console.error(`Unable to read response body for ${response.url()}`, e));
    
  });

  await page.evaluate((id) => {
    const el = document.getElementById(id);
    el.click();
  }, loadBtnId);

  const runBtnId = `${lang}_run`;

  if (!await hasElementWithWait(runBtnId, 5, 1000)) {
    console.error('no run button');
    return false;
  }

  metrics = await page.metrics();
  const heapAfterLoad = metrics.JSHeapTotalSize;

  await page.evaluate((id) => {
    const el = document.getElementById(id);
    el.click();
  }, runBtnId);

  const resultElId = 'result';

  console.log(`Starting waiting for ${lang} result ${new Date()}`)

  if (!await hasElementWithWait(resultElId, 1000, 1000)) {
    console.error('waiting result too long');
    return false;
  }

  const strResult = await page.evaluate((id) => {
    const el = document.getElementById(id);
    return el.innerText;
  }, resultElId);

  const duration = parseFloat(strResult.replace('s', '').trim());

  metrics = await page.metrics();
  const heapAfterRun = metrics.JSHeapTotalSize;

  const result = {
    duration: duration,
    totalHeapDiff: (heapAfterRun - heapBefore) / 1024.0,
    size: networkMetrics.size / (1024.0)
  };

  await browser.close();

  return result;
}

const metricsMap = {
  'duration': {
      tableHeader: 'Time',
      hint: 'Calculation duration measured using browser Performance Api'
  },
  'totalHeapDiff': {
      tableHeader: 'Memory',
      hint: 'Difference (in Kb) between JSHeapTotalSize before loading script and after calculations run'
  },
  'size': {
      tableHeader: 'Size',
      hint: 'Size (in Kb) of files loaded to run calculations'
  }
};

function markdownTable(metrics, metricsMap) {

  let header = '| Lang |';
  let delimiter = '| --- |';
  let content = '';

  for (let metricName in metricsMap) {
      const metricDef = metricsMap[metricName];
      header += `${metricDef.tableHeader} | `;
      delimiter += ' ---- | '
  }

  header += '\n';
  content += '\n';

  for (let key in metrics) {
      content += `| ${key} | `;
      const metric = metrics[key];
      for (let metricName in metricsMap) {
          content += `${metric[metricName].toFixed(2)} |`
      }
      content += '\n';
  }

  return header + delimiter + content;
}

function tableHint(metricsMap) {

  let result = '';

  for (let metricName in metricsMap) {
      const metricDef = metricsMap[metricName];
      result += `- **${metricDef.tableHeader}** - ${metricDef.hint} \n`;
  }

  return result;
}

async function main(jsonPath, markdownPath) {

  let langs = [
    'cpp', 'rust',
    'js', 'golang', 
    'dotnet_aot','dotnet_no_aot', 'dotnet-uno'
  ];

  let langsMap = {
    'cpp': 'C++ (Wasm)',
    'js': 'JS (Browser)',
    'golang': 'Go (Wasm)',
    'rust': 'Rust (Wasm)',
    'dotnet_aot': 'Blazor (AOT)',
    'dotnet_no_aot': 'Blazor',
    'dotnet-uno': 'UNO'
  }

  let metrics = {};

  for (let lang of langs) {
    const r = await testLang(lang);
    metrics[langsMap[lang]] = r;
    console.log(`Result ${r.duration} seconds`);
  }

  const table = markdownTable(metrics, metricsMap);
  const hint = tableHint(metricsMap);
  const markdown = table+hint;

  console.log(markdown);

  fs.writeFileSync(markdownPath, markdown);
  fs.writeFileSync(jsonPath, JSON.stringify(metrics));
  
}

const json_path = process.argv[2];
const markdown_path = process.argv[3];

main(json_path, markdown_path);