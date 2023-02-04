
public class Tetromino
    {
        public Tetromino(ConsoleColor color, bool[,] body)
        {
            this.Body = body;
            this.Color = color;
        }

        public ConsoleColor Color {get; private set ;}
        public bool[,] Body { get; private set; }

        public int Width => this.Body.GetLength(0);

        public int Height => this.Body.GetLength(1);
        
        public Tetromino Rotate()
        {
            var newFigure = new bool[this.Height, this.Width];
            for (int row = 0; row < this.Width; row++)
            {
                for (int col = 0; col < this.Height; col++)
                {
                    newFigure[col, this.Width - row - 1] = this.Body[row, col];
                }
            }

            return new Tetromino(this.Color, newFigure);
        }
    }