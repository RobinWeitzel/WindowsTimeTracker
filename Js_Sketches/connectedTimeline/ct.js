let json = {
	"nodes": [
		{
			"id": 1,
			"name": "A",
			"size": 100
		},
		{
			"id": 2,
			"name": "B",
			"size": 100
		},
		{
			"id": 3,
			"name": "C",
			"size": 100
		},
		{
			"id": 4,
			"name": "D",
			"size": 200
		},
		{
			"id": 5,
			"name": "E",
			"size": 100
		},
		{
			"id": 6,
			"name": "F",
			"size": 100
		},
		{
			"id": 7,
			"name": "G",
			"size": 100
		},
		{
			"id": 8,
			"name": "H",
			"size": 100
		},
		{
			"id": 9,
			"name": "I",
			"size": 100
		},
		{
			"id": 10,
			"name": "J",
			"size": 150
		}
	],
	"links": [
		
		{
			"source": 1,
			"target": 2
		},
		{
			"source": 1,
			"target": 5
		},
		{
			"source": 1,
			"target": 6
		},
		
		{
			"source": 2,
			"target": 3
		},
		{
			"source": 2,
			"target": 7
		}
		,
		
		{
			"source": 3,
			"target": 4
		},
		{
			"source": 8,
			"target": 3
		}
		,
		{
			"source": 4,
			"target": 5
		}
		,
		
		{
			"source": 4,
			"target": 9
		},
		{
			"source": 5,
			"target": 10
		}
	]
}

function start() {
	// set the dimensions and margins of the graph
	var margin = {top: 20, right: 30, bottom: 20, left: 30},
	width = 450 - margin.left - margin.right,
	height = 300 - margin.top - margin.bottom;
	
	// append the svg object to the body of the page
	var svg = d3.select("#my_dataviz")
	.append("svg")
	.attr("width", width + margin.left + margin.right)
	.attr("height", height + margin.top + margin.bottom)
	.append("g")
	.attr("transform",
	"translate(" + margin.left + "," + margin.top + ")");
	
	// Read dummy data
	data = json;
	
	// List of node names
	var allNodes = data.nodes.map(function(d){return d.name})
	
	// A linear scale to position the nodes on the X axis
	var x = d3.scalePoint()
	.range([0, width])
	.domain(allNodes)
	
	// Add the circle for the nodes
	var nodes = svg
	.selectAll("mynodes")
	.data(data.nodes)
	.enter()
	.append("rect")
	.attr("width", function(d){ 
		console.log(d)
		return d.size/4
	})
	.attr("height", 40)
	.attr("x", function(d){ return(x(d.name)-20)})
	.attr("y", height-30)
	.style("fill", "#69b3a2")

	console.log(nodes)
	
	// And give them a label
	var labels = svg
	.selectAll("mylabels")
	.data(data.nodes)
	.enter()
	.append("text")
	.attr("x", function(d){ return(x(d.name))})
	.attr("y", height-10)
	.text(function(d){ return(d.name)})
	.style("text-anchor", "middle")
	
	// Add links between nodes. Here is the tricky part.
	// In my input data, links are provided between nodes -id-, NOT between node names.
	// So I have to do a link between this id and the name
	var idToNode = {};
	data.nodes.forEach(function (n) {
		idToNode[n.id] = n;
	});
	// Cool, now if I do idToNode["2"].name I've got the name of the node with id 2
	
	// Add the links
	var links = svg
	.selectAll('mylinks')
	.data(data.links)
	.enter()
	.append('path')
	.attr('d', function (d) {
		start = x(idToNode[d.source].name)    // X position of start node on the X axis
		end = x(idToNode[d.target].name)      // X position of end node
		return ['M', start, height-30,    // the arc starts at the coordinate x=start, y=height-30 (where the starting node is)
		'A',                            // This means we're gonna build an elliptical arc
		(start - end)/2, ',',    // Next 2 lines are the coordinates of the inflexion point. Height of this point is proportional with start - end distance
		(start - end)/2, 0, 0, ',',
		start < end ? 1 : 0, end, ',', height-30] // We always want the arc on top. So if end is before start, putting 0 here turn the arc upside down.
		.join(' ');
	})
	.style("fill", "none")
	.attr("stroke", "black")
	
	// Add the highlighting functionnality
	nodes
	.on('mouseover', function (d) {
		// Highlight the nodes: every node is green except of him
		nodes.style('fill', "#B8B8B8")
		d3.select(this).style('fill', '#69b3b2')
		// Highlight the connections
		links
		.style('stroke', function (link_d) { return link_d.source === d.id | link_d.target === d.id ? '#69b3b2' : '#b8b8b8';})
		.style('stroke-width', function (link_d) { return link_d.source === d.id | link_d.target === d.id ? 4 : 1;})
	})
	.on('mouseout', function (d) {
		nodes.style('fill', "#69b3a2")
		links
		.style('stroke', 'black')
		.style('stroke-width', '1')
	})
	// text hover nodes
	svg
	.append("text")
	.attr("text-anchor", "middle")
	.style("fill", "#B8B8B8")
	.style("font-size", "17px")
	.attr("x", 50)
	.attr("y", 10)
	.html("Hover nodes")
}