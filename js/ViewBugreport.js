TSFRepository.registerComponent(class ViewBugreport extends TSFComponent {
    constructor() {
        super();

        this.state.report = "";
    }

    sendReport(e) {
        const escapeHtml = unsafe => {
            return unsafe
                .replace(/&/g, "&amp;")
                .replace(/</g, "&lt;")
                .replace(/>/g, "&gt;")
                .replace(/"/g, "&quot;")
                .replace(/'/g, "&#039;")
                .replace(/\n/g, "%0A")
                .replace(/ /g, "%20");
        }

        if (CefSharp.blockReport !== true) {
            const xmlHttp = new XMLHttpRequest();
            /*xmlHttp.onreadystatechange = function() { 
                if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
                    callback(xmlHttp.responseText);
            }*/
            xmlHttp.open("GET", `https://hooks.zapier.com/hooks/catch/3466553/obv012p/?issue_body=${escapeHtml(this.state.report)}`, true); // true for asynchronous 
            xmlHttp.send(null);
        }
        this.state.report = "";
    }
});