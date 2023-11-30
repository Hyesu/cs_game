using HEngine.Core;

namespace HEngineTest;

public class HConfigurationTests
{
    [Test]
    public void TestInit()
    {
        var configuration = HConfiguration.Instance;
        Assert.That(configuration, Is.Not.Null);
        Assert.That(string.IsNullOrEmpty(configuration.DesignTableRoot), Is.False);
    }
}