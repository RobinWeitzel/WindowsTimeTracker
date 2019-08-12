const fs = require('fs');
const request = require('request');

const get = async url => {
    return new Promise((resolve, reject) => {
        request(url, (error, response, body) => {
            resolve(body);
        });
    });
}

const getLocal = async path => {
    return new Promise((resolve, reject) => {
        fs.readFile(path, 'utf8', (err, content) => {
            if (err) {
                reject(err);
            } else {
                resolve(content);
            }
        });
    });
}

const myArgs = process.argv.slice(2);

fs.readFile('index.html', 'utf8', async (err, html) => {

    const styles = html.match(/<link [\s\S]*?>/g);
    const scripts = html.match(/<script [\s\S]*?<\/script>/g);

    // Styles
    for (const match of styles) {
        const href = match.match(/href=["|'](.*?)["|']/)[1];

        let content;

        if (href.startsWith("http")) { // file must be downloaded from the web
            content = await get(href);
        } else { // local file
            content = await getLocal(href);
        }

        html = html.replace(new RegExp(match), `<style>\n${content}\n</style>`);
    }

    // Scripts
    for (const match of scripts) {
        const href = match.match(/src=["|'](.*?)["|']/)[1];

        let content;

        if (href.startsWith("http")) { // file must be downloaded from the web
            content = await get(href);
        } else { // local file
            content = await getLocal(href);
        }

        const start = html.search(new RegExp(match));

        const replacement = `<script>\n${content}\n</script>`;

        html = html.substr(0, start) + replacement + html.substr(start + match.length);

        //html = html.replace(new RegExp(match), `<script>\n${content}\n</script>`);
    }

    // Output file
    fs.mkdirSync('dist');
    fs.writeFile("dist/index.html", html, function (err) {
        if (err) {
            return console.log(err);
        }

        console.log("The file was saved!");
    });
});