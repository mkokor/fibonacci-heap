namespace FibonacciHeap
{
    public class FibonacciHeap<TKeyValue> where TKeyValue : IComparable
    {
        #region NodeClass
        private class Node
        {
            public int Index { get; set; } // This is unique identifier for every node.
            public required TKeyValue KeyValue { get; set; }
            public Node? Parent { get; set; } // Root nodes does not have parent so this property needs to be nullable.
            public required Node LeftSibling { get; set; }
            public required Node RightSibling { get; set; }
            public int Degree { get; set; }
            public Node? Child { get; set; }
            public bool Marked { get; set; }

            public Node(TKeyValue keyValue, Node leftSibling, Node rightSibling)
            {
                // Node will be created as childless root.
                KeyValue = keyValue;
                LeftSibling = leftSibling;
                RightSibling = rightSibling;
            }
        }
        #endregion

        private Node? minimum; // When heap is empty this attribute is null (so it has to be nullable).
        private int size; // This attribute will contain current number of nodes.
        private int uniqueIdentifierGenerator;
    }
}