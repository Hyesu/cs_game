using DesignTable.Core;
using DesignTable.Entry;
using DesignTable.Table;

namespace FeatureTest.Extensions;

public static class DesignTableExtensions
{
    public static DDialog RandomDialog(this DContext ctx)
    {
        var dlgTable = ctx.Get<DDialogTable>();
        var dlgs = dlgTable.All
            .Select(x => x as DDialog)
            .ToList();

        Assert.That(dlgs.Count, Is.GreaterThan(0), $"not found any dialog data");
        return dlgs.ElementAt(Random.Shared.Next(dlgs.Count));
    }
}