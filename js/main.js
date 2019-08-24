// ----------------- Variables --------------------

const keyMapping = {
    8: "backspace", //  backspace
    9: "tab", //  tab
    13: "enter", //  enter
    16: "shift", //  shift
    17: "ctrl", //  ctrl
    18: "alt", //  alt
    19: "pause/break", //  pause/break
    20: "caps lock", //  caps lock
    27: "escape", //  escape
    33: "page up", // page up, to avoid displaying alternate character and confusing people	         
    34: "page down", // page down
    35: "end", // end
    36: "home", // home
    37: "left arrow", // left arrow
    38: "up arrow", // up arrow
    39: "right arrow", // right arrow
    40: "down arrow", // down arrow
    45: "insert", // insert
    46: "delete", // delete
    91: "left window", // left window
    92: "right window", // right window
    93: "select key", // select key
    96: "numpad 0", // numpad 0
    97: "numpad 1", // numpad 1
    98: "numpad 2", // numpad 2
    99: "numpad 3", // numpad 3
    100: "numpad 4", // numpad 4
    101: "numpad 5", // numpad 5
    102: "numpad 6", // numpad 6
    103: "numpad 7", // numpad 7
    104: "numpad 8", // numpad 8
    105: "numpad 9", // numpad 9
    106: "multiply", // multiply
    107: "add", // add
    109: "subtract", // subtract
    110: "decimal point", // decimal point
    111: "divide", // divide
    112: "F1", // F1
    113: "F2", // F2
    114: "F3", // F3
    115: "F4", // F4
    116: "F5", // F5
    117: "F6", // F6
    118: "F7", // F7
    119: "F8", // F8
    120: "F9", // F9
    121: "F10", // F10
    122: "F11", // F11
    123: "F12", // F12
    144: "num lock", // num lock
    145: "scroll lock", // scroll lock
    186: ";", // semi-colon
    187: "=", // equal-sign
    188: ",", // comma
    189: "-", // dash
    190: ".", // period
    191: "/", // forward slash
    192: "`", // grave accent
    219: "[", // open bracket
    220: "\\", // back slash
    221: "]", // close bracket
    222: "'" // single quote
};

// ----------------- General --------------------

let open_link = (url) => {
    //Todo - open link in C#
    window.open(url, '_blank');
}

const toTime = value => {
	return Math.round(value * 100) / 100 + "h";
}

// ----------------- Menu --------------------

let toggle_menu = () => {
    document.getElementById("menu").classList.toggle("hide");
}

let expand_menu = () => {
    document.getElementById("menu").classList.remove("hide");
}

let unexpand_menu = () => {
    document.getElementById("menu").classList.add("hide");
}

