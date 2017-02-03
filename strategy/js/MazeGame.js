function MazeGame(){
}

MazeGame.createMaze = function(mf) {
	if (!(mf instanceof MazeFactory)) {
		throw new Error("Invalid MazeFactory used!");
	}
	var m = mf.makeMaze(5, 5);

	for (var x = 0; x < 5; x++) {
		for (var y = 0; y < 5; y++) {
			m.addComponent(mf.makeWall(), x, y);
		}
	}
	m.addComponent(mf.makeRoom(), 1, 2);
	m.addComponent(mf.makeRoom(), 2, 2);
	m.addComponent(mf.makeRoom(), 3, 2);
	m.addComponent(mf.makeRoom(), 1, 1);
	m.addComponent(mf.makeRoom(), 3, 1);
	m.addComponent(mf.makeRoom(), 1, 3);
	m.addComponent(mf.makeRoom(), 3, 3);

	return m;
}

function MazeFactory() {
	this.makeMaze = function(x, y) {
		return new Maze(x, y);
	}

	this.makeRoom = function() {
		return new Room();
	}

	this.makeWall = function() {
		return new Wall();
	}
}

function CoinFactory() {
	MazeFactory.apply(this, arguments);

	this.makeRoom = function() {
		return new CoinRoom();
	}
}
CoinFactory.prototype = new MazeFactory();

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
		for (var y = 0; y < 5; y++) {
			for (var x = 0; x < 5; x++) {
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

var mf = new CoinFactory();
var m = MazeGame.createMaze(mf);
m.printMaze();
