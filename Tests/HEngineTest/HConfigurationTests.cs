using HEngine.Core;
using HEngine.Utility;

namespace HEngineTest;

public class HConfigurationTests
{
    [Test]
    public void TestInit()
    {
        Assert.That(HConfiguration.Shared, Is.Not.Null);
        
        var fileName = "setting.sample.json";
        var filePath = HPath.FindFilePathByRecursively(AppDomain.CurrentDomain.BaseDirectory, fileName);
        Assert.That(string.IsNullOrEmpty(filePath), Is.False, "샘플 파일 찾기 실패");
        
        HConfiguration.Shared.Init(filePath);
        Assert.That(string.IsNullOrEmpty(HConfiguration.Shared.DesignTableRoot), Is.False);
    }
}