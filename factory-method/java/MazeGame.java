import java.util.Random;

public class MazeGame {

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


    protected Maze makeMaze(int x, int y) {
        return new Maze(x, y);
    }
    
    protected Room makeRoom() {
        return new Room();
    }

    protected Wall makeWall() {
        return new Wall();
    }

    public static void main(String[] args) {
        MazeGame mg = null;
        if (args.length > 0) {
            if (args[0].equals("maze")) {
                mg = new MazeGame();
            } else if (args[0].equals("coin")) {
                mg = new CoinMaze();
            }
        } else {
            System.out.println("Usage: java MazeGame <coin|maze>");
            System.exit(0);
        }
        Maze m = mg.createMaze();
        m.printMaze();

    }
}

class CoinMaze extends MazeGame {
    
    Room makeRoom() {
        return new CoinRoom();
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
        for (int y = 0; y < 5; y++) {
            for (int x = 0; x < 5; x++) {
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

class CoinRoom extends Room {
    boolean coin;

    public CoinRoom() {
        Random r = new Random();
        if (r.nextInt(100) >= 75) {
            coin = true;
        }
    }

    public char getSymbol() {
        return coin ? '@' : '.';
    }
}
