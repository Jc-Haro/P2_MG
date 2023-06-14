using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partitioning : MonoBehaviour
{
    [SerializeField] private int minNodeLevel;
    [SerializeField] private int maxNodeLevel;
    [SerializeField] private float probability;

    [SerializeField] private Partitioning partitioning;
    [SerializeField] private Vector2 size;


    private void Start()
    {
        Rectangle canvas = new Rectangle(transform.position, size);

        Node<Rectangle> root = new Node<Rectangle>(canvas, 0);

        Generate(root);

        List<Node<Rectangle>> leaves = new();
        root.Leaves(leaves);

        for(int i = 0; i< leaves.Count; i++)
        {
            leaves[i].Data.Draw(Color.green, 10);
        }
    }
    public void Generate(Node<Rectangle> node)
    {
        if (node.Level >= maxNodeLevel)
        {
            return;
        }
        if (node.Level >= minNodeLevel && Random.value>probability)
        {
            return;
        }
        Vector2 size = node.Data.Size;
        Vector2 origin = node.Data.Origin;
        Vector2 half = size / 2.0f;

        Rectangle bottomLeft = new Rectangle(origin, half);
        Rectangle bottomRight = new Rectangle(new Vector2(origin.x + half.x,origin.y), half);
        Rectangle topLeft = new Rectangle(new Vector2(origin.x, origin.y + half.y), half);
        Rectangle topRight = new Rectangle(origin + half, half);

        node.BottomLeft = new Node<Rectangle>(bottomLeft, node.Level + 1);
        node.BottomRight = new Node<Rectangle>(bottomRight, node.Level + 1);
        node.TopLeft = new Node<Rectangle>(topLeft, node.Level + 1);
        node.TopRight = new Node<Rectangle>(topRight, node.Level + 1);


        Generate(node.BottomLeft);
        Generate(node.BottomRight);
        Generate(node.TopLeft);
        Generate(node.TopRight);
    }
}