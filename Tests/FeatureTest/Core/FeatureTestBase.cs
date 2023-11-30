using DesignTable.Core;
using HEngine;

namespace FeatureTest;

public class FeatureTestBase
{
    public readonly DContext D;

    public FeatureTestBase()
    {
        D = new DContext(HConfiguration.Instance.DesignTableRoot);
        D.Initialize();
    }
}