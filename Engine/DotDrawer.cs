public class DotDrawer
{
    private readonly Dictionary<(int, int), (char, ConsoleColor)> map;

    public DotDrawer(ref Dictionary<(int, int), (char, ConsoleColor)> map)
    {
        this.map = map;
    }
    public void DrawDot(ConsoleColor c, int x, int y)
    {
        map[(x, y)] = ('☒', c);
    }
}