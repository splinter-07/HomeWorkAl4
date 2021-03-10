using System;
using System.Collections.Generic;

namespace HomeWorkAl4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new Tree();
            list.AddItem(14);   //Корневой узел
            list.AddItem(2);    //LeftChild 14 - 2
            list.AddItem(1);    //LeftChild 2 - 1
            list.AddItem(4);    //RightChild 2 - 4
            list.AddItem(16);   //RightChild 14 - 16
            list.AddItem(15);   //LeftChild 16 - 15
            list.AddItem(27);   //RightChild 16 - 27
            list.AddItem(30);   //RightChild 27 - 30
            list.AddItem(18);   //LeftChild 27 - 18
            list.PrintTree();   //Вывод на консоль дерева, приложила скрин - 1

            list.RemoveItem(4); //Удаление элимента со значением 4
            list.PrintTree();   //Вывод на консоль дерева, приложила скрин - 2

            list.RemoveItem(16); //Удаление элемента со значением 16
            list.PrintTree();   //Вывод на консоль дерева, приложила скрин - 3

        }

    }

    public interface ITree
    {
        TreeNode GetRoot();
        TreeNode GetFreeNode(int value, TreeNode parent); //Возвращает корневой элемент
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
    }

    public class NodeInfo
    {
        public int Depth { get; set; }
        public TreeNode Node { get; set; }
    }
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public TreeNode Parent { get; set; }
        public override bool Equals(object obj)
        {
            var node = obj as TreeNode;

            if (node == null)
                return false;

            return node.Value == Value;

        }
    }
    public class Tree : ITree
    {
        public TreeNode Head { get; set; }
        public List<int> list = new List<int>();
        public void AddItem(int value) // добавить узел
        {
            TreeNode tmp = null;

            if (Head == null)
            {
                Head = GetFreeNode(value, null);
            }
            else
            {
                tmp = Head;
                
            }
            while (tmp != null)
                {
                    if (value > tmp.Value)
                    {
                        if (tmp.RightChild != null)
                        {
                            tmp = tmp.RightChild;
                            continue;
                        }
                        else
                        {
                            tmp.RightChild = GetFreeNode(value, tmp);
                        }
                    }
                    else if (value < tmp.Value)
                    {
                        if (tmp.LeftChild != null)
                        {
                            tmp = tmp.LeftChild;
                            continue;
                        }
                        else
                        {
                            tmp.LeftChild = GetFreeNode(value, tmp);
                        }
                    }
                    else
                    {
                        throw new Exception("Wrong tree state");                  // Дерево построено неправильно
                    }
                    return;
                }
        }
        public TreeNode GetFreeNode(int value, TreeNode parent)
        {
            return new TreeNode
            {
                Value = value,
                Parent = parent
            };
        }
        public void RemoveItem(int value)
        {
            TreeNode tree = GetNodeByValue(value);
            if (tree == null) return;
            TreeNode elem;

            if (tree.LeftChild == null && tree.RightChild == null && tree.Parent != null)
            {
                if (tree == tree.Parent.LeftChild)
                    tree.Parent.LeftChild = null;
                else
                {
                    tree.Parent.RightChild = null;
                }
                return;
            }

            if (tree.Value == value)
            {
                if (tree.RightChild != null)
                {
                    elem = tree.RightChild;
                }
                else elem = tree.LeftChild;

                while (elem.LeftChild != null)
                {
                    elem = elem.LeftChild;
                }
                var temp = elem.Value;
                this.RemoveItem(temp);
                tree.Value = temp;

                return;
            }
        }
        public TreeNode GetNodeByValue(int value)
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Value < value)
                {
                    currentNode = currentNode.RightChild;
                    continue;
                }
                if (currentNode.Value > value)
                {
                    currentNode = currentNode.LeftChild;
                    continue;
                }
                if (currentNode.Value == value)
                    return currentNode;
            }
            return null;
        }

        public void PrintTree()
        {
            Console.Clear();
            PrintTree(Head);
        }
        private int PrintTree(TreeNode node, int x = 0, int y = 0)
        {

            Console.SetCursorPosition(x, y);
            Console.Write(node.Value);

            var loc = y;

            if (node.RightChild != null)
            {
                Console.SetCursorPosition(x + 2, y);
                Console.Write("--");
                y = PrintTree(node.RightChild, x + 4, y);
            }

            if (node.LeftChild != null)
            {
                while (loc <= y)
                {
                    Console.SetCursorPosition(x, loc + 1);
                    Console.Write(" |");
                    loc++;
                }
                y = PrintTree(node.LeftChild, x, y + 2);
            }

            return y;
        }

        public TreeNode GetRoot()
        {
            return Head;
        }
    }


    public static class TreeHelper
    {
        public static NodeInfo[] GetTreeInLine(ITree tree)
        {
            var bufer = new Queue<NodeInfo>();
            var returnArray = new List<NodeInfo>();
            var root = new NodeInfo() { Node = tree.GetRoot() };
            bufer.Enqueue(root);

            while (bufer.Count != 0)
            {
                var element = bufer.Dequeue();
                returnArray.Add(element);

                var depth = element.Depth + 1;

                if (element.Node.LeftChild != null)
                {
                    var left = new NodeInfo()
                    {
                        Node = element.Node.LeftChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(left);
                }
                if (element.Node.RightChild != null)
                {
                    var right = new NodeInfo()
                    {
                        Node = element.Node.RightChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(right);
                }
            }

            return returnArray.ToArray();
        }

    }
}
