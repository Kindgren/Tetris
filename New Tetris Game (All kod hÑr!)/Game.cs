
public class Game
{

    private readonly WASDInput Kb;


    // TODO check this
    int tetrisRows = 14;
    int tetrisColumns = 12;

    GameHandler gameHandler;
    PrintHandler printHandler;


    public Game(WASDInput kb, DotDrawer render)
    {

        this.Kb = kb;

        this.gameHandler = new GameHandler(tetrisRows, tetrisColumns);
        this.printHandler = new PrintHandler(render, tetrisRows, tetrisColumns);

    }

    public void UpdateGame(ref int score)
    {

        bool drop=false;

        this.gameHandler.CurrentFigureRow++;



        if (Kb.WKeyDown)
        {
            var newFigure = this.gameHandler.CurrentFigure.Rotate();
            if (!this.gameHandler.Collision(newFigure))
            {
                this.gameHandler.CurrentFigure = newFigure;
                this.gameHandler.CurrentFigureRow--;
            }
        }
        else if (Kb.AKeyDown)
        {

            if (this.gameHandler.CanMoveToLeft())
            {
                this.gameHandler.CurrentFigureRow--;
                this.gameHandler.CurrentFigureCol--;

            }
        }

              else if (Kb.SKeyDown)
        {

                drop = true;
        }

        else if (Kb.DKeyDown)
        {

            if (this.gameHandler.CanMoveToRight())
            {
                this.gameHandler.CurrentFigureCol++;
                this.gameHandler.CurrentFigureRow--;
            }

        }

        if (this.gameHandler.Collision(this.gameHandler.CurrentFigure))
        {
            this.gameHandler.AddCurrentFigureToTetrisField();
            score+= this.gameHandler.CheckForFullLines()*5;
            score++;
            this.gameHandler.NewRandomFigure();

            if (this.gameHandler.Collision(this.gameHandler.CurrentFigure))
            {   
                this.printHandler.DrawAll(this.gameHandler);
                //game over
                Environment.Exit(0);

            }

        }
                


                
    if(drop){
     while(!this.gameHandler.Collision(this.gameHandler.CurrentFigure))
     {
        this.gameHandler.CurrentFigureRow++;

       
       

     }  

     this.gameHandler.AddCurrentFigureToTetrisField();
     score+= this.gameHandler.CheckForFullLines()*5;
     this.gameHandler.NewRandomFigure();
     drop = false;
     score++;
        
    }

        this.printHandler.DrawAll(this.gameHandler);
   

    }
    

}

