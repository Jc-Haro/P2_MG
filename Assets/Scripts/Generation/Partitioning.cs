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

    [SerializeField] private int margin;

    [SerializeField] private RoomGeneration roomGen;


    private void Start()
    {
        Rectangle canvas = new Rectangle(transform.position, size);

        Node<Rectangle> root = new Node<Rectangle>(canvas, 0);

        Generate(root);

        List<Node<Rectangle>> leaves = new();
        root.Leaves(leaves);

        for(int i = 0; i< leaves.Count; i++)
        {
            //leaves[i].Data.Draw(Color.blue, 10);
        }

        Vector2[] positions = new Vector2[leaves.Count];
        for(int i = 0; i<leaves.Count; i++)
        {
            positions[i] = leaves[i].Data.Center;
        }
        KGraph graph = new KGraph(positions);
        KEdge[] path = graph.KrusKal();

        for(int i = 0; i<path.Length; i++)
        {
            Vector2 src = graph.Vertex[path[i].source].centerPosition;
            Vector2 dst = graph.Vertex[path[i].destination].centerPosition;
           // Debug.DrawLine(src, dst, Color.white, 10);
        }

        int widht = (int)Mathf.Ceil(size.x);
        int heigth = (int)Mathf.Ceil(size.y);

        Grid<int> grid = new Grid<int>(transform.position, widht, heigth, 1.0f, -1);
        //grid.Draw(Color.blue, 10, -1);


        for(int i  = 0; i<path.Length; i++)
        {
            KEdge e = path[i];
            KVertex s = graph.Vertex[e.source];
            KVertex d = graph.Vertex[e.destination];

            grid.PointToIndex(s.centerPosition, out int sX, out int sY);
            grid.PointToIndex(d.centerPosition, out int dX, out int dY);

            float distance = Grid<bool>.Distance(sX,sY,dX,dY);
 while (distance > 0)
            {
                int dx = dX - sX;
                int dy = dY - sY;

                if(dx != 0)
                {
                    sX += dx / Mathf.Abs(dx);
                }
                else
                {
                    sY += dy / Mathf.Abs(dy);
                }
                grid.SetValue(1, sX, sY);
                distance = Grid<bool>.Distance(sX, sY, dX, dY);
            }
        }
           
        grid.Draw(Color.red, 10, 1);


        for (int i = 0; i < leaves.Count; i++)
        {
            Rectangle r = leaves[i].Data;

            Vector2 bl = r.Origin;
            Vector2 tr = r.Origin + r.Size;


            grid.PointToIndex(bl, out int blx, out int bly);
            grid.PointToIndex(tr, out int trx, out int @try);

            for (int x = blx + margin; x < trx - margin; x++)
            {
                for (int y = bly + margin; y < @try - margin; y++)
                {
                    grid.SetValue(0, x, y);
                }
            }
        }

        //grid.Draw(Color.green, 10, 0);

        roomGen.Generate(grid);
        
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
