using System.Numerics;
using HEngine.Utility;
using HFin.Cell;

namespace FeatureTest;

public class HHexCellMatrixTests
{
    class TestCell : IHCell
    {
        public readonly Vector3 Center;
        public bool IsTraversable { get; set; } = true;

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
    [Test(Description = "초기화와 인접 셀 빌드")]
    public void TestAdjacentCells()
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

    [Test(Description = "길찾기")]
    public void TestFindPath()
    {
        // 중심 기준으로 6방향 연결된 셀들
        List<TestCell> connectedCells = [
            new(new(0f, 0f, 0f)),
            new(new(1.732f, 0, 0)),
            new(new(0.866f, 1.5f, 0)),
            new(new(-0.866f, 1.5f, 0)),
            new(new(-1.732f, 0, 0)),
            new(new(-0.866f, -1.5f, 0)),
            new(new(0.866f, -1.5f, 0))
        ];
        var unconnectedCell = new TestCell(new(50f, 50f, 0));
        var matrix = new HHexCellMatrix<TestCell>(1f);   
        matrix.Add(connectedCells);
        matrix.Add([unconnectedCell]);

        var srcCell = connectedCells.RandomElement();
        var connectedCell = connectedCells
            .Where(x => srcCell != x)
            .ToList()
            .RandomElement();
        
        // 연결되지 않은 셀에는 path를 찾을 수 없음
        var errResult = matrix.TryFindPath(srcCell.GetCenter2D(), unconnectedCell.GetCenter2D(), out _);
        Assert.That(errResult, Is.False);
        
        // 연결된 셀끼리는 path를 찾을 수 있음
        var result = matrix.TryFindPath(srcCell.GetCenter2D(), connectedCell.GetCenter2D(), out var path);
        Assert.That(result, Is.True);
        Assert.That(path, Is.Not.Null);
        
        // 첫번째 세그먼트는 출발지점, 마지막 세그먼트는 도착 지점
        Assert.That(path.Count, Is.GreaterThanOrEqualTo(2));
        
        var firstSegment = path.First();
        var lastSegment = path.Last();
        Assert.That(firstSegment, Is.EqualTo(srcCell.GetCenter2D()));
        Assert.That(lastSegment, Is.EqualTo(connectedCell.GetCenter2D()));
        
        // 각 세그먼트는 인접해있으므로, 거리가 지름 이내이다.
        for (int i = 0; i < path.Count - 1; i++)
        {
            var prevSegment = path[i];
            var nextSegment = path[i + 1];
            var distance = Vector2.Distance(prevSegment, nextSegment);

            Assert.That(distance, Is.LessThanOrEqualTo(2 * matrix.Radius));
        }
    }

    [Test(Description = "이동 불가 셀로 인한 경로 차단")]
    public void TestFindPath_BlockedByUntraversable()
    {
        // 셀 배치 (반지름 1f 기준):
        //   NW  NE
        //  W  C  E
        //   SW  SE
        var cellCenter = new TestCell(new(0f, 0f, 0f));
        var cellE = new TestCell(new(1.732f, 0, 0));
        var cellNe = new TestCell(new(0.866f, 1.5f, 0));
        var cellNw = new TestCell(new(-0.866f, 1.5f, 0));
        var cellW = new TestCell(new(-1.732f, 0, 0));
        var cellSw = new TestCell(new(-0.866f, -1.5f, 0));
        var cellSe = new TestCell(new(0.866f, -1.5f, 0));

        var matrix = new HHexCellMatrix<TestCell>(1f);
        matrix.Add([cellCenter, cellE, cellNe, cellNw, cellW, cellSw, cellSe]);

        // 도착지가 이동 불가인 경우 경로를 찾을 수 없음
        cellE.IsTraversable = false;
        var pathFoundToDst = matrix.TryFindPath(cellCenter.GetCenter2D(), cellE.GetCenter2D(), out _);
        Assert.That(pathFoundToDst, Is.False);

        // W → E 경로에서 중간 경로가 모두 이동 불가인 경우 경로를 찾을 수 없음
        cellE.IsTraversable = true;
        cellCenter.IsTraversable = false;
        cellNe.IsTraversable = false;
        cellSe.IsTraversable = false;
        var pathFoundViaDetour = matrix.TryFindPath(cellW.GetCenter2D(), cellE.GetCenter2D(), out _);
        Assert.That(pathFoundViaDetour, Is.False);
    }

    [Test(Description = "이동 불가 셀을 우회하는 경로 탐색")]
    public void TestFindPath_DetourAroundUntraversable()
    {
        // 셀 배치 (반지름 1f 기준):
        //   NW  NE
        //  W  C  E
        //   SW  SE
        var cellCenter = new TestCell(new(0f, 0f, 0f));
        var cellE = new TestCell(new(1.732f, 0, 0));
        var cellNe = new TestCell(new(0.866f, 1.5f, 0));
        var cellNw = new TestCell(new(-0.866f, 1.5f, 0));
        var cellW = new TestCell(new(-1.732f, 0, 0));
        var cellSw = new TestCell(new(-0.866f, -1.5f, 0));
        var cellSe = new TestCell(new(0.866f, -1.5f, 0));

        var matrix = new HHexCellMatrix<TestCell>(1f);
        matrix.Add([cellCenter, cellE, cellNe, cellNw, cellW, cellSw, cellSe]);

        // Center 는 이동 불가지만, NW → NE 를 통한 우회 경로 존재: W → NW → NE → E
        cellCenter.IsTraversable = false;
        var pathFound = matrix.TryFindPath(cellW.GetCenter2D(), cellE.GetCenter2D(), out var path);
        Assert.That(pathFound, Is.True);
        Assert.That(path.First(), Is.EqualTo(cellW.GetCenter2D()));
        Assert.That(path.Last(), Is.EqualTo(cellE.GetCenter2D()));

        // 경로 상에 이동 불가 셀의 좌표가 포함되지 않음
        foreach (var segment in path)
        {
            Assert.That(segment, Is.Not.EqualTo(cellCenter.GetCenter2D()));
        }
    }
}