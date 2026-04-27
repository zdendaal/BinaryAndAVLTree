using System.Numerics;

namespace Trees
{
    /// <summary>
    /// Class representing binary tree by array of fixed size. Items cannot be deleted, only added
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTreeArrayImpl<T> where T : struct, INumber<T>
    {
        private T?[] tree;  // Array representing binary tree structure
        public int Count { get { return tree.Length; } }   // Number of elements in the tree
        public IReadOnlyList<T?> Tree { get { return tree; } }  // Readonly view of the tree array
        private readonly int defaultDepth = 0;  // initial depth of the tree
        private int depth;  // current depth of the tree
        private Func<int, int, int> newTreeSize = (depth, currentSize) => (int)Math.Pow(2, depth) + currentSize;    // returns new size of the tree based on current depth and size

        public BinaryTreeArrayImpl()
        {
            depth = defaultDepth;
            tree = new T?[newTreeSize(defaultDepth, 0)];  // 3 initial capacity
        }

        /// <summary>
        /// Adds value to the tree
        /// </summary>
        /// <param name="value">value to be added</param>
        public void Add(T value)
        {
            int i = 0;
            while (i < tree.Length) // iteration across the tree to find the right place for the new value
            {
                if (tree[i] == null)    // value is not in the tree, so we do not delete anything
                    break;
                else if (tree[i] > value)    // left subtree
                    i = 2 * i + 1;
                else if (tree[i] < value)   // right subtree
                    i = 2 * i + 2;
                else return;    // value is already un the tree, so we do not add it again
            }

            if (tree.Length <= i) {
                depth++;
                Array.Resize(ref tree, newTreeSize(depth, tree.Length));
            }
            tree[i] = value;
        }

        /// <summary>
        /// Iterates through tree and returns true if value exists or false if not
        /// </summary>
        /// <param name="value">searching value in the tree</param>
        /// <returns>true if exists, false if not</returns>
        public bool Contains(T value)
        {
            int i = 0;
            while (i < tree.Length) // iteration across the tree to find the right place for the new value
            {
                if (tree[i] == null)    // value is not in the tree, so we do not delete anything
                    break;
                else if (tree[i] > value)    // left subtree
                    i = 2 * i + 1;
                else if (tree[i] < value)   // right subtree
                    i = 2 * i + 2;
                else return true;    // value is already un the tree, so we do not add it again
            }
            return false;
        }

        /// <summary>
        /// Initializes new default tree, all values are deleted,  depth is set to default
        /// </summary>
        public void Clear()
        {
            depth = defaultDepth;
            tree = new T?[newTreeSize(defaultDepth, 0)];
        }
    }
}
