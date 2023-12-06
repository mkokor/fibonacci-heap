using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using FibonacciHeap.Exceptions;

namespace FibonacciHeap
{
    public class FibonacciHeap<TKeyValue> where TKeyValue : IComparable
    {
        #region NodeClass
        private class Node
        {
            public int Index { get; set; } // This is unique identifier for every node.
            public TKeyValue KeyValue { get; set; }
            public Node? Parent { get; set; } // Root nodes does not have parent so this property needs to be nullable.
            public Node? LeftSibling { get; set; }
            public Node? RightSibling { get; set; }
            public int Degree { get; set; }
            public Node? Child { get; set; }
            public bool Marked { get; set; }

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
        public static int UniqueIdentifierGenerator { get; set; } // This attribute is public only for testing purposes.

        #region HeapSearching
        private List<Node> GetBinomialTree(Node root)
        {
            List<Node> nodes = new();
            if (root is null)
                return nodes;
            nodes.Add(root);
            Node? child = root.Child;
            while (child is not null)
            {
                nodes.AddRange(GetBinomialTree(child));
                child = child.RightSibling;
            }
            return nodes;
        }

        private List<Node> GetAllNodes()
        {
            List<Node> nodes = new();
            Node? currentRoot = minimum;
            if (currentRoot is null)
                return nodes;
            do
            {
                nodes.AddRange(GetBinomialTree(currentRoot));
                currentRoot = currentRoot.RightSibling;
            } while (currentRoot!.Index != minimum!.Index);
            return nodes;
        }

        private Node GetNodeByValue(TKeyValue value)
        {
            return GetAllNodes().FirstOrDefault(node => AreEqual(node.KeyValue, value)) ?? throw new NotFoundException("Node with provided value could not be found.");
        }

        public List<TKeyValue> GetValues()
        {
            List<TKeyValue> values = GetAllNodes().Select(node => node.KeyValue).ToList();
            values.Sort();
            return values;
        }
        #endregion

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

        private static bool IsLessOrEqualTo(TKeyValue firstOperand, TKeyValue secondOperand)
        {
            return IsLessThan(firstOperand, secondOperand) || AreEqual(firstOperand, secondOperand);
        }
        #endregion

        #region Insertion
        private void Insert(TKeyValue keyValue, Node? child, int index = int.MinValue, bool updateMinimum = true)
        {
            if (int.MinValue == index)
                UniqueIdentifierGenerator++;
            Node newNode = new(keyValue)
            {
                Index = index == int.MinValue ? UniqueIdentifierGenerator : index,
                Child = child
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
                if (updateMinimum && IsLessThan(newNode.KeyValue, minimum.KeyValue))
                    minimum = newNode;
            }
            size++;
        }

        public void Insert(TKeyValue value)
        {
            Insert(value, null);
        }
        #endregion

        #region Union
        // This method returns first (minimum) and last (minimum.LeftSibling) element of the root list.
        private static Tuple<Node, Node> GetBorderRoots(FibonacciHeap<TKeyValue> fibonacciHeap)
        {
            if (fibonacciHeap.minimum is null) throw new ArgumentException("The heap is empty.");
            return new Tuple<Node, Node>(fibonacciHeap.minimum!, fibonacciHeap.minimum!.LeftSibling!);
        }

        private static void Destroy(FibonacciHeap<TKeyValue> fibonacciHeap)
        {
            fibonacciHeap.minimum = null;
            fibonacciHeap.size = 0;
            UniqueIdentifierGenerator = 0;
        }

        public static FibonacciHeap<TKeyValue> Unite(FibonacciHeap<TKeyValue> firstHeap, FibonacciHeap<TKeyValue> secondHeap)
        {
            if (firstHeap.minimum is null && secondHeap.minimum is null)
                return new FibonacciHeap<TKeyValue>();
            FibonacciHeap<TKeyValue> union = new()
            {
                minimum = firstHeap.minimum ?? secondHeap.minimum
            };
            if (firstHeap.minimum is not null && secondHeap.minimum is not null)
            {
                Tuple<Node, Node> unionBorders = FibonacciHeap<TKeyValue>.GetBorderRoots(union);
                Tuple<Node, Node> secondHeapBorders = FibonacciHeap<TKeyValue>.GetBorderRoots(secondHeap);
                unionBorders.Item2.RightSibling = secondHeapBorders.Item1;
                secondHeapBorders.Item1.LeftSibling = unionBorders.Item2;
                unionBorders.Item1.LeftSibling = secondHeapBorders.Item2;
                secondHeapBorders.Item2.RightSibling = unionBorders.Item1;
                union.minimum = IsLessThan(firstHeap.minimum.KeyValue, secondHeap.minimum.KeyValue) ? firstHeap.minimum : secondHeap.minimum;
            }
            union.size = firstHeap.size + secondHeap.size;
            FibonacciHeap<TKeyValue>.Destroy(firstHeap);
            FibonacciHeap<TKeyValue>.Destroy(secondHeap);
            return union;
        }
        #endregion

