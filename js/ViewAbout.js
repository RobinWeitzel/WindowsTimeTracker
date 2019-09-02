TSFRepository.registerComponent(class ViewAbout extends TSFComponent {
    constructor() {
        super();
    }

    async openUrl(e, url) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.openUrl(url).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }
});