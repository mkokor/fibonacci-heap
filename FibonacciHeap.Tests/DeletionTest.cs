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
        public void Delete_OnlyMinimumInTheHeap()
        {
            FibonacciHeap<int> heap = new();
            heap.Insert(10);
            heap.Delete(10);

            string expectedResult = "The heap is empty.";
            string actualResult = heap.GetMinimumDetails();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}