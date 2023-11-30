using HEngine.Core;
using DesignTable.Core;

Console.WriteLine("==================== Start Server ====================");

// load design table
var ddRoot = HConfiguration.Instance.DesignTableRoot;
if (null == ddRoot)
    throw new InvalidDataException($"not found app setting - DesignDataRoot");

var dt = new DContext(ddRoot);
dt.Initialize();

// test code
foreach (var section in dt.Sections)
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