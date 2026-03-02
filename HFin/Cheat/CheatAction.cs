namespace HFin.Cheat
{
    public readonly struct CheatAction
    {
        public readonly string Command;
        public readonly string Description;
        public readonly string Usage;
        public readonly string Sample;

        public CheatAction(string command, string desc = "", string usage = "", string sample = "")
        {
            Command = command;
            Description = desc;
            Usage = usage;
            Sample = sample;
        }
    }
}