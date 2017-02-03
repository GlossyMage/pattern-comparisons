function MazeGame(){

	this.mazeGen;
	this.createMaze = function(x, y, mazeGen) {

		this.mazeGen = mazeGen;

		if (this.mazeGen == null) {
			this.mazeGen = new LineGenerator();
		}
		return this.mazeGen.generate(x, y);
	}
}

MazeGenerator = function() { throw "Abstract class!" }

MazeGenerator.prototype.generate = function(x, y) {
	throw "Abstract function not overridden by child!";
}

MazeGenerator.prototype.makeMaze = function(x, y) {
	return new Maze(x, y);
}
MazeGenerator.prototype.makeWall = function() {
	return new Wall();
}
MazeGenerator.prototype.makeRoom = function() {
	return new Room();
}

LineGenerator = function() {

	this.generate = function(x, y) {
		var m = this.makeMaze(x, y);
		for (var i = 0; i < x; i++) {
			for (var j = 0; j < y; j++) {
				m.addComponent(this.makeWall(), i, j);
			}
		}

		for (var i = 1; i < x - 1; i++) {
			m.addComponent(this.makeRoom(), i, parseInt(y/2));
		}
		
		return m;
	}
}

LineGenerator.prototype = Object.create(MazeGenerator.prototype);


/* This generate function could honestly have been made more efficient, but as
 * long as each implementation contains the same inefficiency, that should be
 * irrelevant. */
BoxGenerator = function() {

	this.generate = function(x, y) {
		var m = this.makeMaze(x, y);

		for (var i = 0; i < x; i++) {
			for (var j = 0; j < y; j++) {
				m.addComponent(this.makeWall(), i, j);
			}
		}

		if (x <= 2 || y <= 2) {
			return m;
		}

		for (var i = 1; i < x - 1; i++) {
			m.addComponent(this.makeRoom(), i, 1);
			m.addComponent(this.makeRoom(), i, j-2);
		}

		for (var j = 1; j < y - 1; j++) {
			m.addComponent(this.makeRoom(), 1, j);
			m.addComponent(this.makeRoom(), x-2, j);
		}

		return m;
	}
}

BoxGenerator.prototype = Object.create(MazeGenerator.prototype);

function Maze(x, y) {
	var length = x;
	var height = y;
	var map = new Array(x);

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
		for (var y = 0; y < height; y++) {
			for (var x = 0; x < length; x++) {
				process.stdout.write(map[x][y].getSymbol());
			}
			process.stdout.write("\n");
		}
	}
}

function Room() {
	this.isSolid = function() {
		return false;
	}

	this.getSymbol = function() {
		return '.';
	}
}

function CoinRoom() {
	Room.apply(this, arguments);
	var coin = false;
	if (Math.random() >= 0.75) {
		coin = true;
	}

	this.getSymbol = function() {
		if (coin == true) {
			return '@';
		} else {
			return '.';
		}
	}
}
CoinRoom.prototype = new Room();

function Wall() {
	this.isSolid = function() {
		return true;
	}

	this.getSymbol = function() {
		return '#';
	}
}

var mg = new MazeGame();
var m = mg.createMaze(9, 9, new BoxGenerator());
m.printMaze();

