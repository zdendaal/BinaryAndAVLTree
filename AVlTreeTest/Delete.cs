using Xunit;
using Trees;

namespace DeleteValues
{
    /// <summary>
    /// Unit tests testing AVL tree behaviour on delete nodes
    /// </summary>
    public class Delete
    {
        [Fact]
        public void Delete_FullTree_ShouldBeValidAVL()
        {
            // Arrange
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
            tree.Add(new Node<int>(12));
            tree.Add(new Node<int>(13));
            tree.Add(new Node<int>(14));

            // Act
            tree.Delete(7); // root
            tree.Delete(10); // leaf in right branche
            tree.Delete(11); // right subtree root
            tree.Delete(14); // leaf in right branche, to see if depth propagation from leaf to root works correctly

            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Delete_NonExistingValue_ShouldNotChangeTree()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(new Node<int>(0));
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(2));
            // Act
            tree.Delete(5); // non-existing value
            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Delete_Root_ShouldBeValidAVL()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(new Node<int>(0));
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(2));
            // Act
            tree.Delete(0); // root
            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Delete_AllNodes_ShouldBeValidAVL()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(new Node<int>(0));
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(2));
            // Act
            tree.Delete(0);
            tree.Delete(2);
            tree.Delete(1);
            // Assert
            Assert.True(tree.IsValidAVL());
        }
    }
}
