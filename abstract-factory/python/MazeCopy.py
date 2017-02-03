from abc import ABCMeta, abstractmethod
import time, sys

class MazeGame:

    def __init__(self, type):
        if type == "box":
            self.mazeGen = BoxGenerator()
        elif type == "line":
            self.mazeGen = LineGenerator()
        else:
            print("Wrong input.")
            sys.exit()

    def createMaze(self, x, y):
        return self.mazeGen.generate(x, y)


class Maze:

    def __init__(self, x, y):
        self.length = x
        self.height = y
        self.grid = []

        for i in range(x):
            self.grid.append([])
            for j in range(y):
                self.grid[i].append(0)

    def addComponent(self, component, x, y):
        try:
            self.grid[x][y] = component
        except:
            return

    def printMaze(self):
        for y in range(self.length):
            for x in range(self.height):
                print(self.grid[x][y].getSymbol(), end="")
            print("")

class MazeGenerator:
    __metaclass__ = ABCMeta

    @abstractmethod
    def generate(self, x, y): pass

    def makeWall(self):
        return Wall()

    def makeRoom(self):
        return Room()

class LineGenerator(MazeGenerator):

    def generate(self, x, y):
        m = Maze(x, y)

        for i in range(x):
            for j in range(y):
                m.addComponent(self.makeWall(), i, j)

        i = 1
        j = int(y / 2)

        while ((i < (x - 1)) and (j > 0) and (j < (y - 1))):
            m.addComponent(self.makeRoom(), i, j)
            i += 1

        return m

class BoxGenerator(MazeGenerator):
    
    def generate(self, x, y):
        m = Maze(x, y)

        if (x < 3) or (y < 3):
            return

        for i in range(x):
            for j in range(y):
                m.addComponent(self.makeWall(), i, j)

        for i in range(1, (x - 1)):
            m.addComponent(self.makeRoom(), i, 1)
            m.addComponent(self.makeRoom(), i, y-2)

        for i in range(1, (y - 1)):
            m.addComponent(self.makeRoom(), 1, i)
            m.addComponent(self.makeRoom(), x-2, i)

        return m

class Wall:

    def isSolid(self):
        return True

    def getSymbol(self):
        return '#'

class Room:
    
    def isSolid(self):
        return False

    def getSymbol(self):
        return '.'

def main():
    t1 = time.time()
    mg = MazeGame("box")
    m = mg.createMaze(500, 500)
    t2 = time.time() - t1
    print(t2)
    #m.printMaze()

if __name__ == "__main__":
    main()
