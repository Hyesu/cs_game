namespace HFin.Cheat
{
    public readonly record struct CheatAction(
        string Command,
        string Description = "",
        string Usage = "",
        string Sample = ""
    );
}