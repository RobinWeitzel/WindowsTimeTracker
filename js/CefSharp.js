const CefSharp = {
    async BindObjectAsync() {
        return new Promise((resolve, reject) => {
            resolve();
        });
    },
    blockReport: true
}

class boundAsync {
    static settings = {
        timeNotificationVisible: 10,
        playNotificationSound: false,
        timeBeforeAskingAgain: 30,
        timeSinceAppLastUsed: 5,
        offlineTracking: true,
        hotkeyDisabled: false,
        hotkeys: ["A0", "9"]
    };

    static trackingSettings = {
        blacklist: [
            "Explorer",
            "TimeTracker",
            "Cortana",
            "Start",
            "Window Dialog"
        ]
    };

    /*** GETTER ***/
    static async getDayData(date, id) {
        return new Promise((resolve, reject) => {
            const data = { "value": [{ "label": "Work", "values": [{ "start": 452.0, "length": 1.0, "title": "Project 1" }, { "start": 460.0, "length": 1.0, "title": "Project 2" }, { "start": 461.0, "length": 14.0, "title": "Project 1" }, { "start": 475.0, "length": 32.0, "title": "Project 2" }, { "start": 509.0, "length": 21.0, "title": "Project 3" }, { "start": 552.0, "length": 5.0, "title": "Project 3" }, { "start": 787.0, "length": 27.0, "title": "Project 2" }, { "start": 849.0, "length": 8.0, "title": "Project 2" }, { "start": 870.0, "length": 120.0, "title": "Project 2" }], "colors": ["#1db8fc", "#81d8fd", "#1db8fc", "#81d8fd", "#4fc8fc", "#4fc8fc", "#81d8fd", "#81d8fd", "#81d8fd"] }, { "label": "MA", "values": [{ "start": 453.0, "length": 7.0, "title": "Hardware" }], "colors": ["#ff8080"] }, { "label": "Projekte", "values": [{ "start": 557.0, "length": 28.0, "title": "Mindmap" }], "colors": ["#af90ee"] }, { "label": "Uni", "values": [{ "start": 593.0, "length": 16.0, "title": "Course 1" }, { "start": 610.0, "length": 26.0, "title": "Course 1" }, { "start": 635.0, "length": 1.0, "title": "Course 1" }, { "start": 636.0, "length": 70.0, "title": "Course 1" }, { "start": 706.0, "length": 9.0, "title": "Course 1" }, { "start": 783.0, "length": 4.0, "title": "Course 1" }], "colors": ["#8084ff", "#8084ff", "#8084ff", "#8084ff", "#8084ff", "#8084ff"] }], "counter": id };
            resolve(JSON.stringify(data));
        });
    }

    static async getWeekBreakdownData(date, day, id) {
        return new Promise((resolve, reject) => {
            const data = { "value": [{ "label": "Tu", "datasets": [{ "title": "MA", "value": 3.91, "color": "#ff5858" }, { "title": "Uni", "value": 0.13, "color": "#5e64ff" }] }, { "label": "We", "datasets": [{ "title": "Work", "value": 2.0999999999999996, "color": "#7cd6fd" }, { "title": "MA", "value": 0.18, "color": "#ff5858" }] }, { "label": "Th", "datasets": [{ "title": "Work", "value": 0.78, "color": "#7cd6fd" }, { "title": "MA", "value": 0.24, "color": "#ff5858" }] }, { "label": "Fr", "datasets": [{ "title": "Work", "value": 1.35, "color": "#7cd6fd" }, { "title": "MA", "value": 0.57000000000000006, "color": "#ff5858" }, { "title": "Uni", "value": 2.16, "color": "#5e64ff" }] }, { "label": "Sa", "datasets": [] }, { "label": "Su", "datasets": [{ "title": "Work", "value": 1.43, "color": "#7cd6fd" }] }, { "label": "Mo", "datasets": [{ "title": "Work", "value": 2.36, "color": "#7cd6fd" }, { "title": "Uni", "value": 1.6, "color": "#5e64ff" }] }, { "label": "Tu", "datasets": [{ "title": "Work", "value": 4.41, "color": "#7cd6fd" }] }, { "label": "We", "datasets": [{ "title": "Work", "value": 4.99, "color": "#7cd6fd" }, { "title": "MA", "value": 1.68, "color": "#ff5858" }] }, { "label": "Th", "datasets": [{ "title": "Work", "value": 7.3199999999999994, "color": "#7cd6fd" }] }, { "label": "Fr", "datasets": [{ "title": "Work", "value": 3.79, "color": "#7cd6fd" }, { "title": "MA", "value": 0.12, "color": "#ff5858" }, { "title": "Projekte", "value": 0.47, "color": "#743ee2" }, { "title": "Uni", "value": 2.09, "color": "#5e64ff" }] }], "counter": id };
            resolve(JSON.stringify(data));
        });
    }

