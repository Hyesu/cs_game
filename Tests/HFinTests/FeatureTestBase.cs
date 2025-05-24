using DesignTable.Core;
using HEngine.Utility;

namespace FeatureTest;

public class FeatureTestBase
{
    protected readonly DContext D;

    protected FeatureTestBase()
    {
        var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, $"{nameof(FeatureTestBase)}.cs");
        Assert.That(filePath, Is.Not.Null, "not found app setting - DesignDataRoot");

        filePath = filePath.Replace("\\", "/");
        var tokens = filePath.Split("/");
        var dtRoot = $"{string.Join("/", tokens.Take(tokens.Length - 1))}/TestTableRoot/";
        D = new DContext(dtRoot);
        D.Initialize(true);
    }
}