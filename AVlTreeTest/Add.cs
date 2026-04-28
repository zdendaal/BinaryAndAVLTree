using Xunit;
using Trees;

namespace AddValues
{
    /// <summary>
    /// Unit tests, testing AVL tree on adding nodes
    /// </summary>
    public class Add
    {

        [Fact]
        public void Add_FullTree_ShouldBeValidAVL()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
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
            // Assert
            Assert.True(tree.IsValidAVL());
        }


        [Fact]
        public void Add_FullTree_AddValuesInDescendOrder_ShouldBeValidAVL()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
            tree.Add(new Node<int>(14));
            tree.Add(new Node<int>(13));
            tree.Add(new Node<int>(12));
            tree.Add(new Node<int>(11));
            tree.Add(new Node<int>(10));
            tree.Add(new Node<int>(9));
            tree.Add(new Node<int>(8));
            tree.Add(new Node<int>(7));
            tree.Add(new Node<int>(6));
            tree.Add(new Node<int>(5));
            tree.Add(new Node<int>(4));
            tree.Add(new Node<int>(3));
            tree.Add(new Node<int>(2));
            tree.Add(new Node<int>(1));
            tree.Add(new Node<int>(0));
            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Add_Root_IsValidAVL()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
            tree.Add(new Node<int>(0));
            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Add_DuplicateValues_ShouldNotAddDuplicates()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
            tree.Add(new Node<int>(0));
            tree.Add(new Node<int>(0)); // duplicate
            tree.Add(new Node<int>(0)); // duplicate
            tree.Add(new Node<int>(0)); // duplicate
            tree.Add(new Node<int>(0)); // duplicate
            // Assert
            Assert.True(tree.IsValidAVL());
        }

        [Fact]
        public void Add_Values_ShouldCointains()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
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
            // Assert
            for (int i = 0; i < 15; i++)
                Assert.Equal(i, tree.Find(i)?.GetValue);
        }

        [Fact]
        public void Check_InOrder_ShouldBeOrdered()
        {
            // Arrange
            AVLTree<int> tree = new AVLTree<int>();
            // Act
            for (int i = 0; i < 100; i++)
                tree.Add(new Node<int>(i));
            // Assert
            Assert.Equal(Enumerable.Range(0, 100).ToList(), tree.InOrder(tree.root));
        }
    }
}
