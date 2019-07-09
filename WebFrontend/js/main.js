
/* Constants */

let current_view;
let VIEW_DAILY;
let VIEW_WEEKLY;
let VIEW_SETTINGS;
let VIEW_TRACKING_SETTINGS;
let VIEW_EDIT_DATA;

// ----------------- General --------------------

let open_link = (url) => {
	//Todo - open link in C#
	window.open(url, '_blank');
}

// ----------------- Menu --------------------

let toggle_menu = () => {
	document.getElementById("menu").classList.toggle("hide");
}

let expand_menu = () => {
	console.log("expanding")
	document.getElementById("menu").classList.remove("hide");
}

let unexpand_menu = () => {
	console.log("unexpand menu")
	document.getElementById("menu").classList.add("hide");
}


// ----------------- View/Window Management --------------------

class View {
	constructor(options) {
		this.view_id = options.view_id;
		this.button_id = options.button_id;
		this.components = options.components;

		this.view = document.getElementById(this.view_id);
		this.button = document.getElementById(this.button_id);
	}

	select_view = () => {
		if(this === current_view)
			return;

		if(current_view !== undefined)
			current_view.hide()

		this.show()
		current_view = this;
	}

	/* Show and hide content panel*/
	show = () => {
		this.view.classList.add("show_view")
		this.button.classList.add("selected");
	}
	hide = () => {
		this.view.classList.remove("show_view")
		this.button.classList.remove("selected");
	}
}

// ----------------- Overrides for niceness sake --------------------

Date.prototype.nth = getOrdinalNum = (n) => {
	return n + (n > 0 ? ['th', 'st', 'nd', 'rd'][(n > 3 && n < 21) || n % 10 > 3 ? 0 : n % 10] : '');
}

Date.prototype.add_days = function(days) {
	var date = new Date(this.valueOf());
	date.setDate(date.getDate() + days);
	return date;
}

Date.prototype.add_months = function(months) {
	var date = new Date(this.valueOf());
	date.setMonth(date.getMonth() + months);
	return date;
}

// ----------------- Day Picker --------------------

class DayPickerDay {
	constructor(date, parent) {
		this.date = date;
		this.parent = parent;

		this.div = document.createElement("div");
		this.div.className = "day";
		this.div.parentClassElement = this;
		
		this.div_number = document.createElement("div");
		this.div_number.textContent = getOrdinalNum(date.getDate());
		this.div_number.className = "number"

		this.div_month = document.createElement("div");
		this.div_month.textContent = date.toLocaleString('en-us', { month: 'long' }) + " '" + (date.getFullYear()+"").slice(2);
		this.div_month.className = "month"

		this.div.appendChild(this.div_number);
		this.div.appendChild(this.div_month);
		
		//Overriden if center
		this.div.onclick = () => {
			this.parent.set_center_date(date);
		}
	}

	set_selected = () => {
		this.div.classList.add("selected");
		this.div.onclick = (e) => {

			//Stop container from closing straight away
			e.stopPropagation();
			
			if(!this.parent.datepicker_open) {
				this.parent.open() 
			}
			else {
				this.parent.close() 
			}

		}
	}

	set_unselected = () => {
		this.div.classList.remove("selected");
	}
}

class DayPicker {
	constructor(options) {

		//General
		this.date = options.date;

		//Element ids
		//Navigation & Co
		this.days_container_id = options.days_container_id;
		this.button_back_days_id = options.button_back_days_id;
		this.button_forward_days_id = options.button_forward_days_id;
		this.button_back_months_id = options.button_back_months_id;
		this.button_forward_months_id = options.button_forward_months_id;
		//Datepicker
		this.datepicker_container_id = options.datepicker_container_id;
		this.button_to_today_id = options.button_to_today_id;

		//Get components
		this.div_days_container = document.getElementById(this.days_container_id);
		this.button_back_days = document.getElementById(this.button_back_days_id)
		this.button_forward_days = document.getElementById(this.button_forward_days_id)
		this.button_back_months = document.getElementById(this.button_back_months_id)
		this.button_forward_months = document.getElementById(this.button_forward_months_id)

		this.datepicker_container = document.getElementById(this.datepicker_container_id)
		this.button_to_today = document.getElementById(this.button_to_today_id)

		//Days visible
		this.day_picker_dates = [];

		//Datepicker creation
		this.date_picker = new Lightpick({
			field: document.getElementById("day_picker_dummy_field"),
			inline: true,
			onSelect: (date) => {
				this.set_center_date(date.toDate())
				this.close()
			}
		});
		this.date_picker.setDate(new Date())
		this.date_picker_open = false;

		this.datepicker_container.onclick = (e) => {
			e.stopPropagation();
		}

		//Listeners
		this._add_button_listeners();

		//Init
		this.set_center_date(this.date)
	}

