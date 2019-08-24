const fs = require('fs');
const request = require('request');
const minify = require('html-minifier').minify;

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

const escapeRegExp = string => {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}

let errors = 0;

const myArgs = process.argv.slice(2);

console.log("--- BUILD STARTED ---");

fs.readFile('index.html', 'utf8', async (err, html) => {

    const styles = html.match(/<link [\s\S]*?>/g);
    const scripts = html.match(/<script [\s\S]*?<\/script>/g);

    // Styles
    console.log("LOADING STYLES:");
    console.log(styles.length + " styles found.");
    for (let i = 0; i < styles.length; i++) {
        try{
            const match = styles[i];
            const href = match.match(/href=["|'](.*?)["|']/)[1];

            let content;

            console.log("Loading style " + (i+1) +"/" + (styles.length) + " from " + (href.startsWith("http") ? "the internet." : "local filesystem."))
            if (href.startsWith("http")) { // file must be downloaded from the web
                content = await get(href);
            } else { // local file
                content = await getLocal(href);
            }

            
            const start = html.search(new RegExp(escapeRegExp(match)));
            
            const replacement = `<style>\n${content}\n</style>`;
            
            html = html.substr(0, start) + replacement + html.substr(start + match.length);
            
            console.log("Finished loading style " + (i+1) +"/" + (styles.length) + ".");
        } catch(e) {
            errors++;
            console.error("Could not load style " + (i+1) +"/" + (styles.length) + ".");
        }
    }
    console.log("");

    // Scripts
    console.log("LOADING SCRIPTS:");
    console.log(styles.length + " scripts found.");
    for (let i = 0; i < scripts.length; i++) {
        try{
            const match = scripts[i];
            const href = match.match(/src=["|'](.*?)["|']/)[1];

            let content;

            console.log("Loading script " + (i+1) +"/" + (scripts.length) + " from " + (href.startsWith("http") ? "the internet." : "local filesystem."))
            if (href.startsWith("http")) { // file must be downloaded from the web
                content = await get(href);
            } else { // local file
                content = await getLocal(href);
            }

            const start = html.search(new RegExp(escapeRegExp(match)));

            const replacement = `<script>\n${content}\n</script>`;

            html = html.substr(0, start) + replacement + html.substr(start + match.length);

            console.log("Finished loading script " + (i+1) +"/" + (scripts.length) + ".");
        } catch(e) {
            errors++;
            console.error("Could not load script " + (i+1) +"/" + (scripts.length) + ".");
        }
    }

    // Minify
    /*html = minify(html, {
        minifyCSS: true,
        minifyJS: true,
        collapseWhitespace: true,
        collapseInlineTagWhitespace: true,
        html5: true,
        removeComments: true,

    });*/

    // Output file
    try {
        if(!fs.existsSync('dist'))
            fs.mkdirSync('dist');
        fs.writeFile("dist/index.html", html, function (err) {
            if (err) {
                return console.log(err);
            }

            console.log("Saved file");

            if(errors === 0) {
                console.log("--- BUILD FINISHED SUCCESSFULLY ---");
            } else {
                console.log("--- BUILD FINISHED WITH ERRORS ---");
            }
        });
    } catch(e) {
        errors++;
        console.error("Could not save file.");

        if(errors === 0) {
            console.log("--- BUILD FINISHED SUCCESSFULLY ---");
        } else {
            console.log("--- BUILD FINISHED WITH ERRORS ---");
        }
    }
});