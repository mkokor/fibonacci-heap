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

    }
}