	_add_button_listeners = () => {
		this.button_to_today.onclick = () => {
			this.set_center_date(new Date())
		}
		this.button_back_days.onclick = () => {
			this.shift_days(-5);
		}
		this.button_forward_days.onclick = () => {
			this.shift_days(+5);
		}
		this.button_back_months.onclick = () => {
			this.shift_months(-1)
		}
		this.button_forward_months.onclick = () => {
			this.shift_months(+1)
		}
	}

	_add_date_components = () => {
		this.day_picker_dates = [];
		let tmp_date = new Date(this.date)
		tmp_date.setDate(this.date.getDate() - 2);
		for(let i = 0; i < 5; i++) {
			let day_picker = new DayPickerDay(new Date(tmp_date), this);
			this.day_picker_dates.push(day_picker)
			this.div_days_container.appendChild(day_picker.div)
			tmp_date.setDate(tmp_date.getDate() + 1);
		}
	}

	_remove_all = () => {
		while (this.div_days_container.firstChild) {
			this.div_days_container.firstChild.remove();
		}
	}

	open = () => {
		document.getElementById("day_picker_dummy_container").style.display = "inline";
		this.datepicker_open = true;
		document.body.addEventListener("click", this.close)
	}

	close = () => {
		document.body.removeEventListener("click", this.close)
		document.getElementById("day_picker_dummy_container").style.display = "none";
		this.datepicker_open = false;
	}

	get_center_div = () => {
		return this.day_picker_dates[parseInt(Math.floor(this.day_picker_dates/2))]
	}

	set_center_date = (date) => {

		this.date = date;
		this._remove_all();
		this._add_date_components();

		let center_day_picker = this.day_picker_dates.filter(day_picker => day_picker.date.getDate() === this.date.getDate())[0];
          center_day_picker.set_selected();

		this.close();
	}

	shift_days = (amount_of_days) => {
		this.set_center_date(this.date.add_days(amount_of_days));
	}

	shift_months = (amount_of_months) => {
		this.set_center_date(this.date.add_months(amount_of_months));
	}

}

let day_picker;
date_to_day_of_year = (date) => {
	var start = new Date(date.getFullYear(), 0, 0);
	var diff = (date - start) + ((start.getTimezoneOffset() - date.getTimezoneOffset()) * 60 * 1000);
	var day = Math.floor(diff / (1000 * 60 * 60 * 24));
	return day;
}

// ----------------- Settings --------------------

let dark_mode = (status) => {
	document.getElementById("setting_dark_mode").setAttribute("status", status);
	document.documentElement.setAttribute("dark_mode", status)
}

// ----------------- Edit Data --------------------

const data = [
	['Google', 100, 100, 100, 100,  1998, 807.80],
	['Apple',  100, 100, 100, 100, 1976, 116.52],
	['Yahoo',  100, 100, 100, 100, 1994, 38.66],
	['Google', 100, 100, 100, 100,  1998, 807.80],
	['Apple',  100, 100, 100, 100, 1976, 116.52],
	['Yahoo',  100, 100, 100, 100, 1994, 38.66],
	['Google', 100, 100, 100, 100,  1998, 807.80],
	['Apple',  100, 100, 100, 100, 1976, 116.52],
	['Yahoo',  100, 100, 100, 100, 1994, 38.66],
	['Google', 100, 100, 100, 100,  1998, 807.80],
	['Apple',  100, 100, 100, 100, 1976, 116.52],
	['Yahoo',  100, 100, 100, 100, 1994, 38.66],
];

// ----------------- Report --------------------

