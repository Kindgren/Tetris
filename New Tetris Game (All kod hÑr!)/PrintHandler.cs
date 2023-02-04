   public class PrintHandler
    {
        private int tetrisRows;
        private int tetrisColumns;
        private int consoleRows;
        private int consoleColumns;


        private DotDrawer render;
        public PrintHandler(DotDrawer render, int tetrisRows, int tetrisColumns)
      
        {
            
            this.render=render;
            this.tetrisRows = tetrisRows-1;
            this.tetrisColumns = tetrisColumns-1;
            this.consoleRows =   tetrisRows;
            this.consoleColumns =  tetrisColumns;



  
        }

        public void DrawAll(GameHandler gamehandler)
        {
           
          
            this.DrawTetrisField(gamehandler.TetrisField);      
            this.DrawCurrentFigure(gamehandler.CurrentFigure, gamehandler.CurrentFigureRow, gamehandler.CurrentFigureCol);



        }

public void DrawTetrisField(Dictionary<Tuple<int, int>, Tuple<bool, ConsoleColor>> TetrisField)
{
    foreach (var entry in TetrisField)
    {
        Tuple<int, int> position = entry.Key;
        Tuple<bool, ConsoleColor> value = entry.Value;

        if (value.Item1)
        {
            render.DrawDot(value.Item2, position.Item2, position.Item1);
        }
    }
}


        public void DrawCurrentFigure(Tetromino currentFigure, int currentFigureRow, int currentFigureColumn)
        {
            for (int row = 0; row < currentFigure.Width; row++)
            {
                for (int col = 0; col < currentFigure.Height; col++)
                {
                    if (currentFigure.Body[row, col])
                    {                                  
                        render.DrawDot(currentFigure.Color, col + currentFigureColumn, row + currentFigureRow);
                    }
                }
            }
        }
    
    }