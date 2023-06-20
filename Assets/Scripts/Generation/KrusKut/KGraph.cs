using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGraph 
{
    private List<KVertex> vertex;
    private List<KEdge> edges;

    public KVertex[] Vertex => vertex.ToArray();
    public KEdge[] Edges => edges.ToArray();

    public KGraph(Vector2[] postions)
    {
        vertex = new List<KVertex>();
        for(int i = 0; i <postions.Length; i++)
        {
            KVertex vertex = new KVertex();
            vertex.centerPosition = postions[i];
            vertex.group = -1;
            this.vertex.Add(vertex);
        }

        edges = new List<KEdge>();

        for(int i = 0; i<vertex.Count; i++)
        {
            for( int j = i+1; j<vertex.Count; j++)
            {
                KEdge edge = new KEdge();
                edge.source = i;
                edge.destination = j;
                edge.weight = Vector2.Distance(vertex[i].centerPosition, vertex[j].centerPosition);
                edges.Add(edge);
            }
        }

        edges.Sort(delegate (KEdge x, KEdge y) {
            if (x.weight > y.weight) { return 1; }
            if (x.weight < y.weight) { return -1; }
            else { return 0; }
        });
    }

    public KEdge[] KrusKal()
    {
        List<KEdge> path = new List<KEdge>();
        List<KGroup> pathGroups = new List<KGroup>();

        for(int i = 0; i<edges.Count; i++)
        {
            KEdge edge = edges[i];
            KVertex source = vertex[edge.source];
            KVertex destination = vertex[edge.destination];

            if(source.group == -1  && destination.group == -1)
            {
                source.group = pathGroups.Count;
                destination.group = pathGroups.Count;
                vertex[edge.source] = source;
                vertex[edge.destination] = destination;

                KGroup group = new KGroup();
                group.vertex = new List<int>
                {
                    edge.source,
                    edge.destination
                };

                pathGroups.Add(group);
                path.Add(edges[i]);
                continue;
            }
            if(source.group == destination.group)
            {
                continue;
            }

            if(destination.group == -1)
            {
                int groups = source.group;
                int dg = edge.destination;
                destination.group = groups;
                vertex[dg] = destination;
                pathGroups[groups].vertex.Add(dg);
                path.Add(edges[i]);
                continue;

            }

            if(source.group == -1)
            {
                int groups = destination.group;
                int src = edge.source;
                source.group = groups;
                vertex[src] = source;
                pathGroups[groups].vertex.Add(src);
                path.Add(edges[i]);
                continue;
            }

            KGroup sourceGroup = pathGroups[source.group];
            KGroup destinationGroup = pathGroups[destination.group];

            for(int j = 0; j<destinationGroup.vertex.Count; j++)
            {
                int dgv = destinationGroup.vertex[j];
                KVertex v = vertex[dgv];
                v.group = source.group;
                vertex[dgv] = v;
                sourceGroup.vertex.Add(dgv);
            }
            pathGroups[source.group] = sourceGroup;
            path.Add(edges[i]);
        }

        return path.ToArray();
    }


}
