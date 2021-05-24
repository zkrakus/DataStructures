using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinaryTree
    {
        public class TreeNode
        {
            public TreeNode(int value)
            {
                this.value = value;
            }

            public int value;
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        public TreeNode Root = null;

        public TreeNode Insert(int value)
        {
            if (Root == null)
                Root = new TreeNode(value);
            else if (value <= Root.value)
                Root.Left = Insert(value, Root.Left);
            else
                Root.Right = Insert(value, Root.Right);

            return Root;
        }

        private TreeNode Insert(int value, TreeNode parent)
        {
            if (parent == null)
                return new TreeNode(value);
            if (value <= parent.value)
                parent.Left = Insert(value, parent.Left);
            else
                parent.Right = Insert(value, parent.Right);

            return parent;
        }

        public List<TreeNode> InOrderTraversal()
        {
            List<TreeNode> nodeList = new List<TreeNode>();
            if (Root == null)
                return null;

            InOrderTraversal(nodeList, Root);

            return nodeList;
        }

        private void InOrderTraversal(List<TreeNode> nodeList, TreeNode parent)
        {
            if (parent.Left != null)
                InOrderTraversal(nodeList, parent.Left);

            nodeList.Add(parent);

            if (parent.Right != null)
                InOrderTraversal(nodeList, parent.Right);
        }

        public void Delete(int value)
        {
            if (Root == null)
                return;

            Root = Delete(value, Root);
        }

        private TreeNode Delete(int value, TreeNode parent)
        {
            // Found Value
            if (parent.value == value)
            {
                // Leaf Node
                if (parent.Left != null && parent.Right != null)
                {
                    TreeNode iop = FindInOrderPredecessor(parent);
                    parent.value = iop.value;
                    parent.Left = Delete(iop.value, parent.Left);
                    return parent;
                }
                else if (parent.Left != null)
                {
                    return parent.Left;
                }
                else if (parent.Right != null)
                {
                    return parent.Right;
                }
                else
                {
                    return null;
                }
            }
            // Keep Searching
            else if (value < parent.value)
                parent.Left = Delete(value, parent.Left);
            else if (value > parent.value)
                parent.Right = Delete(value, parent.Right);

            return parent;
        }

        public TreeNode Search(int value)
        {
            if (Root == null)
                return null;

            return Search(value, Root);
        }

        private TreeNode Search(int value, TreeNode parent)
        {
            if (parent == null)
                return null;

            if (parent.value == value)
                return parent;
            else if (value <= parent.value)
                return Search(value, parent.Left);
            else
                return Search(value, parent.Right);
        }

        private TreeNode FindInOrderPredecessor(TreeNode parent)
        {
            TreeNode inOrderPredecessorNode = parent.Left;
            if (parent.Left.Right == null)
                return inOrderPredecessorNode;

            while (inOrderPredecessorNode.Right != null)
                inOrderPredecessorNode = inOrderPredecessorNode.Right;

            return inOrderPredecessorNode;
        }
    }
}
