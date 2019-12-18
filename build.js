const fs = require('fs');
const request = require('request');
const minify = require('html-minifier').minify;
const Babel = require('@babel/standalone');


Babel.registerPlugin(
    '@babel/plugin-proposal-class-properties',
    require('@babel/plugin-proposal-class-properties'),
);
/*"transform-custom-element-classes",
                            "@babel/plugin-proposal-class-properties",*/
Babel.registerPreset('@babel/preset-env', require('@babel/preset-env'));

Babel.registerPreset("my-preset", {
    presets: [
      [Babel.availablePresets["@babel/preset-env"], { "targets": { "node": true } }]
    ],
    plugins: [
      [Babel.availablePlugins["@babel/plugin-proposal-class-properties"]]
    ],
    moduleId: "main"
  });

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
        try {
            const match = styles[i];
            const href = match.match(/href=["|'](.*?)["|']/)[1];

            let content;

            console.log("Loading style " + (i + 1) + "/" + (styles.length) + " from " + (href.startsWith("http") ? "the internet." : "local filesystem."))
            if (href.startsWith("http")) { // file must be downloaded from the web
                content = await get(href);
            } else { // local file
                content = await getLocal(href);
            }

            // Remove BOM
            if (content.charCodeAt(0) === 0xFEFF) {
                content = content.substr(1);
            }

            const start = html.search(new RegExp(escapeRegExp(match)));

            const replacement = `<style>\n${content}\n</style>`;

            html = html.substr(0, start) + replacement + html.substr(start + match.length);

            console.log("Finished loading style " + (i + 1) + "/" + (styles.length) + ".");
        } catch (e) {
            errors++;
            console.error("Could not load style " + (i + 1) + "/" + (styles.length) + ".");
        }
    }
    console.log("");

    let htmlEs5 = html;

    // Scripts
    console.log("LOADING SCRIPTS:");
    console.log(scripts.length + " scripts found.");
    for (let i = 0; i < scripts.length; i++) {
        try {
            const match = scripts[i];
            const href = match.match(/src=["|'](.*?)["|']/)[1];

            /*if(match.includes("CefSharp.js")) {
                console.log("Finished loading script " + (i + 1) + "/" + (scripts.length) + ".");
                continue;
            }*/

            let content;

            console.log("Loading script " + (i + 1) + "/" + (scripts.length) + " from " + (href.startsWith("http") ? "the internet." : "local filesystem."))
            if (href.startsWith("http")) { // file must be downloaded from the web
                content = await get(href);
            } else { // local file
                content = await getLocal(href);
            }

            // Remove BOM
            if (content.charCodeAt(0) === 0xFEFF) {
                content = content.substr(1);
            }

            const start = html.search(new RegExp(escapeRegExp(match)));

            const replacement = `<script>\n${content}\n</script>`;
            let replacementEs5 = replacement;
            if (match.includes("View") || match.includes("Navigation") || match.includes("CefSharp.js")) {
                replacementEs5 = `<script>\n${Babel.transform(content, {
                    presets: [
                        'my-preset'
                    ]
                }).code}\n</script>`;
            }

            html = html.substr(0, start) + replacement + html.substr(start + match.length);

            const startEs5 = htmlEs5.search(new RegExp(escapeRegExp(match)));
            htmlEs5 = htmlEs5.substr(0, startEs5) + replacementEs5 + htmlEs5.substr(startEs5 + match.length);

            console.log("Finished loading script " + (i + 1) + "/" + (scripts.length) + ".");
        } catch (e) {
            errors++;
            console.error("Could not load script " + (i + 1) + "/" + (scripts.length) + ".");
            console.error(e);
        }
    }

    // Minify
    const minHtml = minify(html, {
        minifyCSS: true,
        minifyJS: true,
        collapseWhitespace: false,
        collapseInlineTagWhitespace: false,
        html5: true,
        removeComments: false,

    });

    // Output file
    try {
        if (!fs.existsSync('dist'))
            fs.mkdirSync('dist');
        fs.writeFile("dist/index.html", html, function (err) {
            if (err) {
                return console.log(err);
            }

            console.log("Saved file");

            fs.writeFile("dist/index.min.html", minHtml, function (err) {
                if (err) {
                    return console.log(err);
                }

                console.log("Saved minified file");

                fs.writeFile("dist/index.es5.html", htmlEs5, function (err) {
                    if (err) {
                        return console.log(err);
                    }

                    console.log("Saved es5 file");

                    if (errors === 0) {
                        console.log("--- BUILD FINISHED SUCCESSFULLY ---");
                    } else {
                        console.log("--- BUILD FINISHED WITH ERRORS ---");
                    }
                });
            });
        });
    } catch (e) {
        errors++;
        console.error("Could not save file.");

        if (errors === 0) {
            console.log("--- BUILD FINISHED SUCCESSFULLY ---");
        } else {
            console.error("--- BUILD FINISHED WITH ERRORS ---");
        }
    }
});