TSFRepository.registerComponent(class ViewDaily extends TSFComponent {
    constructor() {
        super();

        this.display = 'block';
        this.counters = [
            0,
            0,
            0
        ]

        this.datePicker = new Lightpick({
            field: document.getElementById("day_picker_dummy_field"),
            inline: true,
            lang: 'en',
            onSelect: (date) => {
                this.setCenterDate(date.toDate());
                this.close();
            }
        });

        this.state.registerDomChangeListener('day', date => {
            this.setDays();

            if (this.dayChart) {
                this.dayChart.setData({ timelines: [] });
                this.getDayData(date.toJSON()).then(timelines => {
                    this.dayChart.setData({ timelines: timelines });
                }).catch(e => console.log("Old data"));
            }

            if (this.weekChart1) {
                this.weekChart1.setData([]);
                this.getWeekDataBreakdown(date.toJSON(), 10).then(data => {
                    this.weekChart1.setData(data);
                }).catch(e => console.log("Old data"));
            }

            if (this.weekChart2) {
                this.weekChart2.setData([]);
                this.getWeekDataSum(date.toJSON(), 7).then(data => {
                    this.weekChart2.setData(data);
                }).catch(e => console.log("Old data"));
            }
        });

        this.datePicker.setDate(moment());
        this.datePickerOpen = false;
    }

    async getDayData(date) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getDayData(date, ++this.counters[0]).then(result => {
                result = JSON.parse(result);
                if(result.counter === this.counters[0])
                    resolve(result.value);
                else
                    reject();
            });
        });
    }

    async getWeekDataBreakdown(date, day) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getWeekBreakdownData(date, day, ++this.counters[1]).then(result => {
                result = JSON.parse(result);
                if(result.counter === this.counters[1])
                    resolve(result.value);
                else
                    reject();
            });
        });
    }

    async getWeekDataSum(date, day) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getWeekSumData(date, day, ++this.counters[2]).then(result => {
                result = JSON.parse(result);
                if(result.counter === this.counters[2])
                    resolve(result.value);
                else
                    reject();
            });
        });
    }

    onShow() {
        const date = this.state.day;

        this.dayChart = this.dayChart || new TimeCharts.Timeline("#chart_daily", {
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
                distance: 15,
                textWidth: 160
            },
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            distance: 20,
            adjustSize: true,
            round: false
        });

        this.weekChart1 = this.weekChart1 || new TimeCharts.Barchart("#chart_weekly_1", {
            data: [],
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            distance: "variable",
            hover: {
                callback: (title, value) => `<span style="color: gray">${toTime(value) + (title !== "" ? " - " : "")}</span>${title}`
            },
            scale: {
                visible: false
            }
        });

        this.weekChart2 = this.weekChart2 || new TimeCharts.Barchart("#chart_weekly_2", {
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
            },
            scale: {
                visible: false
            }
        });

        if(date === "")
            return;

        if(!this.counters)
            this.counters = [0, 0, 0];

        this.dayChart.setData({ timelines: [] });
        this.getDayData(date.toJSON()).then(timelines => {
            this.dayChart.setData({ timelines: timelines });
        }).catch(e => console.log("Old data"));

        this.weekChart1.setData([]);
        this.getWeekDataBreakdown(date.toJSON(), 10).then(data => {
            this.weekChart1.setData(data);
        }).catch(e => console.log("Old data"));

        this.weekChart2.setData([]);
        this.getWeekDataSum(date.toJSON(), 7).then(data => {
            this.weekChart2.setData(data);
        }).catch(e => console.log("Old data"));
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

    shiftToday(e) {
        this.datePicker.setDate(moment());
    }
});