namespace FibonacciHeap.Tests
{
    public class UnionTest
    {
        [Fact]
        public void Unite_EmptyHeaps_ReturnsEmptyHeap()
        {
            UniqueIdentifierFixture.ResetUniqueIdentifier();

            FibonacciHeap<int> firstHeap = new();
            FibonacciHeap<int> secondHeap = new();

            FibonacciHeap<int> union = FibonacciHeap<int>.Unite(firstHeap, secondHeap);
            Console.WriteLine(union.GetSize());
            Assert.True(union.GetSize() == 0);
        }

        [Fact]
        public void Unite_OneRootHeaps_ReturnsCorrectUnion()
        {
            UniqueIdentifierFixture.ResetUniqueIdentifier();

            FibonacciHeap<int> firstHeap = new();
            FibonacciHeap<int> secondHeap = new();
            firstHeap.Insert(19);
            firstHeap.Insert(18);

            FibonacciHeap<int> union = FibonacciHeap<int>.Unite(firstHeap, secondHeap);

            string minimumDetailsExpected = "Index: 2\nValue: 18\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";
            string minimumDetailsActual = union.GetMinimumDetails();

            Assert.True(union.GetSize() == 2);
            Assert.Equal(minimumDetailsExpected, minimumDetailsActual);
        }
    }
}