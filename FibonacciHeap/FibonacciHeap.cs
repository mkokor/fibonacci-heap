namespace FibonacciHeap
{
    public class FibonacciHeap<TKeyValue> where TKeyValue : IComparable
    {
        #region NodeClass
        internal class Node
        {
            internal int Index { get; set; } // This is unique identifier for every node.
            internal TKeyValue KeyValue { get; set; }
            internal Node? Parent { get; set; } // Root nodes does not have parent so this property needs to be nullable.
            internal Node? LeftSibling { get; set; }
            internal Node? RightSibling { get; set; }
            internal int Degree { get; set; }
            internal Node? Child { get; set; }
            internal bool Marked { get; set; }

            public Node(TKeyValue keyValue)
            {
                // Node will be created as childless root.
                // Siblings refrences should be explicitly set after the constructor call.
                KeyValue = keyValue;
            }

            public override string ToString()
            {
                string parent = Parent == null ? "null" : $"{Parent.Index}";
                string child = Child == null ? "null" : $"{Child.Index}";
                string marked = Marked == true ? "true" : "false";
                return $"Index: {Index}\nValue: {KeyValue}\nParent: {parent}\nLeftSibling: {LeftSibling!.Index}\nRightSibling: {RightSibling!.Index}\nDegree: {Degree}\nChild: {child}\nMarked: {marked}";
            }
        }
        #endregion

        private Node? minimum; // When heap is empty this attribute is null (so it has to be nullable).
        private int size; // This attribute will contain current number of nodes.
        private int uniqueIdentifierGenerator;

        #region ComparingGenericValues
        private static bool IsLessThan(TKeyValue firstOperand, TKeyValue secondOperand)
        {
            _ = firstOperand ?? throw new ArgumentNullException(paramName: nameof(firstOperand));
            _ = secondOperand ?? throw new ArgumentNullException(paramName: nameof(secondOperand));
            return firstOperand?.CompareTo(secondOperand) < 0;
        }

        private static bool AreEqual(TKeyValue firstOperand, TKeyValue secondOperand)
        {
            _ = firstOperand ?? throw new ArgumentNullException(nameof(firstOperand));
            _ = secondOperand ?? throw new ArgumentNullException(nameof(secondOperand));
            return firstOperand?.CompareTo(secondOperand) == 0;
        }
        #endregion

        public void Insert(TKeyValue value)
        {
            Node newNode = new(value)
            {
                Index = ++uniqueIdentifierGenerator
            };
            if (minimum == null)
            {
                minimum = newNode;
                newNode.LeftSibling = newNode;
                newNode.RightSibling = newNode;
            }
            else
            {
                Node lastNode = minimum.LeftSibling!;
                lastNode.RightSibling = newNode;
                newNode.LeftSibling = lastNode;
                newNode.RightSibling = minimum;
                minimum.LeftSibling = newNode;
                if (IsLessThan(newNode.KeyValue, minimum.KeyValue))
                    minimum = newNode;
            }
            size++;
        }

        public string GetMinimum()
        {
            if (minimum == null)
                return "The heap is empty.";
            return minimum.ToString();
        }
    }
}