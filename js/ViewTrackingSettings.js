TSF.Repository.registerComponent(class ViewTrackingSettings extends TSF.Component {
    constructor() {
        super();

        this.display = 'block';

        this.getSettings().then(settings => {
            this.state.blacklist = settings.blacklist.map(b => {return {name: b, active: false}});
        });
        this.state.newEntry = "";
        this.state.deletePossible = false;
        this.state.header = false;
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

    async setBlacklist(blacklist) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.setBlacklist(blacklist).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    offlineTrackingClick() {
        this.state.offlineTracking = !this.state.offlineTracking;
    }

    add() {
        this.setBlacklist([...this.state.blacklist.map(item => item.name), this.state.newEntry]).then(result => {
            this.state.blacklist = result.map(b => {return {name: b, active: false}});
            this.state.newEntry = "";
            document.querySelector('#blacklist-header > tr').classList.remove('active');
        });
    }

    delete() {
        const blacklist = this.state.blacklist.filter(b => !b.active).map(b => b.name);

        this.setBlacklist(blacklist).then(result => {
            this.state.blacklist = result.map(b => {return {name: b, active: false}});
            this.state.deletePossible = false;
            document.querySelector('#blacklist-header > tr').classList.remove('active');
        });
    }

    restore() {
        const blacklist = [
            "TimeTracker",
            "Neue Benachrichtigung",
            "Explorer",
            "Cortana",
            "Akkuinformationen",
            "Start",
            "UnlockingWindow",
            "Status",
            "Aktive Anwendungen",
            "Window Dialog",
            "Info-Center",
            "Windows-Standardsperrbildschirm",
            "Host für die Windows Shell-Oberfläche",
            "F12PopupWindow",
            "LockingWindow",
            "CTX_RX_SYSTRAY",
            "[]"
        ];

        this.setBlacklist(blacklist).then(result => {
            this.state.blacklist = result.map(b => {return {name: b, active: false}});
            this.state.deletePossible = false;
            this.state.header = false;
        });
    }

    headerClick(e) {
        this.state.header = !this.state.header;

        if (this.state.header) {
            this.state.blacklist = this.state.blacklist.map(b => {return {name: b.name, active: true}});
            this.state.deletePossible = true;
        } else {
            this.state.blacklist = this.state.blacklist.map(b => {return {name: b.name, active: false}});
            this.state.deletePossible = false;
        }
    }

    rowClick(e, index) {
        if (e.target.nodeName === "DIV") {
            if (this.state.blacklist[index].active) {
                this.state.blacklist[index].active = false;
                this.state.header = false;
                this.state.deletePossible = this.state.blacklist.filter(b => b.active).length > 0;
            } else {
                this.state.blacklist[index].active = true;
                this.state.deletePossible = true;
            }
        }
        else {
            this.state.blacklist[index].active = true;
            this.state.deletePossible = true;
        }
    }
});