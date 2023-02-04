public class WASDInput
{
    public bool WKeyDown { get; set; }
    public bool AKeyDown { get; set; }
    public bool SKeyDown { get; set; }
    public bool DKeyDown { get; set; }
    public bool AnyKeyDown => WKeyDown || AKeyDown || SKeyDown || DKeyDown;
    public void Clear()
    {
        WKeyDown = false;
        AKeyDown = false;
        SKeyDown = false;
        DKeyDown = false;
    }
}