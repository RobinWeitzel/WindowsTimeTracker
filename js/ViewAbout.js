TSF.Repository.registerComponent(class ViewAbout extends TSF.Component {
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