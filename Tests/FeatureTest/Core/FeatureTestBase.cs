using DesignTable.Core;
using HEngine;

namespace FeatureTest;

public class FeatureTestBase
{
    public readonly DContext D;

    public FeatureTestBase()
    {
        var config = HConfiguration.Instance();
        D = new DContext(config.DesignTableRoot);
        D.Initialize();
    }
}