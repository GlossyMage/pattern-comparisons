import java.util.Random;

public class MazeGame {

    ComponentFactory cf;

    public MazeGame() {
        cf = new ComponentFactory();
    }

    public Maze createMaze() {
        Maze m = makeMaze(5, 5);

        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                m.addComponent(makeWall(), x, y);
            }
        }

        m.addComponent(makeRoom(), 1, 2);
        m.addComponent(makeRoom(), 2, 2);
        m.addComponent(makeRoom(), 3, 2);
        m.addComponent(makeRoom(), 1, 1);
        m.addComponent(makeRoom(), 3, 1);
        m.addComponent(makeRoom(), 1, 3);
        m.addComponent(makeRoom(), 3, 3);

        return m;
    }

    public Maze makeMaze(int x, int y) {
        return new Maze(x, y);
    }

    public MazeComponent makeRoom() {
        return cf.createComponent(ComponentType.COINROOM);
    }

    public MazeComponent makeWall() {
        return cf.createComponent(ComponentType.WALL);
    }

    public static void main(String[] args) {
        MazeGame mg = new MazeGame();
        Maze m = mg.createMaze();
        m.printMaze();
        m.printMaze();
    }
}

class Maze {
    private MazeComponent[][] map;
    private int length;
    private int height;
    private int seed;

    public Maze(int x, int y) {
        map = new MazeComponent[x][y];
        length = x;
        height = y;

        Random r = new Random();
        seed = r.nextInt();
    }

    public void addComponent(MazeComponent c, int x, int y) {
        if (!(x >= length || y >= height || x < 0 || y < 0)) {
            map[x][y] = c;
        }
    }

    public void printMaze() {
        MazeContext mc = new MazeContext(seed);

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < length; x++) {
                System.out.print(map[x][y].getSymbol(mc));
                mc.next();
            }
            System.out.println("");
        }
    }
}

class ComponentFactory {
    private MazeComponent[] flyweights;

    public ComponentFactory() {
        flyweights = new MazeComponent[ComponentType.values().length];
    }

    public MazeComponent createComponent(ComponentType ct) {
        if (null == flyweights[ct.ordinal()]) {
            flyweights[ct.ordinal()] = new MazeComponent(ct);
        }

        return flyweights[ct.ordinal()];
    }
}

class MazeComponent {

    ComponentType type;
    
    public MazeComponent(ComponentType ct) {
        this.type = ct;
    }

    public boolean isSolid() {
        return type == ComponentType.WALL ? true : false;
    }

    public char getSymbol(MazeContext mc) {
        switch (type) {
            case WALL:
                return '#';
            case ROOM:
                return '.';
            case COINROOM:
                return mc.hasCoin() ? '@' : '.';
            default:
                return ' ';
        }
    }
}

class MazeContext {
    private int seed;
    private int index = 0;

    public MazeContext(int seed) {
        this.seed = seed;
    }

    public void next() {
        index++;
    }

    public boolean hasCoin() {
        Random r = new Random(seed*index);
        if (r.nextInt(100) >= 75) {
            return true;
        } else {
            return false;
        }
    }
}
