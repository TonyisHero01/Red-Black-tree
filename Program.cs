using System;
using System.Collections.Generic;
using System.Linq;

namespace red_black_tree
{
    interface Set<T>
    {
        bool Insert(T val);        // Add a value.  Returns true if added, false if already present.
        bool Remove(T val);     // Remove a value.  Returns true if removed, false if not present.
        bool Contains(T val);   // Return true if a value is present in the set.
    }
    public class TreeSet<T> : Set<T> where T : IComparable
    {
        class Node
        {
            public T Value;
            public Node Parent;
            public Node LeftChild;
            public Node RightChild;
            public bool Color;
            public const bool RED = true;           // false = black, true = red
            public const bool BLACK = false;
            public Node(T val)
            {
                Value = val;
                Parent = null;
                LeftChild = null;
                RightChild = null;
                Color = BLACK;
            }
            public Node()
            {
                Color = BLACK;
            }
        }
        Node Root;
        private int count = 0;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private const bool LEFT = true;
        private const bool RIGHT = false;
        private Node rotateRight(Node node)
        {
            Node oldRoot = node;
            Node newRoot = oldRoot.LeftChild;
            Node middleNode = newRoot.RightChild;
            newRoot.LeftChild = middleNode;
            oldRoot.RightChild = oldRoot;
            if (oldRoot == Root)
            {
                Root = newRoot;
                Root.Parent = null;
            }
            else if (oldRoot.Parent.LeftChild == oldRoot)
            {
                oldRoot.Parent.LeftChild = newRoot;
                newRoot.Parent = oldRoot.Parent;
            }
            else
            {
                oldRoot.Parent.RightChild = newRoot;
                newRoot.Parent = oldRoot.Parent;
            }
            oldRoot.Parent = newRoot;
            if (middleNode != null)
            {
                middleNode.Parent = oldRoot;
            }
            return newRoot;
        }
        private Node rotateLeft(Node node)
        {
            Node oldRoot = node;
            Node newRoot = oldRoot.RightChild;
            Node middleNode = newRoot.LeftChild;
            newRoot.LeftChild = oldRoot;
            oldRoot.RightChild = middleNode;
            if (oldRoot == Root)
            {
                Root = newRoot;
                Root.Parent = null;
            }
            else if (oldRoot.Parent.LeftChild == oldRoot)
            {
                oldRoot.Parent.LeftChild = newRoot;
                newRoot.Parent = oldRoot.Parent;
            }
            else
            {
                oldRoot.Parent.RightChild = newRoot;
                newRoot.Parent = oldRoot.Parent;
            }
            oldRoot.Parent = newRoot;
            if (middleNode != null)
            {
                middleNode.Parent = oldRoot;
            }         
            return newRoot;
        }
        private Node rotateRightLeft(Node node)
        {
            node.RightChild = rotateRight(node.RightChild);
            Node newRoot = rotateLeft(node);
            return newRoot;
        }
        private Node rotateLeftRight(Node node)
        {
            node.LeftChild = rotateLeft(node.LeftChild);
            Node newRoot = rotateRight(node);
            return newRoot;
        }
        public bool Insert(T val)
        {
            if(Root == null)
            {
                Root = new Node(val);
                Count++;
                return true;
            }
            else
            {
                Stack<bool> positionsInTheParent = new Stack<bool>();
                Node node = new Node(val);
                node.Color = Node.RED;
                Node currentNode = Root;
                while (true)
                {
                    if(currentNode.Value.CompareTo(val) == 0)
                    {
                        return false;
                    }
                    if (currentNode.Value.CompareTo(val) == 1)
                    {
                        positionsInTheParent.Push(LEFT);
                        if (currentNode.LeftChild == null)
                        {
                            node.Parent = currentNode;
                            currentNode.LeftChild = node;
                            break;
                        }
                        currentNode = currentNode.LeftChild;
                    }
                    else
                    {
                        positionsInTheParent.Push(RIGHT);
                        if (currentNode.RightChild == null)
                        {
                            node.Parent = currentNode;
                            currentNode.RightChild = node;
                            break;
                        }
                        currentNode = currentNode.RightChild;
                    }
                }
                Count++;
                if (positionsInTheParent.Count >= 2)
                {
                    insertFixTree(node, positionsInTheParent);
                }
                Root.Color = Node.BLACK;
                return true;
            }
        }
        private void insertFixTree(Node node, Stack<bool> positionsInTheParent)
        {
            Node currentNode = node;
            Node grandParent;
            if (currentNode != Root && isRed(currentNode.Parent))       //kdyz jsou 2 cervene vrcholy za sebou, jinak muzeme nechat
            {
                grandParent = currentNode.Parent.Parent;
                Node uncle;
                if (currentNode.Parent == grandParent.LeftChild)
                {
                    uncle = grandParent.RightChild;
                }
                else
                {
                    uncle = grandParent.LeftChild;
                }
                bool positionInTheParent1 = positionsInTheParent.Pop();
                bool positionInTheParent2 = positionsInTheParent.Pop();
                if (uncle == null || isBlack(uncle))
                {
                    Node newRoot;
                    if (positionInTheParent1 && positionInTheParent2)
                    {
                        newRoot = rotateRight(grandParent);
                        newRoot.Color = !newRoot.Color;
                        newRoot.RightChild.Color = !newRoot.RightChild.Color;
                    }
                    else if (!positionInTheParent1 && !positionInTheParent2)
                    {
                        newRoot = rotateLeft(grandParent);
                        newRoot.Color = !newRoot.Color;
                        newRoot.LeftChild.Color = !newRoot.LeftChild.Color;
                    }
                    else if (positionInTheParent1 && !positionInTheParent2)
                    {
                        newRoot = rotateRightLeft(grandParent);
                        newRoot.Color = !newRoot.Color;
                        newRoot.LeftChild.Color = !newRoot.LeftChild.Color;
                    }
                    else
                    {
                        newRoot = rotateLeftRight(grandParent);
                        newRoot.Color = !newRoot.Color;
                        newRoot.RightChild.Color = !newRoot.RightChild.Color;
                    }
                }
                else
                {
                    uncle.Color = Node.BLACK;
                    currentNode.Parent.Color = Node.BLACK;
                    grandParent.Color = Node.RED;
                    currentNode = grandParent;
                    insertFixTree(currentNode, positionsInTheParent);
                }
            }
        }
        public bool Contains(T val)
        {
            Node currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(val)==1)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(val)==-1)
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T val)
        {
            Node nodeToRemove;
            Node currentNode = Root;
            while (true)
            {
                if (currentNode == null)
                {
                    return false;
                }
                if (currentNode.Value.CompareTo(val) == 1)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(val) == -1)
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    nodeToRemove = currentNode;
                    break;
                }
            }
            if (nodeToRemove == Root && nodeToRemove.LeftChild == null && nodeToRemove.RightChild == null)
            {
                Root = null;
                Count = 0;
                return true;
            }
            Node substitute;
            if (nodeToRemove.LeftChild != null && nodeToRemove.RightChild == null)
            {
                if (nodeToRemove == Root)
                {
                    Root = nodeToRemove.LeftChild;
                }
                else if (nodeToRemove.Parent.LeftChild == nodeToRemove)
                {
                    nodeToRemove.Parent.LeftChild = nodeToRemove.LeftChild;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = nodeToRemove.LeftChild;
                }
                nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;
                substitute = nodeToRemove.LeftChild;
            }
            else if (nodeToRemove.LeftChild == null && nodeToRemove.RightChild != null)
            {
                if (nodeToRemove == Root)
                {
                    Root = nodeToRemove.RightChild;
                }
                else if (nodeToRemove.Parent.LeftChild == nodeToRemove)
                {
                    nodeToRemove.Parent.LeftChild = nodeToRemove.RightChild;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = nodeToRemove.RightChild;
                }
                nodeToRemove.RightChild.Parent = nodeToRemove.Parent;
                substitute = nodeToRemove.RightChild;
            }
            else if (nodeToRemove.LeftChild == null && nodeToRemove.RightChild == null)
            {
                if (nodeToRemove == Root)
                {
                    Root = null;
                }
                else if (nodeToRemove.Parent.LeftChild == nodeToRemove)
                {
                    nodeToRemove.Parent.LeftChild = null;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = null;
                }
                substitute = null;
            }
            else
            {
                
                Node predecessor = findMax(nodeToRemove.LeftChild);
                nodeToRemove.Value = predecessor.Value;
                nodeToRemove = predecessor;
                nodeToRemove.Parent.RightChild = nodeToRemove.LeftChild;
                if (nodeToRemove.LeftChild != null)
                {
                    nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;
                }
                substitute = nodeToRemove.LeftChild;
            }
            if (nodeToRemove.Color == Node.BLACK)
            {
                removeFixTree(substitute, nodeToRemove.Parent);
            }
            Root.Color = Node.BLACK;
            Count--;
            return true;
        }
        private void removeFixTree(Node node, Node substituteParent)
        {
            if (node == Root || isRed(node))
            {
                node.Color = Node.BLACK;
                return;
            }
            Node doubleBlack = node;
            Node sibling;
            Node parent;
            if (node == null)
            {
                parent = substituteParent;
            }
            else
            {
                parent = doubleBlack.Parent;
            }
            if (parent.LeftChild == doubleBlack)
            {
                sibling = parent.RightChild;
                if (sibling.Color == Node.RED)
                {
                    sibling.Parent.Color = Node.RED;
                    sibling.Color = Node.BLACK;
                    rotateLeft(sibling.Parent);
                    sibling = parent.RightChild;
                }
                if (isBlack(sibling.LeftChild) && isBlack(sibling.RightChild))   //2
                {
                    sibling.Color = Node.RED;
                    if (parent.Color == Node.RED)
                    {
                        parent.Color = Node.BLACK;
                        return;
                    }
                    else 
                    {
                        removeFixTree(parent, substituteParent);
                    }
                }
                else
                {
                    if (isRed(sibling.LeftChild) && isBlack(sibling.RightChild)) //3
                    {
                        sibling = rotateRight(sibling);
                        sibling.Color = Node.BLACK;
                        sibling.RightChild.Color = Node.RED;
                    }
                    rotateLeft(sibling.Parent);
                    sibling.Color = sibling.LeftChild.Color;
                    sibling.RightChild.Color = Node.BLACK;
                    sibling.LeftChild.Color = Node.BLACK;
                    return;
                }
            }
            else
            {
                sibling = parent.LeftChild;
                if (sibling.Color == Node.RED)
                {
                    sibling.Parent.Color = Node.RED;
                    sibling.Color = Node.BLACK;
                    rotateRight(sibling.Parent);
                    sibling = parent.LeftChild;
                }
                if (isBlack(sibling.LeftChild) && isBlack(sibling.RightChild))   //2
                {
                    sibling.Color = Node.RED;
                    if (parent.Color == Node.RED)
                    {
                        parent.Color = Node.BLACK;
                        return;
                    }
                    else
                    {
                        removeFixTree(parent, substituteParent);
                    }
                }
                else
                {
                    if (isRed(sibling.RightChild) && isBlack(sibling.LeftChild)) //3
                    {
                        sibling = rotateLeft(sibling);
                        sibling.Color = Node.BLACK;
                        sibling.LeftChild.Color = Node.RED;
                    }
                    rotateRight(sibling.Parent);
                    sibling.Color = sibling.RightChild.Color;
                    sibling.LeftChild.Color = Node.BLACK;
                    sibling.RightChild.Color = Node.BLACK;
                    return;
                }
            }
        }
        private Node findMax(Node node)
        {
            while (node.RightChild != null)
            {
                node = node.RightChild;
            }
            return node;
        }
        private Node findMin(Node node)
        {
            while (node.LeftChild != null)
            {
                node = node.LeftChild;
            }
            return node;
        }
        private bool isBlack(Node node)
        {
            if (node == null || node.Color == Node.BLACK)
            {
                return true;
            }
            return false;
        }
        private bool isRed(Node node)
        {
            if (node != null && node.Color == Node.RED)
            {
                return true;
            }
            return false;
        }
        public T FindMax()
        {
            if (Root == null)
            {
                throw new Exception("Tree is empty");
            }
            Node currentNode = Root;
            while (currentNode.RightChild != null)
            {
                currentNode = currentNode.RightChild;
            }
            return currentNode.Value;
        }
        public T FindMin()
        {
            if (Root == null)
            {
                throw new Exception("Tree is empty");
            }
            Node currentNode = Root;
            while (currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }
            return currentNode.Value;
        }
        public T FindNext(T val)
        {
            Node currentNode = Root;
            Node rightParent = null;
            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(val) == 1)
                {
                    rightParent = currentNode;
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Value.CompareTo(val) == -1)
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                    if (currentNode != null)
                    {
                        currentNode = findMin(currentNode);
                        return currentNode.Value;
                    }
                    else
                    {
                        if (rightParent == null)
                        {
                            throw new Exception("Given value is the biggest value, it have not next value");
                        }
                        return rightParent.Value;
                    }
                    throw new Exception("Given value is the biggest value, it have not next value");
                }
            }
            throw new Exception("Cannot find given value");
        }
        private void printSubtree(Node node, int level)
        {
            if (node == null)
            {
                return;
            }
            printSubtree(node.RightChild, level + 1);
            string spaces = String.Concat(Enumerable.Repeat("  ", level));
            char color;
            if (node.Color == Node.BLACK)
            {
                color = 'B';
            }
            else
            {
                color = 'R';
            }
            Console.WriteLine(spaces + color + ' ' + node.Value.ToString());
            printSubtree(node.LeftChild, level + 1);
        }
        public void Print()
        {
            printSubtree(Root, 0);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeSet<int> tree = new TreeSet<int>();
            Console.WriteLine("Insert value 0:");
            tree.Insert(0);
            tree.Print();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Insert value 1 to 20:");
            for (int i = 1; i < 20; i++)
            {
                tree.Insert(i);
            }
            tree.Print();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Remove value 7:");
            tree.Remove(7);
            tree.Print();
            Console.WriteLine("-----------------------");
            Console.WriteLine("The maximum value:");
            int maxval = tree.FindMax();
            Console.WriteLine(maxval);
            Console.WriteLine("The minimum value:");
            int minval = tree.FindMin();
            Console.WriteLine(minval);
            Console.WriteLine("Next value:");
            int nextval = tree.FindNext(3);
            Console.WriteLine(nextval);
            Console.WriteLine("The number of vertices:");
            Console.WriteLine(tree.Count);
            Console.WriteLine(tree.Contains(5).ToString());
            Console.ReadLine();
        }
    }
}