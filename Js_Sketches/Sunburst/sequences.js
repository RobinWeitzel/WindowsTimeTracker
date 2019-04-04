// Dimensions of sunburst.
var width = 750;
var height = 600;
var radius = Math.min(width, height) / 2;

// Breadcrumb dimensions: width, height, spacing, width of tip/tail.
var b = {
	w: 120,
	h: 30,
	s: 3,
	t: 10
};

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

// Total size of all segments; we set this later, after loading the data.
var totalSize = 0;

var vis = d3.select("#chart").append("svg:svg")
.attr("width", width)
.attr("height", height)
.append("svg:g")
.attr("id", "container")
.attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

var partition = d3.partition()
.size([2 * Math.PI, radius * radius]);

var arc = d3.arc()
.startAngle(function (d) {
	return d.x0;
})
.endAngle(function (d) {
	return d.x1;
})
.innerRadius(function (d) {
	return Math.sqrt(d.y0);
})
.outerRadius(function (d) {
	return Math.sqrt(d.y1);
});

let jsonData;
function start() {
	jsonData = {
		"name": "root",
		"children": [
			{"name":"placeholder", "size":5},
			{
				"name": "monday",
				"children": [
					{
						"name": "work",
						"children": [
							{
								"name": "daily standup",
								"size": 10
							},
							{
								"name": "project",
								"size": 200
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
										"size": 60
									},
									{
										"name": "c",
										"size": 20
									}
								]
							}
						]
					}
				]
			},
			{"name":"placeholder", "size":10},
			{
				"name": "tuesday",
				"children": [
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
			},
			{"name":"placeholder", "size":10},
			{
				"name": "wednesday",
				"children": [
					{
						"name": "work",
						"children": [
							{
								"name": "daily standup",
								"size": 10
							},
							{
								"name": "project",
								"size": 200
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
										"size": 60
									},
									{
										"name": "c",
										"size": 20
									}
								]
							}
						]
					}
				]
			},
			{"name":"placeholder", "size":10},
			{
				"name": "thursday",
				"children": [
					{
						"name": "work",
						"children": [
							{
								"name": "daily standup",
								"size": 10
							},
							{
								"name": "project",
								"size": 200
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
										"size": 60
									},
									{
										"name": "c",
										"size": 20
									}
								]
							}
						]
					}
				]
			},
			{"name":"placeholder", "size":10},
			{
				"name": "friday",
				"children": [
					{
						"name": "work",
						"children": [
							{
								"name": "daily standup",
								"size": 10
							},
							{
								"name": "project",
								"size": 200
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
										"size": 60
									},
									{
										"name": "c",
										"size": 20
									}
								]
							}
						]
					}
				]
			},
			{"name":"placeholder", "size":5}
		]
	};
	
	createVisualization(jsonData);
}

var path; 

// Main function to draw and set up the visualization, once we have the data.
function createVisualization(json) {
	
	// Basic setup of page elements.
	initializeBreadcrumbTrail();
	// drawLegend();
	// d3.select("#togglelegend").on("click", toggleLegend);
	
	// Bounding circle underneath the sunburst, to make it easier to detect
	// when the mouse leaves the parent g.
	vis.append("svg:circle")
	.attr("r", radius)
	.style("opacity", 0);
	
	//vis.append("svg:text").attr("dx", 10).text("Hello")
	
	// Turn the data into a d3 hierarchy and calculate the sums.
	var root = d3.hierarchy(json)
	.sum(function (d) {
		return d.size;
	});
	//.sort(function(a, b) { return b.value - a.value; });
	
	// For efficiency, filter nodes to keep only those large enough to see.
	var nodes = partition(root).descendants()
	.filter(function (d) {
		return (d.x1 - d.x0 > 0.005); // 0.005 radians = 0.29 degrees
	});
	
	for(let fdc in nodes[0].children) {
		let first_degree_child = nodes[0].children[fdc]
		console.log(first_degree_child)
		first_degree_child.y0 = 30000;
		//first_degree_child.append("text").text("HALLO!")
	}
	
	path = vis.data([json]).selectAll("path")
	.data(nodes)
	.enter().append("svg:path")
	.attr("display", function (d) {
		return d.depth ? null : "none";
	})
	.attr("d", arc)
	.attr("fill-rule", "evenodd")
	.style("fill", function (d) {
		return colors[d.data.name];
	})
	.attr("id", function (d,i) {
		console.log(d.data.name,i)
		return d.data.name;
	})
	.style("opacity", 1)
	.on("mouseover", mouseover);
	
	
	let days = ["#monday", "#tuesday", "#wednesday", "#thursday", "#friday"];
	let daysName = ["MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY"];
	for(let i = 0; i < 5; i++) {
		var textArc = vis.append("text")
		.attr("class", "innerDescriptionText")
		.attr("dy", 35)
		.append("textPath")				//append a textPath to the text element
		.style("text-anchor","middle")
		.attr("startOffset", "25%")
		.attr("xlink:href", days[i]) 	//place the ID of the path here
		.text(daysName[i]);
		// var textArc = vis.append("text")
		// .attr("class", "innerDescriptionText")
		// .attr("x", 5) //Move the text from the start angle of the arc
		// .attr("dy", 40) //Move the text down
		// .append("textPath")
		// .attr("xlink:href", days[i]) 	//place the ID of the path here
		// .text(daysName[i]);
	}	
	
	// Add the mouseleave handler to the bounding circle.
	d3.select("#container").on("mouseleave", mouseleave);
	
	// Get total size of the tree = value of root node from partition.
	totalSize = path.datum().value;
};

