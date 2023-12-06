namespace FibonacciHeap.Tests
{
    [TestClass]
    public class KeyDecreaseTest
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void DecreaseKey_NewValueLessThanMinimum_ReturnsCorrectMinimum()
        {
            FibonacciHeap<int> heap = new();
            int[] values = new int[] { 10, 22, 13, 4, 13, 12 };
            foreach (int value in values)
                heap.Insert(value);
            heap.ExtractMinimum();

            heap.DecreaseKey(22, 3);

            string actualResult = heap.GetMinimumDetails();
            string expectedResult = "Index: 2\nValue: 3\nParent: null\nLeftSibling: 6\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}