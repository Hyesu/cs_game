using HEngine.Core;

namespace HEngineTest;

public class HConfigurationTests
{
    [Test]
    public void TestInit()
    {
        Assert.That(HConfiguration.Shared, Is.Not.Null);
        
        HConfiguration.Shared.Init();
        Assert.That(string.IsNullOrEmpty(HConfiguration.Shared.DesignTableRoot), Is.False);
    }
}