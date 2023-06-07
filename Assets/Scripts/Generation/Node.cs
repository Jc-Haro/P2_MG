using System.Collections;
using System.Collections.Generic;


public class Node<T>
{


    public T Data { get; private set; }
    public int Level { get; private set; }

    //Children Nodes 
   
    public Node<T> BottomLeft { get; set; }
    public Node<T> BottomRight { get; set; }
    public Node<T> TopLeft { get; set; }
    public Node<T> TopRight { get; set; }

    public Node(T data, int level)
    {
        Data = data;
        Level = level;
    }

    public void Leaves(List<Node<T>> leaves)
    {
        if(BottomLeft != null){ BottomLeft.Leaves(leaves); }
        if (BottomRight != null) { BottomRight.Leaves(leaves); }
        if (TopLeft != null) { TopLeft.Leaves(leaves); }
        if (TopRight != null) { TopRight.Leaves(leaves); }

        if(BottomLeft == null && BottomRight == null && TopLeft == null && TopRight == null)
        {
            leaves.Add(this);
        }
    }
}
