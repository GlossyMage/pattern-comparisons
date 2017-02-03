function MazeGame() {
	this.componentFactory = new ComponentFactory();

	this.createMaze = function(x, y) {
		var m = this.makeMaze(x, y);

		for (var i = 0; i < x; i++) {
			for (var j = 0; j < y; j++) {
				m.addComponent(this.makeWall(), i, j);
			}
		}
		m.addComponent(this.makeRoom(), 1, 2);
		m.addComponent(this.makeRoom(), 2, 2);
		m.addComponent(this.makeRoom(), 3, 2);
		m.addComponent(this.makeRoom(), 1, 1);
		m.addComponent(this.makeRoom(), 3, 1);
		m.addComponent(this.makeRoom(), 1, 3);
		m.addComponent(this.makeRoom(), 3, 3);

		return m;
	}

	this.makeMaze = function(x, y) {
		return new Maze(x, y, (Math.random() * 100));
	}

	this.makeRoom = function() {
		return this.componentFactory.createComponent("coinroom");
	}

	this.makeWall = function() {
		return this.componentFactory.createComponent("wall");
	}
}

function Maze(x, y, s) {
	var length = x;
	var height = y;
	var map = new Array(x);
	var seed = s;

	for (var i = 0; i < x; i++) {
		map[i] = new Array(y);
		for (var j = 0; j < y; j++) {
			map[i][j] = 0;
		}
	}

	this.addComponent = function(component, x, y) {
		if (!(x >= length || y >= height || x < 0 || y < 0)) {
			map[x][y] = component;
		}
	}

	this.printMaze = function() {
		var context = new MazeContext(seed);
		for (var y = 0; y < 5; y++) {
			for (var x = 0; x < 5; x++) {
				process.stdout.write(map[x][y].getSymbol(context));
				context.next();
			}
			process.stdout.write("\n");
		}
	}
}

function ComponentFactory() {
	var flyweights = {};

	this.createComponent = function(componentType) {
		if (!flyweights[componentType]) {
			flyweights[componentType] = new MazeComponent(componentType);
		}

		return flyweights[componentType];
	}
}

function MazeComponent(ct) {
	var componentType = ct;

	this.isSolid = function() {
		if (componentType == "wall") {
			return true;
		} else {
			return false;
		}
	}

	this.getSymbol = function(mazeContext) {
		if (componentType == "wall") {
			return '#';
		} else if (componentType == "room") {
			return '.';
		} else if (componentType == "coinroom") {
			return mazeContext.hasCoin() ? '@' : '.';
		} else {
			return ' ';
		}
	}
}

function MazeContext(s) {
	var seed = s;
	var index = 0;

	this.next = function() {
		index++;
	}

	this.hasCoin = function() {
		var r = new Random(seed*(index+1));
		var coin = false;

		if (r.random() >= 75) {
			return true;
		} else {
			return false;
		}
	}
}

function Random(s) {
	var seed = s;

	this.random = function() {
		var x = Math.sin(seed++) * 10000;
		return (x - Math.floor(x)) * 100;
	}
}

var mg = new MazeGame();
var m = mg.createMaze(5, 5);
m.printMaze();
