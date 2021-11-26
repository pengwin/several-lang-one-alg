const fs = require('fs');
const path = require('path');

function loadMetricsFromFile(filePath, metricsMap) {
    const result = {};
    const text = fs.readFileSync(filePath, 'utf-8').split('\n');
    for (let line of text) {
        line = line.trim();
        for (let metricName in metricsMap) {
            const metric = metricsMap[metricName];
            let makerPos = line.indexOf(metric.marker);
            if (makerPos === -1) {
                continue;
            }

            const value = line.substr(makerPos + metric.marker.length);
            result[metricName] = value;
        }
    }
    return result;
}

function loadAllMetrics(metricsPath, binariesMap, metricsMap) {
    const result = {};
    for (let key in binariesMap) {
        const metrics = loadMetricsFromFile(path.join(metricsPath, binariesMap[key].file), metricsMap);
        result[key] = metrics;
    }
    return result;
}

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
            content += `${metric[metricName]} |`
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

function main() {

    const metricsMap = {
        'elapsedTime': {
            marker: 'Elapsed (wall clock) time (h:mm:ss or m:ss): ',
            tableHeader: 'Time',
            hint: 'Elapsed (wall clock) time (h:mm:ss or m:ss)'
        },
        'maxMem': {
            marker: 'Maximum resident set size (kbytes): ',
            tableHeader: 'Memory',
            hint: 'Maximum resident set size (kbytes)'
        },
        'normalDfs': {
            marker: 'Normal (*<=n) dfs cases count: ',
            tableHeader: 'Normal',
            hint: 'Number of calls to DFS function compared to number of graph nodes (calls<=n)'
        },
        'averageDfs': {
            marker: 'Average (n<*<3n) dfs cases count: ',
            tableHeader: 'Average',
            hint: 'Number of calls to DFS function compared to number of graph nodes (n< calls <3n)'
        },
        'badDfs': {
            marker: 'Bad dfs (*>3n) cases count: ',
            tableHeader: 'Bad',
            hint: 'Number of calls to DFS function compared to number of graph nodes (calls > 3n)'
        },
        'worstDfs': {
            marker: 'Worst dfs case: ',
            tableHeader: 'Worst',
            hint: 'The worst number of calls to DFS function and number of graph nodes'
        }
    };

    const metricsPath = '../../metrics';

    const binariesMap = {
        'C++': {
            file: 'cpp.txt'
        },
        'Go': {
            file: 'go.txt'
        },
        '.NET': {
            file: 'dotnet.txt'
        },
        'JS': {
            file: 'js.txt'
        }
    };
    const metrics = loadAllMetrics(metricsPath, binariesMap, metricsMap);
    const table = markdownTable(metrics, metricsMap);

    console.log(table);
    console.log('');
    console.log(tableHint(metricsMap));
}

main();





