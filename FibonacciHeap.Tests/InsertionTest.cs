namespace FibonacciHeap.Tests;

public class InsertionTest
{
    [Fact]
    public void Insert_FirstNode_CorrectMinimumElement()
    {
        FibonacciHeap<int> fibonacciHeap = new();

        fibonacciHeap.Insert(10);

        string actualResult = fibonacciHeap.GetMinimum();
        string expectedResult = $"Index: 1\nValue: 10\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";

        Assert.Equal(expectedResult, actualResult);
    }
}