        #region MinimumExtraction
        private static void LinkTrees(Node firstTreeRoot, Node secondTreeRoot)
        {
            secondTreeRoot.LeftSibling!.RightSibling = secondTreeRoot.RightSibling;
            secondTreeRoot.RightSibling!.LeftSibling = secondTreeRoot.LeftSibling;
            secondTreeRoot.Parent = firstTreeRoot;
            if (firstTreeRoot.Child is null)
            {
                firstTreeRoot.Child = secondTreeRoot;
                secondTreeRoot.LeftSibling = secondTreeRoot;
                secondTreeRoot.RightSibling = secondTreeRoot;
            }
            else
            {
                Node lastChild = firstTreeRoot.Child.RightSibling!;
                secondTreeRoot.LeftSibling = firstTreeRoot.Child;
                secondTreeRoot.RightSibling = firstTreeRoot.Child.RightSibling;
                firstTreeRoot.Child.RightSibling = secondTreeRoot;
                lastChild.LeftSibling = secondTreeRoot;
            }
            firstTreeRoot.Degree++;
            secondTreeRoot.Marked = false;
        }

        private void Consolidate()
        {
            int uniqueTreesDegreeUpperBound = (int)Math.Log(size, (1 + Math.Sqrt(5)) / 2);
            Node?[] degreesArray = new Node[uniqueTreesDegreeUpperBound];
            List<int> visitedRoots = new();
            Node currentRoot = minimum!;
            while (true)
            {
                if (visitedRoots.Contains(currentRoot.Index))
                    break;
                visitedRoots.Add(currentRoot.Index);
                int degree = currentRoot.Degree;
                while (degreesArray[degree] is not null)
                {
                    Node degreeEquivalent = degreesArray[degree]!;
                    if (IsLessThan(degreeEquivalent.KeyValue, currentRoot.KeyValue))
                        (currentRoot, degreeEquivalent) = (degreeEquivalent, currentRoot);
                    LinkTrees(currentRoot, degreeEquivalent);
                    degreesArray[degree] = null;
                    degree++;
                }
                degreesArray[degree] = currentRoot;
                currentRoot = currentRoot.RightSibling!;
            }
            minimum = null;
            for (int i = 0; i < uniqueTreesDegreeUpperBound; i++)
                if (degreesArray[i] is null)
                    continue;
                else if (minimum is null)
                    minimum = degreesArray[i];
                else if (IsLessThan(degreesArray[i]!.KeyValue, minimum.KeyValue))
                    minimum = degreesArray[i];
        }

        public TKeyValue ExtractMinimum()
        {
            if (minimum is null) throw new NullReferenceException("The heap is empty.");
            Node criticalNode = minimum;
            Node? child = minimum.Child;
            while (child is not null)
            {
                Insert(child.KeyValue, child.Child, child.Index, false);
                child = child.RightSibling;
                if (child!.Index == minimum.Child!.Index) break;
            }
            if (criticalNode == criticalNode.RightSibling)
                minimum = null;
            else
            {
                criticalNode.LeftSibling!.RightSibling = criticalNode.RightSibling;
                criticalNode.RightSibling!.LeftSibling = criticalNode.LeftSibling;
                minimum = minimum.RightSibling;
                Consolidate();
            }
            size--;
            return criticalNode!.KeyValue;
        }
        #endregion

        #region KeyDecrease
        public void DecreaseKey(TKeyValue oldValue, TKeyValue newValue)
        {

        }
        #endregion
        // This method was made for testing purposes.
        public string GetMinimumDetails()
        {
            if (minimum == null)
                return "The heap is empty.";
            return minimum.ToString();
        }

        // This method returns number of nodes in the heap.
        public int GetSize()
        {
            return size;
        }
    }
}