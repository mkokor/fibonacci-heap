namespace FibonacciHeap.Tests
{
    [TestClass]
    public class DeletionTest
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void Delete_OnlyMinimumInTheHeap_ReturnsCorrectMinimumDetailsMessage()
        {
            FibonacciHeap<int> heap = new();
            heap.Insert(10);
            heap.Delete(10);

            string expectedResult = "The heap is empty.";
            string actualResult = heap.GetMinimumDetails();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Delete_MultipleElementsInTheHeap_ReturnsCorrectSizeAndMinimum()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 10, 22, 13, 4, 13, 12 };
            foreach (int value in values)
                heap.Insert(value);
            heap.ExtractMinimum();
            heap.Delete(22);

            string expectedResult = "Index: 1\nValue: 10\nParent: null\nLeftSibling: 6\nRightSibling: 6\nDegree: 1\nChild: 5\nMarked: false";
            string actualResult = heap.GetMinimumDetails();

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsTrue(heap.GetSize() == 4);
        }

        [TestMethod]
        public void Delete_MultipleElementsInTheHeap_ReturnsCorrectMinimum()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 20, 1, 31, 10, 4 };
            foreach (int value in values)
                heap.Insert(value);
            heap.ExtractMinimum();
            heap.Delete(20);

            string expectedResult = "Index: 5\nValue: 4\nParent: null\nLeftSibling: 3\nRightSibling: 3\nDegree: 1\nChild: 4\nMarked: false";
            string actualResult = heap.GetMinimumDetails();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Delete_UnexistingElement_ReturnsCorrectSize()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 21, 98, 231, 0, 1, 4 };
            foreach (int value in values)
                heap.Insert(value);

            int sizeBefore = heap.GetSize();
            heap.Delete(999);
            int sizeAfter = heap.GetSize();

            Assert.IsTrue(sizeAfter == sizeBefore);
        }
    }
}