    static async getWeekSumData(date, day, id) {
        return new Promise((resolve, reject) => {
            const data = { "value": [{ "label": "Work", "datasets": [{ "title": "Project 1", "value": 7.12, "color": "#1db8fc" }, { "title": "Project 3", "value": 0.42000000000000004, "color": "#4fc8fc" }, { "title": "Project 2", "value": 15.399999999999999, "color": "#81d8fd" }] }, { "label": "MA", "datasets": [{ "title": "Hardware", "value": 1.7999999999999998, "color": "#ff8080" }] }, { "label": "Projekte", "datasets": [{ "title": "Mindmap", "value": 0.47, "color": "#af90ee" }] }, { "label": "Uni", "datasets": [{ "title": "Course 1", "value": 3.69, "color": "#8084ff" }] }], "counter": id };
            resolve(JSON.stringify(data));
        });
    }

    static async getReportData1(activities, start, end, zoomLevel, id) {
        return new Promise((resolve, reject) => {
            const data = { "value": [{ "label": "16.10", "datasets": [{ "title": "Privat", "value": 2.89, "color": "#feef72" }, { "title": "Projekte", "value": 2.37, "color": "#743ee2" }] }, { "label": "17.10", "datasets": [{ "title": "Privat", "value": 1.3299999999999998, "color": "#feef72" }] }, { "label": "18.10", "datasets": [{ "title": "Privat", "value": 0.57, "color": "#feef72" }, { "title": "Projekte", "value": 1.34, "color": "#743ee2" }] }, { "label": "19.10", "datasets": [{ "title": "Projekte", "value": 3.52, "color": "#743ee2" }] }, { "label": "20.10", "datasets": [{ "title": "Projekte", "value": 2.35, "color": "#743ee2" }] }, { "label": "21.10", "datasets": [{ "title": "Privat", "value": 2.29, "color": "#feef72" }, { "title": "Projekte", "value": 0.84000000000000008, "color": "#743ee2" }, { "title": "Uni", "value": 1.65, "color": "#5e64ff" }] }, { "label": "22.10", "datasets": [] }, { "label": "23.10", "datasets": [{ "title": "Work 2", "value": 0.2, "color": "#ffa00a" }, { "title": "Projekte", "value": 0.35, "color": "#743ee2" }] }, { "label": "24.10", "datasets": [{ "title": "Work 2", "value": 4.54, "color": "#ffa00a" }] }, { "label": "25.10", "datasets": [{ "title": "Work 2", "value": 0.02, "color": "#ffa00a" }, { "title": "MA", "value": 6.129999999999999, "color": "#ff5858" }] }, { "label": "26.10", "datasets": [] }, { "label": "27.10", "datasets": [] }, { "label": "28.10", "datasets": [{ "title": "Work", "value": 0.24, "color": "#7cd6fd" }, { "title": "MA", "value": 0.87000000000000011, "color": "#ff5858" }, { "title": "Projekte", "value": 0.25, "color": "#743ee2" }, { "title": "Uni", "value": 1.6300000000000001, "color": "#5e64ff" }] }, { "label": "29.10", "datasets": [{ "title": "Work", "value": 0.18, "color": "#7cd6fd" }, { "title": "MA", "value": 4.42, "color": "#ff5858" }] }, { "label": "30.10", "datasets": [] }, { "label": "31.10", "datasets": [{ "title": "MA", "value": 1.75, "color": "#ff5858" }, { "title": "Projekte", "value": 3.0199999999999996, "color": "#743ee2" }] }, { "label": "01.11", "datasets": [{ "title": "MA", "value": 3.93, "color": "#ff5858" }] }, { "label": "02.11", "datasets": [] }, { "label": "03.11", "datasets": [] }, { "label": "04.11", "datasets": [{ "title": "Work 2", "value": 0.52, "color": "#ffa00a" }, { "title": "MA", "value": 3.17, "color": "#ff5858" }, { "title": "Uni", "value": 1.65, "color": "#5e64ff" }] }, { "label": "05.11", "datasets": [{ "title": "MA", "value": 3.91, "color": "#ff5858" }, { "title": "Uni", "value": 0.13, "color": "#5e64ff" }] }, { "label": "06.11", "datasets": [{ "title": "Work", "value": 2.0999999999999996, "color": "#7cd6fd" }, { "title": "MA", "value": 0.18, "color": "#ff5858" }] }, { "label": "07.11", "datasets": [{ "title": "Work", "value": 0.78, "color": "#7cd6fd" }, { "title": "MA", "value": 0.24, "color": "#ff5858" }] }, { "label": "08.11", "datasets": [{ "title": "Work", "value": 1.35, "color": "#7cd6fd" }, { "title": "MA", "value": 0.57000000000000006, "color": "#ff5858" }, { "title": "Uni", "value": 2.16, "color": "#5e64ff" }] }, { "label": "09.11", "datasets": [] }, { "label": "10.11", "datasets": [{ "title": "Work", "value": 1.43, "color": "#7cd6fd" }] }, { "label": "11.11", "datasets": [{ "title": "Work", "value": 2.36, "color": "#7cd6fd" }, { "title": "Uni", "value": 1.6, "color": "#5e64ff" }] }, { "label": "12.11", "datasets": [{ "title": "Work", "value": 4.41, "color": "#7cd6fd" }] }, { "label": "13.11", "datasets": [{ "title": "Work", "value": 4.99, "color": "#7cd6fd" }, { "title": "MA", "value": 1.68, "color": "#ff5858" }] }, { "label": "14.11", "datasets": [{ "title": "Work", "value": 7.3199999999999994, "color": "#7cd6fd" }] }, { "label": "15.11", "datasets": [{ "title": "Work", "value": 3.79, "color": "#7cd6fd" }, { "title": "MA", "value": 0.12, "color": "#ff5858" }, { "title": "Projekte", "value": 0.47, "color": "#743ee2" }, { "title": "Uni", "value": 2.09, "color": "#5e64ff" }] }, { "label": "16.11", "datasets": [{ "title": "Work", "value": 0.06, "color": "#7cd6fd" }] }], "counter": id };
            resolve(JSON.stringify(data));
        });
    }

