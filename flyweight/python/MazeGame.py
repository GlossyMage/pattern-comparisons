import random

class MazeGame:
    def __init__(self):
        self.componentFactory = ComponentFactory()

    def createMaze(self, x, y):
        m = self.makeMaze(x, y)

        for i in range(x):
            for j in range(y):
                m.addComponent(self.makeWall(), i, j)

        m.addComponent(self.makeRoom(), 1, 2)
        m.addComponent(self.makeRoom(), 2, 2)
        m.addComponent(self.makeRoom(), 3, 2)
        m.addComponent(self.makeRoom(), 1, 1)
        m.addComponent(self.makeRoom(), 3, 1)
        m.addComponent(self.makeRoom(), 1, 3)
        m.addComponent(self.makeRoom(), 3, 3)

        return m

    def makeMaze(self, x, y):
        return Maze(x, y)

    def makeRoom(self):
        return self.componentFactory.createComponent("coinroom")

    def makeWall(self):
        return self.componentFactory.createComponent("wall")

class Maze:
    def __init__(self, x, y):
        self.seed = random.randint(0, 5000)
        self.length = x
        self.height = y
        self.grid = []

        for i in range(x):
            self.grid.append([])
            for j in range(y):
               self.grid[i].append(0) 

    def addComponent(self, component, x, y):
        self.grid[x][y] = component

    def printMaze(self):
        context = MazeContext(self.seed)
        for y in range(self.length):
            for x in range(self.height):
                print(self.grid[x][y].getSymbol(context), end="")
                context.increment()
            print("")


class ComponentFactory:
    def __init__(self):
        self.flyweights = {} 

    def createComponent(self, componentType):
        if componentType not in self.flyweights:
            self.flyweights[componentType] = MazeComponent(componentType)

        return self.flyweights[componentType]

class MazeComponent:
    def __init__(self, ct):
        self.componentType = ct

    def isSolid(self):
        if self.componentType == "wall":
            return True
        else:
            return False

    def getSymbol(self, mazeContext):
        if self.componentType == "wall":
            return '#'
        elif self.componentType == "room":
            return '.'
        elif self.componentType == "coinroom":
            return '@' if mazeContext.hasCoin() else '.'
        else:
            return ' '

class MazeContext:
    def __init__(self, seed):
        self.seed = seed
        self.index = 0

    def increment(self):
        self.index += 1

    def hasCoin(self):
        random.seed(self.seed*self.index)
        coin = False
        if random.randint(0, 100) >= 75:
            coin = True
        
        return coin

def main():
   mg = MazeGame() 
   m = mg.createMaze(5, 5)
   m.printMaze()

if __name__ == "__main__":
    main()