class TagBar {
	constructor(options) {			
		
		this.options = options.options;
		this.selected = options.selected || [];
		
		this.tag_container_id = options.tag_container_id;
		this.div = document.getElementById(this.tag_container_id);

		this.input_id = options.input_id;
		this.div_input = document.getElementById(this.input_id);

		this._add_input_listeners();
		this._make_datalist();
		this._set_datalist_options(this.options);
	}

	_add_input_listeners = () => {
		let last_non_matching = null;
		this.div_input.onkeyup = (e) => {
			/* this works because when appending from the datalist via enter, no key is 
			pressed even tho the keyup is triggered - dont ask me why */
			if(e.keyCode === undefined) { 
				this.div_input.value = last_non_matching;
			}
		}

		this.div_input.oninput = (e) => {
			if(this.options.includes(e.srcElement.value)) {
				this.select_tag(e.srcElement.value);
			}else {
				last_non_matching = e.srcElement.value;
			}
		}
	}

	_make_datalist = () => {
		this.datalist = document.createElement("datalist")
		this.datalist.id = this.tag_container_id+"_datalist";
		this.div.appendChild(this.datalist)
	}

	_set_datalist_options = (options) => {
		while (this.datalist.firstChild) {
			this.datalist.removeChild(this.datalist.firstChild);
		}
		for(let option of options) {
			let datalist_option = document.createElement("option");
			datalist_option.value = option;
			this.datalist.appendChild(datalist_option);
		}
	}

	add_option = (option) => {
		this.options.push(option);
		this.update_options();
	}

	remove_option = (option) => {
		this.unselect_tag(option)
		let index = this.selected.indexOf(tag);
		
		if (index === -1)
			return;

		this.selected.splice(index, 1);
		this.options.push(option);
		this.update_options();
	}
	
	update_options = () => {
		let selected_options = this.selected.map(tag => tag.tag_name);
		let left_options = this.options.filter( ( el ) => !selected_options.includes( el ) );
		this._set_datalist_options(left_options);
	}

	select_tag = (tag_name) => {
		
		if(!this.options.includes(tag_name)) {
			console.error(`Trying to add tag "${tag_name}", not in options.`);
			return;
		}

		if(this.selected.filter(tag => tag.tag_name === tag_name).length !== 0) {
			console.error(`Trying to add tag "${tag_name}", already selected.`);
			return;
		}

		let tag = new Tag(tag_name, this);
		this.selected.push(tag);
		this.div.appendChild(tag.div);
		this.update_options();
	}

	unselect_tag = (tag_name) => {
		let tag = this.selected.filter(tag => tag.tag_name === tag_name);
		if (tag.length === 0)
			return;

		tag = tag[0];
		let index = this.selected.indexOf(tag);
		this.selected.splice(index, 1);
		this.div.removeChild(tag.div)
		this.update_options();
	}
}

class Tag {
	constructor(tag_name, parent) {
		this.tag_name = tag_name;
		this.parent = parent;
		this.create_dom();
	}
	create_dom = () => {
		this.div = document.createElement("div");
		this.div.className = "tag";
		this.div.textContent = this.tag_name;
		this.div_close = document.createElement("i");
		this.div_close.className = "fas fa-times";
		this.div.appendChild(this.div_close);

		this.div.onclick = (e) => {
			this.parent.unselect_tag(this.tag_name);
		}
	}
}

// -----------------------------------------------------------------

let test = (options) => {
	if(!options.test2) {
		console.error("missing argument");
		return;
	}
	console.log("reached next");
}

// -----------------------------------------------------------------

