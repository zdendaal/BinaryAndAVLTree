using Trees;
using Xunit;

namespace TreeTests
{
    public class BinaryTreeTests
    {
        [Fact]
        public void Add_FullTree_ShouldAddValuesInCorrectPositions()
        {
            // Arrange
            BinaryTreeArrayImpl<int> tree = new BinaryTreeArrayImpl<int>();

            // Act
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(2);
            tree.Add(4);
            tree.Add(6);
            tree.Add(8);

            // Assert
            Assert.Equal([ 5, 3, 7, 2, 4, 6, 8 ], tree.Tree);
        }

        [Fact]
        public void Add_DuplicateValues_ShouldNotAddDuplicates()
        {
            // Arrange
            BinaryTreeArrayImpl<int> tree = new BinaryTreeArrayImpl<int>();
            // Act
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(3); // duplicate
            tree.Add(7);
            tree.Add(5);
            tree.Add(3);
            // Assert
            Assert.Equal([ 5, 3, 7 ], tree.Tree);
        }

        [Fact]
        public void Contains_ExistingValue_ShouldReturnTrue()
        {
            // Arrange
            BinaryTreeArrayImpl<int> tree = new BinaryTreeArrayImpl<int>();
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            // Act
            bool result = tree.Contains(3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_RandomValues_ShouldAddValuesInCorrectPositions()
        {
            // Arrange
            BinaryTreeArrayImpl<int> tree = new BinaryTreeArrayImpl<int>();

            // Act
            tree.Add(10);
            tree.Add(5);
            tree.Add(15);
            tree.Add(3);
            tree.Add(7);
            tree.Add(12);
            tree.Add(18);
            tree.Add(-1);
            tree.Add(-500);
            tree.Add(120);

            // Assert
            List<int?> expected = new List<int?>();
            expected.AddRange([ 10, 5, 15, 3, 7, 12, 18, -1]);
            expected.AddRange(Enumerable.Repeat<int?>(null, 6).ToArray());
            expected.Add(120);
            expected.Add(-500);
            expected.AddRange(Enumerable.Repeat<int?>(null, 15));
            Assert.Equal(expected.ToArray(), tree.Tree);
        }
    }
}
