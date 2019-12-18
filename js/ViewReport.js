TSF.Repository.registerComponent(class ViewReport extends TSF.Component {
    constructor() {
        super();

        this.counters = [0, 0, 0];
        this.state.scroll = 0;
        this.state.zoomLevel = 0;
        this.state.registerDomChangeListener("scroll", val => {
            const newVal = Math.max(0, Math.min(2, Math.floor(val / 250)));
            if(newVal !== this.state.zoomLevel)
                this.state.zoomLevel = newVal; 
        });

        this.state.registerDomChangeListener("zoomLevel", val => {
            this.loadNewData1();
        });

        this.state.comboboxActive = false;
        this.state.header = false;
        this.getActivities().then(activities => {
            this.state.activities = activities.map(b => {return {name: b, active: true}});
        });

        this.state.combobox = "";

        this.state.registerDomChangeListener('activities', val => {
            this.state.header = val.filter(a => a.name.startsWith(this.state.combobox) && !a.active).length === 0;
        });

        this.state.registerDomChangeListener('comboboxActive', val => {
            if(!val) { // Combobox is closed
                this.state.combobox = "";
                this.loadNewData1();
                this.loadNewData2();
                this.loadNewData3();
            }
        });

        this.state.registerDomChangeListener('combobox', val => {
            this.state.header = this.state.activities.filter(a => a.name.startsWith(val) && !a.active).length === 0;
        });
        
        document.addEventListener('click', e => {
            if(e.target.classList.contains("combobox")) {
                this.state.comboboxActive = true;
            } else if(e.target.matches('.combobox-item-container') || e.target.closest('.combobox-item-container') || !e.target.closest('body')) {
            } else {
                this.state.comboboxActive = false;
            }
        });
    }

    headerClick(e) {
        if (!this.state.header) {
            this.state.activities = this.state.activities.map(b => {
                if(b.name.startsWith(this.state.combobox))
                    return {name: b.name, active: true}
                else
                    return b;
            });
        } else {
            this.state.activities = this.state.activities.map(b => {
                if(b.name.startsWith(this.state.combobox))
                    return {name: b.name, active: false}
                else
                    return b;
            });
        }
    }

    rowClick(e, index) {
        if (e.target.nodeName === "DIV") {
            if (this.state.activities[index].active) {
                this.state.activities[index].active = false;
            } else {
                this.state.activities[index].active = true;
            }
        }
        else {
            this.state.activities[index].active = true;
        }
    }

    async getActivities() {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getActivities().then(result => {
                resolve(JSON.parse(result));
            });
        });
    }


    async getChart1Data(activities, start, end, zoomLevel) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getReportData1(activities.filter(a => a.active).map(a => a.name), start.toJSON(), end.toJSON(), zoomLevel, ++this.counters[0]).then(result => {
                result = JSON.parse(result);
                if(this.counters[0] !== result.counter)
                    reject();
                else
                    resolve(result.value);
            });
        });
    }

    async getChart2Data(activities, start, end) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getReportData2(activities.filter(a => a.active).map(a => a.name), start.toJSON(), end.toJSON(), ++this.counters[1]).then(result => {
                result = JSON.parse(result);
                if(this.counters[1] !== result.counter)
                    reject();
                else
                    resolve(result.value);
            });
        });
    }

    
    async getChart3Data(activities, start, end) {
        return new Promise(async (resolve, reject) => {
            if (typeof boundAsync === "undefined")
                await CefSharp.BindObjectAsync("boundAsync");

            boundAsync.getReportData3(activities.filter(a => a.active).map(a => a.name), start.toJSON(), end.toJSON(), ++this.counters[2]).then(result => {
                result = JSON.parse(result);
                if(this.counters[2] !== result.counter)
                    reject();
                else
                    resolve(result.value);
            });
        });
    }

    onShow() {
        if(!this.counters)
            this.counters = [0, 0, 0];
        this.chart1 = this.chart1 || new TimeCharts.Barchart("#report_chart1", {
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
                visible: true,
                interval: 2
            },
            minDistance: 50,
            max: 12,
            draggable: true,
            onScroll: e => {
                if(this.chart1.data.length > 0) {
                    const newVal = Math.min(Math.max(0, this.state.scroll + e.deltaY), 700);
                    this.state.scroll = newVal;
                }
            }
        });     

        this.chart2 = this.chart2 || new TimeCharts.Barchart("#report_chart2", {
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
                callback: (title, value) => `<span style="color: gray">${toTime(value)}</span>${title !== "" ? " - " + title : ""}`,
                visible: true
            },
            scale: {
                visible: false
            }
        });

        this.chart3 = this.chart3 || new TimeCharts.Piechart("#report_chart3", {
            data: [],
            padding: {
                top: 20,
                right: 20,
                bottom: 20,
                left: 20
            },
            donutFactor: 0.6,
            hover: {
                callback: (title, value) => `<span style="color: gray">${toTime(value)}</span>${title !== "" ? " - " + title : ""}`,
                visible: true
            },
        });

        if(!this.datepicker) {
            this.datepicker = new Lightpick({ 
                field: document.getElementById('report-datepicker'),
                singleDate: false,
                numberOfMonths: 2,
                selectForward: true,
                onSelect: (start, end) => {
                    this.state.start = start;
                    this.state.end = end;
                },
                onClose: () => {
                    this.loadNewData1();
                    this.loadNewData2();
                    this.loadNewData3();
                }
            });  

            let start = new Date();
            start.setMonth(start.getMonth()-1);
            this.datepicker.setDateRange(start, new Date());
        }

        
        if(this.state.activities !== "") {
            this.loadNewData1();
            this.loadNewData2();
            this.loadNewData3();
        }
    }

    loadNewData1() {
        if(this.state.zoomLevel === 2) {
            this.chart1.max = 31 * 12;
            this.chart1.scale.interval = 31 * 2;
        }
        else if(this.state.zoomLevel === 1) {
            this.chart1.max = 7 * 12;
            this.chart1.scale.interval = 7 * 2;
        }
        else  {
            this.chart1.max = 12;
            this.chart1.scale.interval = 2;
        }   
        this.chart1.setData([]);
        this.getChart1Data(this.state.activities, this.state.start, this.state.end, this.state.zoomLevel || 0).then(data => {                
            this.chart1.setData(data);
        }).catch(e => console.log("Old data"));
    }

    loadNewData2() {
        this.chart2.setData([]);
        this.getChart2Data(this.state.activities, this.state.start, this.state.end).then(data => {
            this.chart2.setData(data);
        }).catch(e => console.log("Old data"));
    }

    loadNewData3() {
        this.chart3.setData([]);
        this.getChart3Data(this.state.activities, this.state.start, this.state.end).then(data => {
            this.chart3.setData(data);
        }).catch(e => console.log("Old data"));
    }
});