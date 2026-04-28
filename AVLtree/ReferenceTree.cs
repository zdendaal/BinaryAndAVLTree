using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Trees
{
    public class ReferenceTree
    {
        /// <summary>
        /// Returns List<T> in in-order. Non recursive traversing is used to iterate throught all nodes of the tree.
        /// Throws ArgumentNullException if node is null.
        /// </summary>
        /// <typeparam name="T">Key value.</typeparam>
        /// <param name="node">Root node of the tree.</param>
        public List<T> InOrder<T>(Node<T> node) where T : struct, INumber<T>
        {
            ArgumentNullException.ThrowIfNull(node);
            Node<T>? currentNodePtr = node;
            List<T> result = new List<T>();

            while (currentNodePtr != node.parent)
            {
                while (true)    // traversing down
                {
                    // currentNodePtr is null because of traversing up is stopped by top level while loop condition
                    while (currentNodePtr!.left is not null)    // traversing down
                        currentNodePtr = currentNodePtr.left;
                    result.Add(currentNodePtr.value);
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
                        result.Add(currentNodePtr.value);
                        if (currentNodePtr.right is not null)
                        {
                            currentNodePtr = currentNodePtr.right;
                            break;  // to continue with traversing down higher in top level while loop
                        }
                    }
                }
            }

            return result;
        }
    }
}