// Fade all but the current sequence, and show it in the breadcrumb trail.
function mouseover(d) {
	
	//var percentage = (100 * d.value / totalSize).toPrecision(3);
	//var percentageString = percentage + "%";
	console.log(d)

	let percentageString;
	if (d.value < 0.1) {
		percentageString = "< 10mins";
	} else {
		percentageString = (d.value / 60).toPrecision(2) + "hrs";
	}
	
	d3.select("#item")
	.text(d.data.name);

	d3.select("#value")
	.text(percentageString);
	
	d3.select("#explanation")
	.style("visibility", "");
	
	var sequenceArray = d.ancestors().reverse();
	sequenceArray.shift(); // remove root node from the array
	updateBreadcrumbs(sequenceArray, percentageString);
	
	// Fade all the segments.
	d3.selectAll("path")
	.style("opacity", 0.3);
	
	// Then highlight only those that are an ancestor of the current segment.
	vis.selectAll("path")
	.filter(function (node) {
		return (sequenceArray.indexOf(node) >= 0);
	})
	.style("opacity", 1);
}

// Restore everything to full opacity when moving off the visualization.
function mouseleave(d) {
	
	// Hide the breadcrumb trail
	d3.select("#trail")
	.style("visibility", "hidden");
	
	// Deactivate all segments during transition.
	d3.selectAll("path").on("mouseover", null);
	
	// Transition each segment to full opacity and then reactivate it.
	d3.selectAll("path")
	.transition()
	.duration(1000)
	.style("opacity", 1)
	.on("end", function () {
		d3.select(this).on("mouseover", mouseover);
	});
	
	d3.select("#explanation")
	.style("visibility", "hidden");
}

function initializeBreadcrumbTrail() {
	// Add the svg area.
	var trail = d3.select("#sequence").append("svg:svg")
	.attr("width", width)
	.attr("height", 50)
	.attr("id", "trail");
	// Add the label at the end, for the percentage.
	trail.append("svg:text")
	.attr("id", "endlabel")
	.style("fill", "#000");
}

// Generate a string that describes the points of a breadcrumb polygon.
function breadcrumbPoints(d, i) {
	var points = [];
	points.push("0,0");
	points.push(b.w + ",0");
	points.push(b.w + b.t + "," + (b.h / 2));
	points.push(b.w + "," + b.h);
	points.push("0," + b.h);
	if (i > 0) { // Leftmost breadcrumb; don't include 6th vertex.
	points.push(b.t + "," + (b.h / 2));
}
return points.join(" ");
}

// Update the breadcrumb trail to show the current sequence and percentage.
function updateBreadcrumbs(nodeArray, percentageString) {
	
	// Data join; key function combines name and depth (= position in sequence).
	var trail = d3.select("#trail")
	.selectAll("g")
	.data(nodeArray, function (d) {
		return d.data.name + d.depth;
	});
	
	// Remove exiting nodes.
	trail.exit().remove();
	
	// Add breadcrumb and label for entering nodes.
	var entering = trail.enter().append("svg:g");
	
	entering.append("svg:polygon")
	.attr("points", breadcrumbPoints)
	.style("fill", function (d) {
		return colors[d.data.name];
	});
	
	entering.append("svg:text")
	.attr("x", (b.w + b.t) / 2)
	.attr("y", b.h / 2)
	.attr("dy", "0.35em")
	.attr("text-anchor", "middle")
	.text(function (d) {
		return d.data.name;
	});
	
	// Merge enter and update selections; set position for all nodes.
	entering.merge(trail).attr("transform", function (d, i) {
		return "translate(" + i * (b.w + b.s) + ", 0)";
	});
	
	// Now move and update the percentage at the end.
	// d3.select("#trail").select("#endlabel")
	// .attr("x", (nodeArray.length + 0.5) * (b.w + b.s))
	// .attr("y", b.h / 2)
	// .attr("dy", "0.35em")
	// .attr("text-anchor", "middle")
	// .text(percentageString);
	
	// Make the breadcrumb trail visible, if it's hidden.
	d3.select("#trail")
	.style("visibility", "");
	
}

// function drawLegend() {
	
// 	// Dimensions of legend item: width, height, spacing, radius of rounded rect.
// 	var li = {
// 		w: 150,
// 		h: 30,
// 		s: 3,
// 		r: 3
// 	};
	
// 	var legend = d3.select("#legend").append("svg:svg")
// 	.attr("width", li.w)
// 	.attr("height", d3.keys(colors).length * (li.h + li.s));
	
// 	var g = legend.selectAll("g")
// 	.data(d3.entries(colors))
// 	.enter().append("svg:g")
// 	.attr("transform", function (d, i) {
// 		return "translate(0," + i * (li.h + li.s) + ")";
// 	});
	
// 	g.append("svg:rect")
// 	.attr("rx", li.r)
// 	.attr("ry", li.r)
// 	.attr("width", li.w)
// 	.attr("height", li.h)
// 	.style("fill", function (d) {
// 		return d.value;
// 	});
	
// 	g.append("svg:text")
// 	.attr("x", li.w / 2)
// 	.attr("y", li.h / 2)
// 	.attr("dy", "0.35em")
// 	.attr("text-anchor", "middle")
// 	.text(function (d) {
// 		return d.key;
// 	});
// }

// function toggleLegend() {
// 	var legend = d3.select("#legend");
// 	if (legend.style("visibility") == "hidden") {
// 		legend.style("visibility", "");
// 	} else {
// 		legend.style("visibility", "hidden");
// 	}
// }
