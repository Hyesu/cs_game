using System.Numerics;
using HFin.Cell;

namespace FeatureTest;

public class HHexCellMatrixTests
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
    [Test]
    public void Test()
    {
        var matrix = new HHexCellMatrix<TestCell>(1f);
        var cellCenter = new TestCell(new(0f, 0f, 0f));
        var cellE = new TestCell(new(1.732f, 0, 0));
        var cellNe = new TestCell(new(0.866f, 1.5f, 0));
        var cellNw = new TestCell(new(-0.866f, 1.5f, 0));
        var cellW = new TestCell(new(-1.732f, 0, 0));
        var cellSw = new TestCell(new(-0.866f, -1.5f, 0));
        var cellSe = new TestCell(new(0.866f, -1.5f, 0));
        
        // 초기화 확인
        matrix.Add([cellCenter, cellE, cellNe, cellNw, cellW, cellSw, cellSe]);
        Assert.That(matrix.CellCount, Is.EqualTo(7));

        // 인접 체크
        var adjacentCenter = matrix.GetAllAdjacent(cellCenter).ToHashSet();
        Assert.That(adjacentCenter.Count, Is.EqualTo(6));
        Assert.That(adjacentCenter.Contains(cellCenter), Is.False);
        Assert.That(adjacentCenter.Contains(cellE), Is.True);
        Assert.That(adjacentCenter.Contains(cellNe), Is.True);
        Assert.That(adjacentCenter.Contains(cellNw), Is.True);
        Assert.That(adjacentCenter.Contains(cellW), Is.True);
        Assert.That(adjacentCenter.Contains(cellSw), Is.True);
        Assert.That(adjacentCenter.Contains(cellSe), Is.True);

        var adjacentE = matrix.GetAllAdjacent(cellE).ToHashSet();
        Assert.That(adjacentE.Count, Is.EqualTo(3));
        Assert.That(adjacentE.Contains(cellCenter), Is.True);
        Assert.That(adjacentE.Contains(cellE), Is.False);
        Assert.That(adjacentE.Contains(cellNe), Is.True);
        Assert.That(adjacentE.Contains(cellNw), Is.False);
        Assert.That(adjacentE.Contains(cellW), Is.False);
        Assert.That(adjacentE.Contains(cellSw), Is.False);
        Assert.That(adjacentE.Contains(cellSe), Is.True);
        
        var adjacentNe = matrix.GetAllAdjacent(cellNe).ToHashSet();
        Assert.That(adjacentNe.Count, Is.EqualTo(3));
        Assert.That(adjacentNe.Contains(cellCenter), Is.True);
        Assert.That(adjacentNe.Contains(cellE), Is.True);
        Assert.That(adjacentNe.Contains(cellNe), Is.False);
        Assert.That(adjacentNe.Contains(cellNw), Is.True);
        Assert.That(adjacentNe.Contains(cellW), Is.False);
        Assert.That(adjacentNe.Contains(cellSw), Is.False);
        Assert.That(adjacentNe.Contains(cellSe), Is.False);
        
        var adjacentNw = matrix.GetAllAdjacent(cellNw).ToHashSet();
        Assert.That(adjacentNw.Count, Is.EqualTo(3));
        Assert.That(adjacentNw.Contains(cellCenter), Is.True);
        Assert.That(adjacentNw.Contains(cellE), Is.False);
        Assert.That(adjacentNw.Contains(cellNe), Is.True);
        Assert.That(adjacentNw.Contains(cellNw), Is.False);
        Assert.That(adjacentNw.Contains(cellW), Is.True);
        Assert.That(adjacentNw.Contains(cellSw), Is.False);
        Assert.That(adjacentNw.Contains(cellSe), Is.False);
        
        var adjacentW = matrix.GetAllAdjacent(cellW).ToHashSet();
        Assert.That(adjacentW.Count, Is.EqualTo(3));
        Assert.That(adjacentW.Contains(cellCenter), Is.True);
        Assert.That(adjacentW.Contains(cellE), Is.False);
        Assert.That(adjacentW.Contains(cellNe), Is.False);
        Assert.That(adjacentW.Contains(cellNw), Is.True);
        Assert.That(adjacentW.Contains(cellW), Is.False);
        Assert.That(adjacentW.Contains(cellSw), Is.True);
        Assert.That(adjacentW.Contains(cellSe), Is.False);
        
        var adjacentSw = matrix.GetAllAdjacent(cellSw).ToHashSet();
        Assert.That(adjacentSw.Count, Is.EqualTo(3));
        Assert.That(adjacentSw.Contains(cellCenter), Is.True);
        Assert.That(adjacentSw.Contains(cellE), Is.False);
        Assert.That(adjacentSw.Contains(cellNe), Is.False);
        Assert.That(adjacentSw.Contains(cellNw), Is.False);
        Assert.That(adjacentSw.Contains(cellW), Is.True);
        Assert.That(adjacentSw.Contains(cellSw), Is.False);
        Assert.That(adjacentSw.Contains(cellSe), Is.True);
        
        var adjacentSe = matrix.GetAllAdjacent(cellSe).ToHashSet();
        Assert.That(adjacentSe.Count, Is.EqualTo(3));
        Assert.That(adjacentSe.Contains(cellCenter), Is.True);
        Assert.That(adjacentSe.Contains(cellE), Is.True);
        Assert.That(adjacentSe.Contains(cellNe), Is.False);
        Assert.That(adjacentSe.Contains(cellNw), Is.False);
        Assert.That(adjacentSe.Contains(cellW), Is.False);
        Assert.That(adjacentSe.Contains(cellSw), Is.True);
        Assert.That(adjacentSe.Contains(cellSe), Is.False);
        
        // 클리어
        matrix.Clear();
        Assert.That(matrix.CellCount, Is.Zero);
    }
}