init = () => {

	day_picker = new DayPicker({
		date: new Date(), 
		days_container_id: "day_picker",
		button_back_days_id: "day_back",
		button_forward_days_id: "day_forward",
		button_back_months_id: "month_back", 
		button_forward_months_id: "month_forward",
		datepicker_container_id: "day_picker_dummy_container",
		// datepicker_clingy_field_id: "day_picker_dummy_field",
		button_to_today_id: "button_to_today"
	});
	VIEW_DAILY = new View({
		view_id: "view_daily",
		button_id: "button_view_daily", 
		components: {day_picker: day_picker}
	});	


	edit_data_excel = jexcel(document.getElementById('edit_data_table'), {
		data:data,
		colWidths: [ 300, 100, 100, 50, 50, 100, 100]
	});
	VIEW_EDIT_DATA = new View({
		view_id: "view_edit_data",
		button_id: "button_view_edit_data",
		components: {
			edit_data_excel: edit_data_excel
		}
	});


	// let tag_selector = new TagBar({
	// 	options: ["Wish You Were Here", "Welcome to the Machine", "Have a Cigar", "Shine On You Crazy Diamond", "Another Brick in the Wall"],
	// 	tag_container_id: "tags_bar",
	// 	input_id: "datalist_report_select"
	// });
	let date_picker = new Lightpick({
		field: document.getElementById('datepicker'),
		singleDate: false,
		numberOfMonths: 2,
	});
	VIEW_REPORT = new View({
		view_id: "view_report", 
		button_id: "button_view_report",
		components: {
			date_picker: date_picker
		}
	});


	VIEW_ABOUT = new View({
		view_id: "view_about",
		button_id: "button_view_about"
	});

	VIEW_BUGREPORT = new View({
		view_id: "view_bugreport",
		button_id: "button_view_bugreport"
	});

	VIEW_SETTINGS = new View({
		view_id: "view_settings",
		button_id: "button_view_settings"
	});
	
	let ignore_apps = new IgnoreApps({
		div_container_id: "setting_ignore_app_container",
		button_add_id: "add_app_to_ignore",
		apps_to_ignore: ["explorer", "WindowsTimeTracker", "AdobeXD", "Java", "explorer", "WindowsTimeTracker", "AdobeXD", "Java", "explorer", "WindowsTimeTracker", "AdobeXD", "Java", "explorer", "WindowsTimeTracker", "AdobeXD", "Java"]
	})

	VIEW_TRACKING_SETTINGS = new View({
		view_id: "view_tracking_settings", 
		button_id: "button_view_tracking_settings",
		components: {
			ignore_apps: ignore_apps
		}
	});
	
	let tag_options = [
		{
			name: "Over1",
			children: [
				{
					name: "Under 1x",
				},
				{
					name: "Under 1y",
				},
			]
		},
		{
			name: "Over2",
			children: [
				{
					name: "Under2z",
					level: 1,
				},
				{
					name: "Under2a",
					level: 1,
				},
			]
		},
		{
			name: "Over3",
			children: [
				{
					name: "Under 1x",
				},
				{
					name: "Under 1y",
				},
			]
		},
		{
			name: "Over1",
			children: [
				{
					name: "Under 1",
				},
				{
					name: "Under 1",
				},
			]
		},
		{
			name: "Over1",
			children: [
				{
					name: "Under 1",
				},
				{
					name: "Under 1",
				},
			]
		}
	]
	tagselector = new TagSelector({
		container_id: "tags_suggestion_container",
		input_id: "tags_selector",
		button_tags_select_all_id: "button_tags_select_all",
		button_tags_unselect_all_id: "button_tags_unselect_all",
		tag_bar_id: "tags_bar",
		tag_options: tag_options
	});

	VIEW_ABOUT.select_view();
}

class TagSelector {
	constructor(options) {
		this.container_id = options.container_id;
		this.input_id = options.input_id;
		this.button_tags_select_all_id = options.button_tags_select_all_id;
		this.button_tags_unselect_all_id = options.button_tags_unselect_all_id;
		this.tag_bar_id = options.tag_bar_id;

		this.container = document.getElementById(this.container_id);
		this.input = document.getElementById(this.input_id);
		this.button_tags_select_all = document.getElementById(this.button_tags_select_all_id);
		this.button_tags_unselect_all = document.getElementById(this.button_tags_unselect_all_id);
		this.tag_bar = document.getElementById(this.tag_bar_id);

		this.tag_options = options.tag_options;

		this.hovering = false;
		this.mode_add = true;

		this.children = [];

		for(let tag_option of this.tag_options) {
			// this.createGuiElement(tag_option)
			let tag = new Tag2({
				tag_name: tag_option.name,
				level: 0,
				handler: this,
				children: []
			})
			this.children.push(tag);
			this.container.appendChild(tag.div);
			this.tag_bar.appendChild(tag.div_tag);
			if(tag_option.children) {
				for(let tag_child of tag_option.children) {
					let tag_c = new Tag2({
						tag_name: tag_child.name,
						level: 1,
						handler: this,
						parent: tag
					})
					tag.children.push(tag_c);
					this.container.appendChild(tag_c.div);
					this.tag_bar.appendChild(tag_c.div_tag);
				}
			}
		}
		
		this._add_listeners()
	}