// ----------------- Views --------------------
SimpleViewRepository.registerClass(class ViewDaily extends SimpleView {
    constructor() {
        super();

        this.display = 'block';

        this.datePicker = new Lightpick({
            field: document.getElementById("day_picker_dummy_field"),
            inline: true,
            lang: 'en',
            onSelect: (date) => {
                this.setCenterDate(date.toDate());
                this.close();
            }
        });

        this.state.attachDomChange('day', date => {
            this.setDays();

            if (this.dayChart) {
                this.dayChart.setData({ timelines: [] });
                this.getDayData(date.toJSON()).then(timelines => {
                    this.dayChart.setData({ timelines: timelines });
                });
            }

            if (this.weekChart1) {
                this.weekChart1.setData([]);
                this.getWeekDataBreakdown(date.toJSON(), 7).then(data => {
                    this.weekChart1.setData(data);
                });
            }

            if (this.weekChart2) {
                this.weekChart2.setData([]);
                this.getWeekDataSum(date.toJSON(), 7).then(data => {
                    this.weekChart2.setData(data);
                });
            }
        });

        this.datePicker.setDate(moment());
        this.datePickerOpen = false;
    }

    async getDayData(date) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getDayData(date).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }


    async getWeekDataBreakdown(date, day) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getWeekBreakdownData(date, day).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    async getWeekDataSum(date, day) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getWeekSumData(date, day).then(result => {
                resolve(JSON.parse(result));
            });
        });
    }

    onShow() {
        const date = this.state.day;

        this.dayChart = new TimeCharts.Timeline("#chart_daily", {
            scale: {
                from: 0 * 60,
                to: 24 * 60,
                intervalStart: 0
            },
            data: {
                timelines: [],
            },
            legend: {
                visible: true,
                distance: 15
            },
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            distance: 20,
            adjustSize: true,
        });

        this.weekChart1 = new TimeCharts.Barchart("#chart_weekly_1", {
            data: [],
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            distance: 20,
            hover: {
                callback: (title, value) => `<span style="color: gray">${toTime(value)}</span>${title !== "" ? " - " + title : ""}`
            }
        });

        this.weekChart2 = new TimeCharts.Barchart("#chart_weekly_2", {
            data: [],
            orientation: "horizontal",
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            distance: 20,
            hover: {
                callback: (title, value) => `<span style="color: gray">${toTime(value)}</span>${title !== "" ? " - " + title : ""}`
            }
        });

        this.dayChart.setData({ timelines: [] });
        this.getDayData(date.toJSON()).then(timelines => {
            this.dayChart.setData({ timelines: timelines });
        });

        this.weekChart1.setData([]);
        this.getWeekDataBreakdown(date.toJSON(), 7).then(data => {
            this.weekChart1.setData(data);
        });

        this.weekChart2.setData([]);
        this.getWeekDataSum(date.toJSON(), 7).then(data => {
            this.weekChart2.setData(data);
        });
    }

    open() {
        document.getElementById("day_picker_dummy_container").style.display = "inline";
        this.datePickerOpen = true;
        document.body.addEventListener("click", this.close)
    }

    close() {
        document.body.removeEventListener("click", this.close)
        document.getElementById("day_picker_dummy_container").style.display = "none";
        this.datePickerOpen = false;
    }

    setCenterDate(date) {
        date = moment(date);
        this.state.day = date;

        this.close();
    }

    dayClick(e, day) {
        e.stopPropagation();
        if (day.isSame(this.state.day)) { // center date clicked
            this.open();
        } else {
            this.datePicker.setDate(day);
        }
    }

    dayRender(day) {
        return `<div class="number">${day.format('Do')}</div>
                <div class="month">${day.format("MMMM 'YY")}</div>`;
    }

    setDays() {
        this.state.days = [
            this.state.day.clone().subtract(2, "days"),
            this.state.day.clone().subtract(1, "days"),
            this.state.day.clone(),
            this.state.day.clone().add(1, "days"),
            this.state.day.clone().add(2, "days"),
        ]
    }

    shiftDays(e, days) {
        this.datePicker.setDate(this.state.day.add(days, 'days'));
    }

    shiftMonths(e, months) {
        this.datePicker.setDate(this.state.day.add(months, 'months'));
    }
});

SimpleViewRepository.registerClass(class ViewReport extends SimpleView {
    constructor() {
        super();
    }
});

SimpleViewRepository.registerClass(class ViewSettings extends SimpleView {
    constructor() {
        super();

        this.display = 'block';
        this.state.darkMode = false;
        this.state.timeVisibleOnScreen = 10;
        this.state.playNotificationSound = false;
        this.state.timeUntilAskedAgain = 30;
        this.state.timeout = 30;
        this.state.hotkeyEnabled = false;
        this.state.hotkeys = "";

        this.keys = [];
        this.keysRendered = [];
    }

    darkModeClick() {
        this.state.darkMode = !this.state.darkMode;
        document.documentElement.setAttribute("dark_mode", this.state.darkMode);
    }

    playNotificationSoundClick() {
        this.state.playNotificationSound = !this.state.playNotificationSound;
    }

    hotkeyEnabledClick() {
        this.state.hotkeyEnabled = !this.state.hotkeyEnabled;
    }

    hotkey_on_key_down(event) {
        event.preventDefault();
        const char = event.which || event.keyCode;

        if (this.keys.length === 0) { // new hotkey input
            this.keys = [];
            this.keysRendered = [];
        }

        const i = this.keys.indexOf(char);

        if (i < 0) {
            this.keys.push(char);
            this.keysRendered.push(char);
        }

        this.hotkey_render_output();
    }

    hotkey_on_key_up(event) {
        event.preventDefault();
        const char = event.which || event.keyCode;

        const i = this.keys.indexOf(char);

        if (i >= 0) {
            this.keys.splice(i, 1);
        }
    }

    hotkey_on_key_press(event) {
        return false;
    }

    hotkey_render_output() {
        let value = [];

        for (const key of this.keysRendered) {
            value.push(keyMapping[key] || String.fromCharCode(key));
        }

        this.state.hotkeys = value.join(" + ");
    }
});

SimpleViewRepository.registerClass(class ViewTackingSettings extends SimpleView {
    constructor() {
        super();
    }
});

SimpleViewRepository.registerClass(class ViewEditData extends SimpleView {
    constructor() {
        super();
    }
});

SimpleViewRepository.registerClass(class ViewBugReport extends SimpleView {
    constructor() {
        super();
    }
});

SimpleViewRepository.registerClass(class ViewAbout extends SimpleView {
    constructor() {
        super();
    }
});


SimpleViewRepository.afterLoad = () => SimpleViewRepository.navigate('ViewDaily');