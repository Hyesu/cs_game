using System.Numerics;
using HFin.Cell;

namespace FeatureTest;

public class HCellMatrix2DTests
{
    class TestCell : IHCell
    {
        public readonly Vector3 Center;

        public TestCell(Vector3 center)
        {
            Center = center;
        }

        public Vector3 GetCenter()
        {
            return Center;
        }

        public Vector2 GetCenter2D()
        {
            return new Vector2(Center.X, Center.Y);
        }
    }
    
    ////////////////////////////////////////
    [Test(Description = "6방향")]
    public void TestDirection6()
    {
        var matrix = new HCellMatrix2D<TestCell>(1, 6);
        var cellCenter = new TestCell(new(0f, 0f, 0f));
        var cell30 = new TestCell(new(1.299f, 0.75f, 0f));
        var cell90 = new TestCell(new(0f, 1.5f, 0f));
        var cell150 = new TestCell(new(-1.299f, 0.75f, 0));
        var cell210 = new TestCell(new(-1.299f, -0.75f, 0));
        var cell270 = new TestCell(new(0, -1.5f, 0));
        var cell330 = new TestCell(new(1.299f, -0.75f, 0));
        
        // 초기화 확인
        matrix.Add([cellCenter, cell30, cell90, cell150, cell210, cell270, cell330]);
        Assert.That(matrix.CellCount, Is.EqualTo(7));

        // 인접 체크
        var adjacentCenter = matrix.GetAllAdjacent(cellCenter).ToHashSet();
        Assert.That(adjacentCenter.Count, Is.EqualTo(6));
        Assert.That(adjacentCenter.Contains(cellCenter), Is.False);
        Assert.That(adjacentCenter.Contains(cell30), Is.True);
        Assert.That(adjacentCenter.Contains(cell90), Is.True);
        Assert.That(adjacentCenter.Contains(cell150), Is.True);
        Assert.That(adjacentCenter.Contains(cell210), Is.True);
        Assert.That(adjacentCenter.Contains(cell270), Is.True);
        Assert.That(adjacentCenter.Contains(cell330), Is.True);

        var adjacent30 = matrix.GetAllAdjacent(cell30).ToHashSet();
        Assert.That(adjacent30.Count, Is.EqualTo(3));
        Assert.That(adjacent30.Contains(cellCenter), Is.True);
        Assert.That(adjacent30.Contains(cell30), Is.False);
        Assert.That(adjacent30.Contains(cell90), Is.True);
        Assert.That(adjacent30.Contains(cell150), Is.False);
        Assert.That(adjacent30.Contains(cell210), Is.False);
        Assert.That(adjacent30.Contains(cell270), Is.False);
        Assert.That(adjacent30.Contains(cell330), Is.True);
        
        var adjacent90 = matrix.GetAllAdjacent(cell90).ToHashSet();
        Assert.That(adjacent90.Count, Is.EqualTo(3));
        Assert.That(adjacent90.Contains(cellCenter), Is.True);
        Assert.That(adjacent90.Contains(cell30), Is.True);
        Assert.That(adjacent90.Contains(cell90), Is.False);
        Assert.That(adjacent90.Contains(cell150), Is.True);
        Assert.That(adjacent90.Contains(cell210), Is.False);
        Assert.That(adjacent90.Contains(cell270), Is.False);
        Assert.That(adjacent90.Contains(cell330), Is.False);
        
        var adjacent150 = matrix.GetAllAdjacent(cell150).ToHashSet();
        Assert.That(adjacent150.Count, Is.EqualTo(3));
        Assert.That(adjacent150.Contains(cellCenter), Is.True);
        Assert.That(adjacent150.Contains(cell30), Is.False);
        Assert.That(adjacent150.Contains(cell90), Is.True);
        Assert.That(adjacent150.Contains(cell150), Is.False);
        Assert.That(adjacent150.Contains(cell210), Is.True);
        Assert.That(adjacent150.Contains(cell270), Is.False);
        Assert.That(adjacent150.Contains(cell330), Is.False);
        
        var adjacent210 = matrix.GetAllAdjacent(cell210).ToHashSet();
        Assert.That(adjacent210.Count, Is.EqualTo(3));
        Assert.That(adjacent210.Contains(cellCenter), Is.True);
        Assert.That(adjacent210.Contains(cell30), Is.False);
        Assert.That(adjacent210.Contains(cell90), Is.False);
        Assert.That(adjacent210.Contains(cell150), Is.True);
        Assert.That(adjacent210.Contains(cell210), Is.False);
        Assert.That(adjacent210.Contains(cell270), Is.True);
        Assert.That(adjacent210.Contains(cell330), Is.False);
        
        var adjacent270 = matrix.GetAllAdjacent(cell270).ToHashSet();
        Assert.That(adjacent270.Count, Is.EqualTo(3));
        Assert.That(adjacent270.Contains(cellCenter), Is.True);
        Assert.That(adjacent270.Contains(cell30), Is.False);
        Assert.That(adjacent270.Contains(cell90), Is.False);
        Assert.That(adjacent270.Contains(cell150), Is.False);
        Assert.That(adjacent270.Contains(cell210), Is.True);
        Assert.That(adjacent270.Contains(cell270), Is.False);
        Assert.That(adjacent270.Contains(cell330), Is.True);
        
        var adjacent330 = matrix.GetAllAdjacent(cell330).ToHashSet();
        Assert.That(adjacent330.Count, Is.EqualTo(3));
        Assert.That(adjacent330.Contains(cellCenter), Is.True);
        Assert.That(adjacent330.Contains(cell30), Is.True);
        Assert.That(adjacent330.Contains(cell90), Is.False);
        Assert.That(adjacent330.Contains(cell150), Is.False);
        Assert.That(adjacent330.Contains(cell210), Is.False);
        Assert.That(adjacent330.Contains(cell270), Is.True);
        Assert.That(adjacent330.Contains(cell330), Is.False);
        
        // 클리어
        matrix.Clear();
        Assert.That(matrix.CellCount, Is.Zero);
    }
}