
// Mapping of step names to colors.
var colors = {
	"monday": "#36688d",
	"tuesday": "#f3cd05",
	"wednesday": "#f49f05",
	"thursday": "#f18904",
	"friday": "#bda589",
	
	"work": "#a7414a",
	
	"meetings": "#466a61",
	"project": "#a37c27",
	"project2": "#563838",
	"daily standup": "#ba2727",
	
	"a": "#84ada3",
	"b": "#5cc9ae",
	"c": "#2d8d75",
	"weekly sync": "#674966",
	
	"introduction": "#d667d3",
	"brainstorming": "#b754b4",
	"working": "#6c3b6a",
	
	"placeholder": "#00ced100"
};

let json = {
	"name": "tuesday",
	"children": [
		{
			"name":"Breakfast",
			"size": 60 
		},
		{
			"name": "work",
			"children": [
				{
					"name": "daily standup",
					"size": 10
				},
				{
					"name": "project",
					"size": 20
				},
				{
					"name": "meetings",
					"children": [
						{
							"name": "a",
							"size": 20
						},
						{
							"name": "b",
							"size": 40
						},
						{
							"name": "c",
							"size": 20
						}
					]
				},
				{
					"name": "weekly sync",
					"children": [
						{
							"name": "introduction",
							"size": 20
						},
						{
							"name": "brainstorming",
							"size": 40
						},
						{
							"name": "working",
							"size": 160
						}
					]
				},
			]
		}
	]
}

let global_index = 0;
let all_items = [];
let all_links = [];

function convert(json) {
	let your_day = {
		"name" : "Your Day",
		"color": "#edbd00", 
		"index": 0
	}
	all_items.push(your_day);
	reduce(json.children, 0)
}

function reduce(children, previous_index) {
	for(let c in children) {
		let find_item = existsAlready(all_items, children[c].name);

		//if it doesnt exist already
		if(find_item == false) {
			global_index++;
			new_item = {
				"name" : children[c].name,
				"index": global_index,
				"color": colors[children[c].name] //"#edbd00" //json.children[c].color
			};
			all_items.push(new_item)
		}

		console.log(children[c])
		let link = {
			"source" : previous_index,
			"value" : deepSum(children[c]),
			"target" : global_index
		};

		all_links.push(link)

		//recursively add the items children as well
		reduce(children[c].children, global_index);
	}
}

function deepSum(child) {
	if(child.children == undefined) {
		return child.size;
	}else {
		let a  = 0;
		for(let c in child.children) {
			a = a + deepSum(child.children[c])
		}
		return a;
	}
}

function existsAlready(list, current_element) {
	let a = json.children.find(item => {
		return item.name === current_element.name
	})
	return (a != undefined) ? a : false;
}

function start() {
	
	convert(json);

	json = {
		"nodes" : all_items,
		"links": all_links
	}

	var chart = d3.select("#chart").append("svg").chart("Sankey.Path");
	chart.name(label)
	.colorNodes(function(name, node) {
		return color(node, 1) || colors.fallback;
	})
	.colorLinks(function(link) {
		return color(link.source, 4) || color(link.target, 1) || colors.fallback;
	})
	.nodeWidth(15)
	.nodePadding(10)
	.spread(true)
	.alignLabel('start')
	.iterations(0)
	.draw(json);
	console.log(chart)
}

function label(node) {
	return node.name;
	//return node.name.replace(/\s*\(.*?\)$/, '');
}
function color(node, depth) {
	return node.color;
}