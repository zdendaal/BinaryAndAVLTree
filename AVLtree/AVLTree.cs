using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Trees
{
    /// <summary>
    /// Implementation of AVL tree for big data, values are not copied between nodes, but whole nodes are switched by references
    /// this is suitable for big data, but not for primitive types, because of reference overhead.
    /// But in this example we will use int as value type, to make it easier to test and debug. For real big data, you can replace it or add any objects to nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AVLTree<T> where T : struct, INumber<T>
    {
        public Node<T>? root;

        public AVLTree() { }

        public void Add(Node<T> node)
        {
            PlaceNode(node);
            var currentPtr = node.parent;
            Balance(currentPtr);
        }


        /// <summary>
        /// Places node to proper leaf in the tree, increments left and right depth of nodes on the way to leaf.
        /// </summary>
        /// <param name="node"></param>
        private void PlaceNode(Node<T> node)
        {
            if (root is null) {
                root = node;
                return;
            }

            var currentPtr = root;
            while (true)
            {
                if (node.value < currentPtr.value)
                {
                    currentPtr.leftDepth++;
                    if (currentPtr.left is null)
                    {
                        currentPtr.left = node;
                        node.parent = currentPtr;
                        return;
                    }
                    currentPtr = currentPtr.left;
                    continue;
                }
                else if (node.value > currentPtr.value)
                {
                    currentPtr.rightDepth++;
                    if(currentPtr.right is null)
                    {
                        currentPtr.right = node;
                        node.parent = currentPtr;
                        return;
                    }
                    currentPtr = currentPtr.right;
                    continue;
                }
                else
                    return; // tree already contains this value
            }
        }

        public void Balance(Node<T>? currentPtr)
        {
            while (currentPtr is not null)
            {
                if (currentPtr.leftDepth - currentPtr.rightDepth > 1) // left rotation
                {
                    if (currentPtr.left!.leftDepth - currentPtr.left.rightDepth >= 0)
                        currentPtr = RightRotation(currentPtr);
                    else
                        currentPtr = LRRotation(currentPtr);

                    currentPtr.parent?.leftDepth = Math.Max(currentPtr.rightDepth, currentPtr.leftDepth) + 1;
                    currentPtr.parent?.left = currentPtr;
                }
                else if (currentPtr.leftDepth - currentPtr.rightDepth < -1) // right rotation
                {
                    if (currentPtr.right!.leftDepth - currentPtr.right.rightDepth <= 0)
                        currentPtr = LeftRotation(currentPtr);
                    else
                        currentPtr = RLRotation(currentPtr);

                    currentPtr.parent?.rightDepth = Math.Max(currentPtr.rightDepth, currentPtr.leftDepth) + 1;
                    currentPtr.parent?.right = currentPtr;
                }
                currentPtr = currentPtr.parent;
            }
        }

        /// <summary>
        /// Does right rotation on given node, returns new root of subtree.
        /// </summary>
        /// <param name="node">node with bf to be balanced</param>
        /// <returns>new root of subtree</returns>
        private Node<T> RightRotation(Node<T> node)
        {
            Node<T>? nodeLeft = node.left;
            Node<T>? nodeLeftRight = nodeLeft?.right;

            node.leftDepth = nodeLeft?.rightDepth ?? 0;

            nodeLeft!.parent = node.parent;
            if (nodeLeft.parent is null) root = nodeLeft;
            nodeLeft.right = node;

            node.left = nodeLeftRight;
            node.parent = nodeLeft;
            node.leftDepth = (nodeLeftRight is not null) ? Math.Max(nodeLeftRight.rightDepth, nodeLeftRight.leftDepth) + 1 : 0;

            nodeLeft.rightDepth = Math.Max(node.leftDepth, node.rightDepth) + 1;

            nodeLeftRight?.parent = node;

            return nodeLeft;
        }

        /// <summary>
        /// Left-Right rotation. First left, then right. Returns new root of subtree.
        /// </summary>
        /// <param name="node">node with bf to be balanced</param>
        /// <returns>returns new root of subtree</returns>
        private Node<T> LRRotation(Node<T> node)
        {
            node.left = LeftRotation(node.left);
            node.leftDepth = Math.Max(node.left.rightDepth, node.left.leftDepth) + 1;

            return RightRotation(node);
        }

        /// <summary>
        /// Left rotation from given node, returns new root of subtree.
        /// </summary>
        /// <param name="node">node with bf to be balanced</param>
        /// <returns>new root of subtree</returns>
        private Node<T> LeftRotation(Node<T> node)
        {
            Node<T>? nodeRight = node.right;
            Node<T>? nodeRightLeft = nodeRight?.left;

            node.rightDepth = nodeRight?.leftDepth ?? 0;

            nodeRight!.parent = node.parent;
            if (nodeRight.parent is null) root = nodeRight;
            nodeRight.left = node;

            node.right = nodeRightLeft;
            node.parent = nodeRight;
            node.rightDepth = (nodeRightLeft is not null) ? Math.Max(nodeRightLeft.rightDepth, nodeRightLeft.leftDepth) + 1 : 0;

            nodeRight.leftDepth = Math.Max(node.leftDepth, node.rightDepth) + 1;

            nodeRightLeft?.parent = node;

            return nodeRight;
        }

        /// <summary>
        /// Right-Left rotation of given node. First right, then left. Returns new root of subtree.
        /// </summary>
        /// <param name="node">node with bf to be balanced</param>
        /// <returns>returns new root of subtree</returns>
        private Node<T> RLRotation(Node<T> node)
        {
            node.right = RightRotation(node.right!);
            node.rightDepth = Math.Max(node.right.rightDepth, node.right.leftDepth) + 1;

            return LeftRotation(node);
        }

        /// <summary>
        /// Finds node in tree.
        /// </summary>
        /// <returns>Node if found, null if not</returns>
        public Node<T>? Find(T value)
        {
            if (root is null)
                return null;

            var currentPtr = root;
            while (true)
            {
                if (currentPtr.value == value)
                    return currentPtr;
                if (value < currentPtr.value)
                {
                    if (currentPtr.left is null)
                        return null;
                    currentPtr = currentPtr.left;
                }
                if (value > currentPtr.value)
                {
                    if (currentPtr.right is null)
                        return null;
                    currentPtr = currentPtr.right;
                }
            }
        }

        /// <summary>
        /// Deletes node from AVL tree by dereferencing from parent and replacing with child if needed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Delete(T value)
        {
            Node<T>? node = Find(value);
            if (node is null) return false;

            ref Node<T>? parentChildRef = ref node.parent!;
            if(parentChildRef is null)
                parentChildRef = ref root!;
            else if (parentChildRef.left == node)
                parentChildRef = ref parentChildRef.left!;
            else
                parentChildRef = ref parentChildRef.right!;

            // case with no children
            if (node.left is null && node.right is null)
            {
                parentChildRef = null;  // in case root is node, parent is not null (parentChildRef is not null)
                return true;
            }

            //case with one child
            if (node.left is null || node.right is null)
            {
                Node<T>? child = node.left ?? node.right;
                parentChildRef = child;
                if (child is not null)
                    child.parent = node.parent;
                node.parent = null;
                return true;
            }

            Node<T> replacement;
            // case with two children
            if (node.left.leftDepth - node.right.rightDepth > 0)
            {
                replacement = node.left;
                int i = 0;
                while (replacement.right is not null)
                {
                    replacement = replacement.right;
                    i++;
                }

                if (i == 0)
                {
                    parentChildRef = replacement;   // relation from parent to replacement
                    replacement.parent = node.parent;
                    //node.parent = null;
                    //node.left = null;
                    //node.right = null;
                    Balance(replacement);
                    return true;
                }
                else
                {
                    replacement.parent!.right = replacement.left;  // relation from replacement parent to replacement child
                    if (replacement.left is not null)
                        replacement.left.parent = replacement.parent;
                    replacement.left = node.left;
                    replacement.right = node.right;
                    if (replacement.left is not null)
                        replacement.left.parent = replacement;
                    if (replacement.right is not null)
                        replacement.right.parent = replacement;
                    parentChildRef = replacement;   // relation from parent to replacement
                    Node<T> rebalanceFrom = replacement.parent;
                    replacement.parent = node.parent;
                    //node.parent = null;
                    //node.left = null;
                    //node.right = null;
                    Balance(rebalanceFrom);
                    return true;

                }
            }
            else
            {
                replacement = node.right;
                int i = 0;
                while (replacement!.left is not null)
                {
                    replacement = replacement.left;
                    i++;
                }

                if (i == 0)
                {
                    parentChildRef = replacement;   // relation from parent to replacement
                    replacement.parent = node.parent;
                    //node.parent = null;
                    //node.left = null;
                    //node.right = null;
                    Balance(replacement);
                    return true;
                }
                else
                {
                    replacement.parent!.left = replacement.right;  // relation from replacement parent to replacement child
                    if (replacement.right is not null)
                        replacement.right.parent = replacement.parent;
                    replacement.right = node.right;
                    replacement.left = node.left;
                    if (replacement.left is not null)
                        replacement.left.parent = replacement;
                    if (replacement.right is not null)
                        replacement.right.parent = replacement;
                    parentChildRef = replacement;   // relation from parent or root (if root value is being deleted) to replacement
                    Node<T> rebalanceFrom = replacement.parent;
                    replacement.parent = node.parent;
                    //node.parent = null;
                    //node.left = null;
                    //node.right = null;
                    Balance(rebalanceFrom);
                    return true;

                }
            }
        }


        /// <summary>
        /// Cleares tree from all nodes
        /// </summary>
        public void Clear()
        {
            root = null;    // Garbage collector will take care of unreferenced nodes
        }
    }
}
