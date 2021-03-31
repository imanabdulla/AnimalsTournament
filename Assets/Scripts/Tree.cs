using System.Collections.Generic;

class Tree<T> 
{
    TreeNode<T> root = null;
    List<TreeNode<T>> nodes = new List<TreeNode<T>>();

    public Tree(T value)
    {
        root = new TreeNode<T>(value, null);
        nodes.Add(root);
    }

    public int Count
    {
        get { return nodes.Count; }
    }

    public TreeNode<T> Root
    {
        get { return root; }
    }


    public bool AddNode(TreeNode<T> node)
    {
        if (node == null ||
            node.Parent == null ||
            !nodes.Contains(node.Parent))
        {
            return false;
        }
        else if (node.Parent.Children.Contains(node))
        {
            // node already a child of parent
            return false;
        }
        else
        {
            // add child as tree node and as a child to parent
            nodes.Add(node);
            return node.Parent.AddChild(node);
        }
    }
}

