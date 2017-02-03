public class MazeGame {

    private MazeGenerator mazeGen;

    public MazeGame(String type) {
        if (type.equals("box")) {
            mazeGen = new BoxGenerator();
        } else if (type.equals("line")) {
            mazeGen = new LineGenerator();
        } else {
            System.exit(0);
        }
    }

    public Maze createMaze(int x, int y) {
        if (mazeGen != null) {
            return mazeGen.generate(x, y);
        }
        return null;
    }

    Maze makeMaze(int x, int y) {
        return new Maze(x, y);
    }
    
    public static void main(String[] args) {
        if (args.length != 1) {
            System.out.println("usage: java "
            System.exit(0);
        }

        MazeGame mg = new MazeGame(args[0]);
        Maze m = mg.createMaze(10, 10);
        m.printMaze();
    }
}

abstract class MazeGenerator {
    abstract Maze generate(int x, int y);

    protected Wall makeWall() {
        return new Wall();
    }

    protected Room makeRoom() {
        return new Room();
    }
}

class LineGenerator extends MazeGenerator {
    public Maze generate(int x, int y) {
        Maze m = new Maze(x, y);

        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                m.addComponent(makeWall(), i, j);
            }
        }

        int j = y / 2;

        for (int i = 1; (i < (x - 1)) && (j > 0) && (j < (y - 1)); i++) {
            m.addComponent(makeRoom(), i, j);
        }

        return m;
    }

}

class BoxGenerator extends MazeGenerator {
    public Maze generate(int x, int y) {
        if (x < 3 || y < 3) {
            return null;
        }

        Maze m = new Maze(x, y);

        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                m.addComponent(makeWall(), i, j);
            }
        }

        for (int i = 1; i < (x - 1); i++) {
            m.addComponent(makeRoom(), i, 1);
            m.addComponent(makeRoom(), i, y-2);
        }

        for (int j = 1; j < (y - 1); j++) {
            m.addComponent(makeRoom(), 1, j);
            m.addComponent(makeRoom(), x-2, j);
        }

        return m;
    }
}

class Maze {

    private MazeComponent[][] map;
    private int length;
    private int height;

    public Maze(int x, int y) {
        map = new MazeComponent[x][y];
        length = x;
        height = y;
    }

    public void addComponent(MazeComponent c, int x, int y) {
        if (!(x >= length || y >= height || x < 0 || y < 0)) {
            map[x][y] = c;
        }
    }

    public void printMaze() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < length; x++) {
                System.out.print(map[x][y].getSymbol());
            }
            System.out.println("");
        }
    }
}

class Room implements MazeComponent {
    
    public boolean isSolid() {
        return false;
    }

    public char getSymbol() {
        return '.';
    }
}

class Wall implements MazeComponent {
    
    public boolean isSolid() {
        return true;
    }

    public char getSymbol() {
        return '#';
    }
}
