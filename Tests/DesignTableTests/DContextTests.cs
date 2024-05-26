using DesignTable.Core;
using HEngine.Core;
using HEngine.Utility;

namespace DesignTableTests;

public class DContextTests
{
    private DContext _ctx;

    [SetUp]
    public void OnSetUp()
    {
        var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, "DContextTests.cs");
        if (null == filePath)
            throw new InvalidDataException($"not found app setting - DesignDataRoot");

        var tokens = filePath.Split("/");
        var dtRoot = $"{string.Join("/", tokens.Take(tokens.Length - 1))}/TestTableRoot/";
        _ctx = new DContext(dtRoot);
        _ctx.Initialize();
    }
    
    ///////////////////////
    [Test]
    public void TestJsonTable()
    {
        foreach (var table in _ctx.Tables)
        {
            foreach (var entry in table.All)
            {
                Console.WriteLine($"table({table.Name}) - id({entry.Id}) strId({entry.StrId})");
            }
        }

        var dlg = _ctx.Dialog.GetByStrId("dlg_sample");
        Console.WriteLine($"dlg({dlg.StrId}) type({dlg.Type})");
        foreach (var speech in dlg.Speeches)
        {
            Console.WriteLine($"-- {speech.Character} : {speech.Text} // emotion({speech.Emotion})");
        }
    }
}