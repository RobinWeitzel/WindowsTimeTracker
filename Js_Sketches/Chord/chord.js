function start() {
	
	// create the svg area
	var svg = d3.select("#my_dataviz")
	.append("svg")
	.attr("width", 440)
	.attr("height", 440)
	.append("g")
	.attr("transform", "translate(220,220)")
	
	// create input data: a square matrix that provides flow between entities
	var matrix = [
		[60,0,0,0,0,0],
		[0,0,0,120,0,0],
		[0,0,0,0,240,0],
		[0,60,0,0,0,0],
		[0,0,240,0,0,0],
		[0,0,0,0,0,800]
	];
	
	// give this matrix to d3.chord(): it will calculates all the info we need to draw arc and ribbon
	var res = d3.chord()
	.padAngle(0.05)     // padding between entities (black arc)
	.sortSubgroups(d3.descending)
	(matrix)
	
	// add the groups on the inner part of the circle
	svg
	.datum(res)
	.append("g")
	.selectAll("g")
	.data(function(d) { return d.groups; })
	.enter()
	.append("g")
	.append("path")
	.style("fill", "grey")
	.style("stroke", "black")
	.attr("d", d3.arc()
	.innerRadius(200)
	.outerRadius(210)
	)
	
	// Add the links between groups
	svg
	.datum(res)
	.append("g")
	.selectAll("path")
	.data(function(d) { return d; })
	.enter()
	.append("path")
	.attr("d", d3.ribbon()
	.radius(200)
	)
	.attr("class", "connection")
}