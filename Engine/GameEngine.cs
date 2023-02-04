public class GameEngine
{
    private Dictionary<(int, int), (char, ConsoleColor)> map;
    private DotDrawer shapeRender;
    private WASDInput input;
    private Game game;
    private System.Timers.Timer aTimer;
    public GameEngine(int width = 40, int height = 20, int interval = 400)
    {
        Console.TreatControlCAsInput = true;
        Console.CursorVisible = false;

        map = new Dictionary<(int, int), (char, ConsoleColor)>();
        shapeRender = new DotDrawer(ref map);
        input = new WASDInput();
        game = new Game(input, shapeRender);

        aTimer = new System.Timers.Timer();
        aTimer.Elapsed += delegate { RunGame(game, map, input); };
        aTimer.Interval = interval;
    }
    public void Run()
    {
        aTimer.Enabled = true;
    }

    private static void RunGame(Game game, Dictionary<(int, int), (char, ConsoleColor)> map, WASDInput kb)
    {
        if (!kb.AnyKeyDown && Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).KeyChar;
            if (key == 'w' || key == 'W')
                kb.WKeyDown = true;
            if (key == 'a' || key == 'A')
                kb.AKeyDown = true;
            if (key == 's' || key == 'S')
                kb.SKeyDown = true;
            if (key == 'd' || key == 'D')
                kb.DKeyDown = true;
        }
        game.UpdateGame();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        for (int j = 0; j < 14; j++)
        {
            Console.Write("         ║");
            if(j==2)
            {Console.Write("────────────");}
            else{
            for (int i = 0; i < 12; i++)
            {
                if (map.ContainsKey((i, j)))
                {
                    Console.ForegroundColor = map[(i, j)].Item2;
                    Console.Write(map[(i, j)].Item1);
                }
                else
                    Console.Write(" ");
            }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("║");
            Console.WriteLine();
        }
        Console.Write("         ╚════════════╝");
        map.Clear();
        kb.Clear();
    }
}