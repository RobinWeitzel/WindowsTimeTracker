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

TSFProxy.registerComparison('Moment', (a, b) => a._i === b._i);

TSFRepository.registerComponent(class ViewReport extends TSFComponent {
    constructor() {
        super();
    }
});

TSFRepository.registerComponent(class ViewEditData extends TSFComponent {
    constructor() {
        super();
    }
});

TSFRepository.registerComponent(class ViewBugReport extends TSFComponent {
    constructor() {
        super();
    }
});

TSFRepository.registerComponent(class ViewAbout extends TSFComponent {
    constructor() {
        super();
    }
});