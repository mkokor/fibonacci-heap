namespace FibonacciHeap.Test
{
    [TestClass]
    public class UnionTest
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void Unite_EmptyHeaps_ReturnsEmptyHeap()
        {
            FibonacciHeap<int> firstHeap = new();
            FibonacciHeap<int> secondHeap = new();

            FibonacciHeap<int> union = FibonacciHeap<int>.Unite(firstHeap, secondHeap);

            Assert.IsTrue(union.GetSize() == 0);
        }

        [TestMethod]
        public void Unite_OneRootHeaps_ReturnsCorrectUnion()
        {
            FibonacciHeap<int> firstHeap = new();
            FibonacciHeap<int> secondHeap = new();
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
            firstHeap.Insert(19);
            firstHeap.Insert(18);

            FibonacciHeap<int> union = FibonacciHeap<int>.Unite(firstHeap, secondHeap);

            string minimumDetailsExpected = "Index: 2\nValue: 18\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";
            string minimumDetailsActual = union.GetMinimumDetails();

            Assert.IsTrue(union.GetSize() == 2);
            Assert.AreEqual(minimumDetailsExpected, minimumDetailsActual);
        }
    }
}