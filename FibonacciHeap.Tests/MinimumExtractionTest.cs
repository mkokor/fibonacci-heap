namespace FibonacciHeap.Tests
{
    [TestClass]
    public class MinimumExtractionTest
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void ExtractMinumum_EmptyHeap_ThrowsNullReferenceException()
        {
            FibonacciHeap<int> heap = new();

            Assert.ThrowsException<NullReferenceException>(() => heap.ExtractMinimum());
        }


        [TestMethod]
        public void ExtractMinumum_ConsolidationNotRequired_SetsCorrectMinimum()
        {
            FibonacciHeap<int> heap = new();
            heap.Insert(10);
            heap.Insert(11);

            heap.ExtractMinimum();

            string minimumDetailsExpected = "Index: 2\nValue: 11\nParent: null\nLeftSibling: 2\nRightSibling: 2\nDegree: 0\nChild: null\nMarked: false";
            string minimumDetailsActual = heap.GetMinimumDetails();

            Assert.AreEqual(minimumDetailsExpected, minimumDetailsActual);
        }

        [TestMethod]
        public void ExtractMinumum_ConsolidationRequired_SetsCorrectMinimum()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 1, 4, 20, 90, 9, 4, 3, 0, 100 };
            foreach (int value in values)
                heap.Insert(value);

            heap.ExtractMinimum();

            string minimumDetailsExpected = "Index: 1\nValue: 1\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 3\nChild: 2\nMarked: false";
            string minimumDetailsActual = heap.GetMinimumDetails();

            Assert.AreEqual(minimumDetailsExpected, minimumDetailsActual);
        }

        [TestMethod]
        public void ExtractMinumum_MultipleTreesRemaining_SetsCorrectMinimum()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 1, 4, 20, 90 };
            foreach (int value in values)
                heap.Insert(value);

            heap.ExtractMinimum();

            string minimumDetailsExpected = "Index: 2\nValue: 4\nParent: null\nLeftSibling: 4\nRightSibling: 4\nDegree: 1\nChild: 3\nMarked: false";
            string minimumDetailsActual = heap.GetMinimumDetails();

            Assert.AreEqual(minimumDetailsExpected, minimumDetailsActual);
        }
    }
}