namespace FibonacciHeap.Test
{
    [TestClass]
    public class InsertionTest
    {
        [TestInitialize]
        public void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }

        [TestMethod]
        public void Insert_FirstNode_ReturnsCorrectMinimumElement()
        {
            FibonacciHeap<int> fibonacciHeap = new();
            fibonacciHeap.Insert(10);

            string actualResult = fibonacciHeap.GetMinimumDetails();
            string expectedResult = "Index: 1\nValue: 10\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Insert_NewMinimum_ReturnsCorrectMinimumElement()
        {
            FibonacciHeap<int> fibonacciHeap = new();
            fibonacciHeap.Insert(10);
            fibonacciHeap.Insert(1);

            string actualResult = fibonacciHeap.GetMinimumDetails();
            string expectedResult = "Index: 2\nValue: 1\nParent: null\nLeftSibling: 1\nRightSibling: 1\nDegree: 0\nChild: null\nMarked: false";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}