
public class GameHandler
{
    public static List<Tetromino> TetrisFigures = new List<Tetromino>()
            {
                new Tetromino(ConsoleColor.Magenta, new bool[,] // line
                {
                    { true, true, true, true }
                }),
                new Tetromino(ConsoleColor.DarkGray, new bool[,] // square
                {
                    { true, true },
                    { true, true }
                }),
                new Tetromino(ConsoleColor.White, new bool[,] // T
                {
                    { false, true, false },
                    { true, true, true },
                }),
                new Tetromino(ConsoleColor.Yellow, new bool[,] // S
                {
                    { false, true, true, },
                    { true, true, false, },
                }),
                new Tetromino(ConsoleColor.Blue, new bool[,] // Z
                {
                    { true, true, false },
                    { false, true, true },
                }),
                new Tetromino(ConsoleColor.Green, new bool[,] // J
                {
                    { true, false, false },
                    { true, true, true }
                }),
                new Tetromino(ConsoleColor.Red, new bool[,] // L
                {
                    { false, false, true },
                    { true, true, true }
                }),
            };


    public Tetromino CurrentFigure = TetrisFigures[6];

    public Dictionary<Tuple<int, int>, Tuple<bool, ConsoleColor>> TetrisField
    = new Dictionary<Tuple<int, int>, Tuple<bool, ConsoleColor>>();

    public int CurrentFigureRow;

    public int CurrentFigureCol;

    //public bool[,] TetrisField;

    public int TetrisRows;

    public int TetrisColumns;


    private Random random;

    public GameHandler(int tetrisRows, int tetrisColumns)
    {


        this.CurrentFigureRow = 0;
        this.CurrentFigureCol = 0;
        // this.TetrisField = new bool[tetrisRows, tetrisColumns];
        this.TetrisRows = tetrisRows;
        this.TetrisColumns = tetrisColumns;
        this.random = new Random();
        this.NewRandomFigure();

        for (int row = 0; row < tetrisRows; row++)
        {
            for (int col = 0; col < tetrisColumns; col++)
            {
                this.TetrisField[new Tuple<int, int>(row, col)] =
                    new Tuple<bool, ConsoleColor>(false, ConsoleColor.Black);
            }
        }


    }


    public void NewRandomFigure()
    {

        this.CurrentFigure = TetrisFigures[random.Next(0, 6)];
        this.CurrentFigureRow = 0;
        this.CurrentFigureCol = 5;
    }

    public void AddCurrentFigureToTetrisField()
    {
        for (int row = 0; row < this.CurrentFigure.Width; row++)
        {
            for (int col = 0; col < this.CurrentFigure.Height; col++)
            {
                if (this.CurrentFigure.Body[row, col])
                {
                    // this.TetrisField[this.CurrentFigureRow + row, this.CurrentFigureCol + col] = true;

                    this.TetrisField[new Tuple<int, int>(this.CurrentFigureRow + row, this.CurrentFigureCol + col)] =
                    new Tuple<bool, ConsoleColor>(true, this.CurrentFigure.Color);


                }
            }
        }
    }

    // TODO: This needs to be checked
    public int CheckForFullLines()
    {
        int lines = 0;

        for (int row = 0; row < this.TetrisRows; row++)
        {
            bool rowIsFull = true;
            for (int col = 0; col < this.TetrisColumns; col++)
            {
                Tuple<bool, ConsoleColor>? cell = null;
                if (!this.TetrisField.TryGetValue(Tuple.Create(row, col), out cell) || cell.Item1 == false)
                {
                    rowIsFull = false;
                    break;
                }
            }

            if (rowIsFull)
            {
                for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                {
                    for (int col = 0; col < this.TetrisColumns; col++)
                    {
                        this.TetrisField[Tuple.Create(rowToMove, col)] = this.TetrisField[Tuple.Create(rowToMove - 1, col)];
                    }
                }

                lines++;
            }
        }
        return lines;
    }


    //TODO: this is a bit buggy

    public bool Collision(Tetromino figure)
    {
        if (this.CurrentFigureCol > this.TetrisColumns - figure.Height)
        {
            return true;
        }

        if (this.CurrentFigureRow + figure.Width == this.TetrisRows)
        {
            return true;
        }

        for (int row = 0; row < figure.Width; row++)
        {
            for (int col = 0; col < figure.Height; col++)
            {
                if (figure.Body[row, col])
                {
                    var fieldKey = new Tuple<int, int>(this.CurrentFigureRow + row + 1, this.CurrentFigureCol + col);
                    if (this.TetrisField.ContainsKey(fieldKey) && this.TetrisField[fieldKey].Item1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    public bool CanMoveToLeft()
    {
        return (this.CurrentFigureCol >= 1 && !CheckForCollision(-1));
    }



    public bool CanMoveToRight()
    {

        return ((this.CurrentFigureCol < TetrisColumns - this.CurrentFigure.Height) && !CheckForCollision(1));

    }
    private bool CheckForCollision(int direction) //direction = -1 left, = 1 right
    {
        for (int row = 0; row < CurrentFigure.Width; row++)
        {
            for (int col = 0; col < CurrentFigure.Height; col++)
            {
                if (CurrentFigure.Body[row, col] &&
                this.TetrisField.ContainsKey(new Tuple<int, int>(this.CurrentFigureRow + row, this.CurrentFigureCol + col + direction)) &&
                this.TetrisField[new Tuple<int, int>(this.CurrentFigureRow + row, this.CurrentFigureCol + col + direction)].Item1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}             
    
