namespace FibonacciHeap.Tests
{
    [TestClass]
    public class HeapSearching
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void GetValues_MultipleValues_ReturnsAllValues()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 1, 4, 20, 90, 9, 4, 3, 0, 100 };
            foreach (int value in values)
                heap.Insert(value);

            List<int> actualResult = heap.GetValues();
            actualResult.Sort();
            List<int> expectedResult = new() { 0, 1, 3, 4, 4, 9, 20, 90, 100 };

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}