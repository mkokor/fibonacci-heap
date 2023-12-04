namespace FibonacciHeap.Tests
{
    public static class UniqueIdentifierFixture
    {
        public static void ResetUniqueIdentifier()
        {
            FibonacciHeap<int>.UniqueIdentifierGenerator = 0;
        }
    }
}