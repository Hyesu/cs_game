using DesignTable.Core;
using HEngine.Utility;

namespace DesignTableTests;

public class DContextTests
{
    private DContext _ctx;

    [SetUp]
    public void OnSetUp()
    {
        var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, $"{nameof(DContextTests)}.cs");
        Assert.That(filePath, Is.Not.Null, "not found app setting - DesignDataRoot");

        filePath = filePath.Replace("\\", "/");
        var tokens = filePath.Split("/");
        var dtRoot = $"{string.Join("/", tokens.Take(tokens.Length - 1))}/TestTableRoot/";
        _ctx = new DContext(dtRoot);
        _ctx.Initialize(true);
    }
    
    ///////////////////////
    [Test]
    public void TestJsonTable()
    {
        foreach (var table in _ctx.Tables)
        {
            foreach (var entry in table.All)
            {
                TestContext.Out.WriteLine($"table({table.Name}) - id({entry.Id}) strId({entry.StrId})");
            }
        }

        var dlg = _ctx.Dialog.GetByStrId("dlg_sample");
        TestContext.Out.WriteLine($"dlg({dlg.StrId}) type({dlg.Type})");
        
        foreach (var speech in dlg.Speeches)
        {
            TestContext.Out.WriteLine($"-- {speech.Character} : {speech.Text} // emotion({speech.Emotion})");
        }
    }

    [Test]
    public void TestXmlTable()
    {
        Assert.That(_ctx.Sample.All.Count(), Is.GreaterThan(0));

        var sample = _ctx.Sample.Get(1); 
        Assert.That(sample, Is.Not.Null);
        Assert.That(sample.NumberField, Is.GreaterThan(0));
        Assert.That(sample.StringField, Is.Not.Null);
    }
}