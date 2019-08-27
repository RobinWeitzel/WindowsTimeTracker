TSFRepository.registerComponent(class ViewTrackingSettings extends TSFComponent {
    constructor() {
        super();

        this.display = 'block';
        
        this.getSettings().then(settings => {
            this.state.offlineTracking = settings.offlineTracking;
            this.state.blacklist = settings.blacklist;
        });
    }

    async getSettings() {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getTrackingSettings().then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    async setSettings() {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.setTrackingSettings({
                OfflineTracking: this.state.offlineTracking,
                Blacklist: this.state.blacklist.map(item => item) 
            }).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    offlineTrackingClick() {
        this.state.offlineTracking = !this.state.offlineTracking;
    }

    deleteItem(e, item) {
        const i = this.state.blacklist.indexOf(item);

        if(i >= 0) {
            this.state.blacklist.splice(i, 1);
        }
    }

    blacklistInput(e, index) {
        const value = e.path[0].value;
        this.state.blacklist[index] = value;
        const input = document.getElementById('setting_ignore_app_container').children[index].querySelector('input');
        input.focus();
        input.setSelectionRange(value.length, value.length);
    }

    addItem(e) {
        this.state.blacklist.push("");
    }
});