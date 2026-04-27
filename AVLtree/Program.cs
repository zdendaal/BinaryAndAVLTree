using System.Numerics;

namespace Trees{

    class Program
    {
        public static void Main(string[] args)
        {
            /*
            BinaryTreeArrayImpl<int> tree = new BinaryTreeArrayImpl<int>();
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(2);
            tree.Add(4);
            tree.Add(6);
            tree.Add(8);
            tree.Add(10);
            tree.Add(3);
            tree.Add(1);
            tree.Add(0);
            tree.Add(9);

            PrintBinaryArrayTree(tree);

            */

            AVLTree<int> tree = new AVLTree<int>();

            tree.Add(new Node<int>(0));
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(2));
            tree.Add(new Node<int>(3));
            tree.Add(new Node<int>(4));
            tree.Add(new Node<int>(5));
            tree.Add(new Node<int>(6));
            tree.Add(new Node<int>(7));
            tree.Add(new Node<int>(8));
            tree.Add(new Node<int>(9));
            tree.Add(new Node<int>(10));
            tree.Add(new Node<int>(11));

/*
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(3));
            tree.Add(new Node<int>(2));
*/
            PrintInOrder(tree.root);

            tree.Delete(10);

            PrintInOrder(tree.root);

            Console.ReadKey();
        }

        /// <summary>
        /// Prints tree with node in root node inOrder. Non recursive traversing is used to iterate throught all nodes of the tree.
        /// Throws ArgumentNullException if node is null.
        /// </summary>
        /// <typeparam name="T">key value</typeparam>
        /// <param name="node"></param>
        public static void PrintInOrder<T>(Node<T> node) where T : struct, INumber<T>
        {
            Node<T>? currentNodePtr = node;
            ArgumentNullException.ThrowIfNull(node);

            while (currentNodePtr != node.parent)
            {
                while (true)    // traversing down
                {
                    // currentNodePtr is null because of traversing up is stopped by top level while loop condition
                    while (currentNodePtr!.left is not null)    // traversing down
                        currentNodePtr = currentNodePtr.left;
                    Console.Write(" " + currentNodePtr.value);
                    if (currentNodePtr.right is not null)
                    {
                        currentNodePtr = currentNodePtr.right;    // traversing one node right
                        continue;
                    }
                    // else
                    break;
                }

                // traversing up
                T value;
                while (true)
                {
                    value = currentNodePtr.value;
                    currentNodePtr = currentNodePtr.parent;
                    if (currentNodePtr is null) break;

                    if (value < currentNodePtr.value)
                    {
                        Console.Write(" " + currentNodePtr.value);
                        if (currentNodePtr.right is not null)
                        {
                            currentNodePtr = currentNodePtr.right;
                            break;  // to continue with traversing down higher in top level while loop
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public static void PrintBinaryArrayTree<T>(BinaryTreeArrayImpl<T> tree) where T : struct, INumber<T>
        {
            for(int i = 0; i < tree.Count-1; i++)
            {
                Console.Write((tree.Tree[i] is null ? "null" : tree.Tree[i]) + ",");
            }
            Console.WriteLine((tree.Tree[tree.Count-1] is null ? "null" : tree.Tree[tree.Count-1]));
        }
    }
}