	_add_listeners = () => {
		//Upon clicking the input box, open the selector
		this.input.onclick = (e) => {
			e.stopPropagation();
			this.open();
		}
		//When clicking on the selector, stop event propagation (otherwise would close itself)
		this.container.onclick = (e) => {
			e.stopPropagation();
		}

		//Upon typing, search
		this.input.oninput = (e) => {
			this.filter(this.input.value);
		}

		//Buttons
		this.button_tags_select_all.onclick = () => {
			this.input.focus();
			this.children.map(child => {
				if(child.showing)
					child.select()
			});
		}
		this.button_tags_unselect_all.onclick = () => {
			this.input.focus();
			this.children.map(child => {
				if(child.showing)
					child.unselect()
			});
		}
	}

	select_all_visibile = () => {
		this.children.map(child => {
			if(child.showing) {
				child.select();
			}
		});
	}

	filter = (query) => {
		this.children.map(child => child.filter(query));
	}

	open = () => {
		this.container.style.display = "inherit";
		this.showing = true;
		document.body.addEventListener("click", this.close)
	}

	close = () => {
		this.container.style.display = "none";
		this.showing = false;
		document.body.removeEventListener("click", this.close)
	}

	createGuiElement = (tag_option) => {
		let div_tag = document.createElement("div");
		div_tag.textContent = tag_option.name;
		div_tag.classList.add("tag_suggestion")
		if(tag_option.level === 0)
			div_tag.classList.add("l0")
		else
			div_tag.classList.add("l1")
		this.container.appendChild(div_tag)

		div_tag.onmousedown = (e) => {
			console.log("down", tag_option.name);
		
			if(div_tag.classList.contains("selected")) {
				this.mode_add = false;
			}else {
				this.mode_add = true;
			}
			div_tag.classList.toggle("selected");
			
			this.hovering = true;
		}
		div_tag.onmouseup = (e) => {
			console.log("up", tag_option.name);
			this.hovering = false;
		}
		div_tag.onmouseenter = (e) => {
			// console.log("enter", tag_option.name);
			if(this.hovering && this.mode_add) {
				div_tag.classList.add("selected");
			}else if(this.hovering && !this.mode_add) {
				div_tag.classList.remove("selected");
			}
		}
	}
}

class TagX {
	constructor(tag_name, parent) {
		this.tag_name = tag_name;
		this.parent = parent;
		this.create_dom();
	}
	create_dom = () => {
		this.div = document.createElement("div");
		this.div.className = "tag";
		this.div.textContent = this.tag_name;
		this.div_close = document.createElement("i");
		this.div_close.className = "fas fa-times";
		this.div.appendChild(this.div_close);

		this.div.onclick = (e) => {
			this.parent.unselect();
		}
	}
}

class Tag2 {
	constructor(options) {
		this.tag_name = options.tag_name;
		this.level = options.level;
		this.children = options.children || [];
		this.parent = options.parent || undefined;
		this.handler = options.handler;

		this.selectable = true;

		//Create gui element for tag
		this.div_tag = document.createElement("div");
		this.div_tag.className = "tag hide";
		this.div_tag.textContent = this.tag_name;
		this.div_tag_close = document.createElement("i");
		this.div_tag_close.className = "fas fa-times";
		this.div_tag.appendChild(this.div_tag_close);

		//Create gui element in selector
		this.div = document.createElement("div");
		this.div.textContent = this.tag_name;
		this.div.classList.add("tag_suggestion")

		if(this.level === 0)
			this.div.classList.add("l0") 
		else
			this.div.classList.add("l1") 

		this._add_listeners();
	}