    static async getReportData2(activities, start, end, id) {
        return new Promise((resolve, reject) => {
            const data = { "value": [{ "label": "Work 2", "datasets": [{ "title": "Help", "value": 0.52, "color": "#ffce80" }, { "title": "Project 1", "value": 4.76, "color": "#ffba4d" }] }, { "label": "Work", "datasets": [{ "title": "Project 1", "value": 7.48, "color": "#1db8fc" }, { "title": "Project 3", "value": 0.42000000000000004, "color": "#4fc8fc" }, { "title": "Project 2", "value": 20.660000000000004, "color": "#81d8fd" }, { "title": "Zeiterfassung", "value": 0.42, "color": "#039fe2" }] }, { "label": "MA", "datasets": [{ "title": "Hardware", "value": 8.5199999999999978, "color": "#ff8080" }, { "title": "Research", "value": 8.53, "color": "#ff1a1a" }, { "title": "Simulation", "value": 9.92, "color": "#ff4d4d" }] }, { "label": "Privat", "datasets": [{ "title": "Job Search", "value": 4.1899999999999995, "color": "#fef180" }] }, { "label": "Projekte", "datasets": [{ "title": "Firefly", "value": 3.0199999999999996, "color": "#8f64e8" }, { "title": "Mindmap", "value": 0.47, "color": "#af90ee" }, { "title": "Recipe Website", "value": 7.26, "color": "#af90ee" }, { "title": "Robofish", "value": 0.25, "color": "#6f37e1" }, { "title": "TimeTracker", "value": 1.1400000000000001, "color": "#561ec8" }] }, { "label": "Uni", "datasets": [{ "title": "Course 1", "value": 10.909999999999998, "color": "#8084ff" }] }], "counter": id };
            resolve(JSON.stringify(data));
        });
    }

    static async getReportData3(activities, start, end, id) {
        return new Promise((resolve, reject) => {
            const data = {
                value: [
                    {
                        value: 0.25,
                        label: "Test"
                    },
                    {
                        value: 0.5,
                        label: "Test2"
                    },
                    {
                        value: 0.25,
                        label: "Test3"
                    }
                ],
                counter: id
            };
            resolve(JSON.stringify(data));
        });
    }

    static async getTrackingSettings() {
        return new Promise((resolve, reject) => {
            resolve(JSON.stringify(boundAsync.trackingSettings));
        });
    }

    static async getSettings() {
        return new Promise((resolve, reject) => {
            resolve(JSON.stringify(boundAsync.settings));
        });
    }

    static async getActivities() {
        return new Promise((resolve, reject) => {
            const data = [];
            resolve(JSON.stringify(data));
        });
    }

    /*** SETTER ***/
    static async setTimeNotificationVisible(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.timeNotificationVisible = val;
            resolve(JSON.stringify(boundAsync.settings.timeNotificationVisible));
        });
    }

    static async setPlayNotificationSound(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.playNotificationSound = val;
            resolve(JSON.stringify(boundAsync.settings.playNotificationSound));
        });
    }

    static async setTimeBeforeAskingAgain(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.timeBeforeAskingAgain = val;
            resolve(JSON.stringify(boundAsync.settings.timeBeforeAskingAgain));
        });
    }

    static async setTimeSinceAppLastUsed(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.timeSinceAppLastUsed = val;
            resolve(JSON.stringify(boundAsync.settings.timeSinceAppLastUsed));
        });
    }

    static async setOfflineTracking(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.offlineTracking = val;
            resolve(JSON.stringify(boundAsync.settings.offlineTracking));
        });
    }

    static async setHotkeyDisabled(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.hotkeyDisabled = val;
            resolve(JSON.stringify(boundAsync.settings.hotkeyDisabled));
        });
    }

    static async setHotkeys(val) {
        return new Promise((resolve, reject) => {
            boundAsync.settings.hotkeys = val;
            resolve(JSON.stringify(boundAsync.settings.hotkeys));
        });
    }

    static async setBlacklist(val) {
        return new Promise((resolve, reject) => {
            boundAsync.trackingSettings.blacklist = val;
            resolve(JSON.stringify(boundAsync.trackingSettings.blacklist));
        });
    }

    /*** OTHERS ***/
    static async openUrl(url) {
        return new Promise((resolve, reject) => {
            const win = window.open(url, '_blank');
            win.focus();
        });
    }
}
