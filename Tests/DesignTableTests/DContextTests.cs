using DesignTable.Core;
using HEngine.Core;
using HEngine.Utility;

namespace DesignTableTests;

public class DContextTests
{
    [Test]
    public void TestDialogTable()
    {
        var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, "DContextTests.cs");
        if (null == filePath)
            throw new InvalidDataException($"not found app setting - DesignDataRoot");

        var tokens = filePath.Split("/");
        var dtRoot = $"{string.Join("/", tokens.Take(tokens.Length - 1))}/TestTableRoot/";
        var dt = new DContext(dtRoot);
        dt.Initialize();
        
        foreach (var section in dt.Tables)
        {
            foreach (var entry in section.All)
            {
                Console.WriteLine($"table({section.Name}) - id({entry.Id}) strId({entry.StrId})");
            }
        }

        var dlg = dt.Dialog.GetByStrId("dlg_sample");
        Console.WriteLine($"dlg({dlg.StrId}) type({dlg.Type})");
        foreach (var speech in dlg.Speeches)
        {
            Console.WriteLine($"-- {speech.Character} : {speech.Text} // emotion({speech.Emotion})");
        }
    }
}