using System.Collections.Generic;

class TreeNode<T> 
{
    T _value;
    TreeNode<T> parent;
    List<TreeNode<T>> children;

    public TreeNode(T value, TreeNode<T> parent)
    {
        this._value = value;
        this.parent = parent;
        children = new List<TreeNode<T>>();
    }

    public T Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public TreeNode<T> Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public IList<TreeNode<T>> Children
    {
        get { return children.AsReadOnly(); }
    }


    public bool AddChild(TreeNode<T> child)
    {
        // don't add duplicate children
        if (children.Contains(child))
        {
            return false;
        }
        else if (child == this)
        {
            // don't add self as child
            return false;
        }
        else
        {
            // add as child and add self as parent
            children.Add(child);
            child.Parent = this;
            return true;
        }
    }
}
