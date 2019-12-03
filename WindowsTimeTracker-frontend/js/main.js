// ----------------- General --------------------

let open_link = (url) => {
    //Todo - open link in C#
    window.open(url, '_blank');
}

const toTime = value => {
    const totalMinutes = Math.round(value * 60);
    const hours = Math.floor(totalMinutes / 60);
    const minutes = totalMinutes % 60;
	return `${hours}:${minutes < 10 ? "0" + minutes : minutes}`;
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

// ----------------- iFrame resizing hint -----
window.addEventListener("message", (event) => {
     if(TSFRepository && TSFRepository.classes.get("ViewDaily")){
          console.log(TSFRepository.classes.get("ViewDaily"));
      TSFRepository.classes.get("ViewDaily").resize();
     }
},false);