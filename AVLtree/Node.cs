using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Trees
{
    /// <summary>
    /// Node structure for AVL tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Node<T> where T : struct, INumber<T>
    {
        internal Node<T>? left;
        internal int leftDepth = 0;     // depth of the left subtree

        internal Node<T>? right;
        internal int rightDepth = 0;   // depth of the right subtree

        internal Node<T>? parent;

        internal T value;   // key

        public Node(T value)
        {
            this.value = value;
        }
    }
}