	_add_listeners = () => {
		this.div.onmousedown = (e) => {
			// console.log("down", this.tag_name);
		
			if(this,this.is_selected) {
				this.handler.mode_add = false;
				this.unselect();
			}else {
				this.handler.mode_add = true;
				this.select();
			}
			
			this.handler.hovering = true;
		}
		this.div.onmouseup = (e) => {
			// console.log("up", this.tag_name);
			this.handler.hovering = false;
		}
		this.div.onmouseenter = (e) => {
			// console.log("enter", tag_option.name);
			if(this.handler.hovering && this.handler.mode_add) {
				if(this.children.length === 0)
					this.select();
			}else if(this.handler.hovering && !this.handler.mode_add) {
				if(this.children.length === 0)
					this.unselect();
			}
		}
		this.showing = true;

		this.div_tag.onclick = () => {
			this.unselect();
		}
	}

	_set_unselectable = () => {
		this.div.classList.add("unselectable");
		this.selectable = false;
	}
	_set_selectable = () => {
		this.div.classList.remove("unselectable");
		this.selectable = true;
	}

	filter = (query) => {
		if(this.tag_name.includes(query)) {
			this.show();
			this._set_selectable();
			this.children.map(child => child.show());
			return true;
		}else {
			let found = this.children.filter(child => child.filter(query)).length;
			// console.log(this.tag_name, found, query)
			if(found !== 0 && found === this.children.length) {
				this.show();
				this._set_selectable();
			}else if(found !== 0) {
				this._set_unselectable();
				this.show();
			}else {
				this.hide();
			}
		}
	}

	show = () => {
		this.showing = true;
		this.div.classList.remove("hide")
	}

	hide = () => {
		this.showing = false;
		this.div.classList.add("hide")
	}

	//Selects & permutates to children
	select = () => {
		if(this.selectable) {
			for(let child of this.children) {
				child.select()
			}
			if(this.children.length === 0)
				this.div.classList.add("selected")
			this.is_selected = true;
					          
	          if(this.children.length === 0)
				this.div_tag.classList.remove("hide")
		}
	}

	// Unselects only iteself
	unselect_excl = () => {
		this.is_selected = false;
		this.div.classList.remove("selected")
	}

	// Unselects & permutates to children
	unselect = () => {
		for(let child of this.children) {
			child.unselect()
		}
		if(this.parent) {
			this.parent.unselect_excl();
		}
		this.is_selected = false;
		this.div.classList.remove("selected")
		this.div_tag.classList.add("hide")
	}
}


class IgnoreApps {
	constructor(options) {
		this.div_container_id = options.div_container_id;
		this.div_container = document.getElementById(this.div_container_id);

		this.button_add_id = options.button_add_id;
		this.button_add = document.getElementById(this.button_add_id);

		this.elements = [];
		if(options.apps_to_ignore !== undefined) {
			for(let app of options.apps_to_ignore) {
				this.create(app);
			}
		}

		this.button_add.onclick = () => {
			this.create();
		}
	}

	tolist = () => {
		this.elements.map(element => element.get_text());
	}
	
	create = (text = "") => {
		let ignore_app_element = new IgnoreAppElement(text, this);
		this.div_container.appendChild(ignore_app_element.div);
		this.elements.push(ignore_app_element);
		this.div_container.scrollTop = this.div_container.scrollHeight;
	}

	remove = (ignore_app_element) => {
		this.div_container.removeChild(ignore_app_element.div);
		this.elements.splice(this.elements.indexOf(ignore_app_element), 1);
	}
}

class IgnoreAppElement {
	constructor(text, parent) {
		this.parent = parent;

		this.div = document.createElement("div");
		this.div.className = "setting_ignore_app";
		
		this.div_entry = document.createElement("input");
		this.div_entry.className = "setting_entry";
		this.div_entry.value = text;
		
		this.button = document.createElement("button");
		this.button.className = "standard_button";
		this.button.setAttribute("icononly", "true");
		
		this.icon = document.createElement("i")
		this.icon.className = "fas fa-times";

		this.button.appendChild(this.icon)
		this.div.appendChild(this.div_entry)
		this.div.appendChild(this.button)

		this.button.onclick = () => {
			this.parent.remove(this);
		}
	}

	get_text = () => {
		return this.div_entry.value;
	}
}