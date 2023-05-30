using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShoots : MonoBehaviour
{

    [SerializeField] private int samples;

    private void Awake()
    {
        Vector2[] positions = Generate();
        for(int i  = 0; i<positions.Length-1; i++)
        {
            Vector2 start = positions[i];
            Vector2 end = positions[i+1];
            Debug.DrawLine(start, end, Color.magenta, 10);
        }
    }
    public Vector2[] Generate()
    {
        Vector2[] result = new Vector2[samples];

        float angle = (Mathf.PI * 2) / samples;
        for(int i = 0; i<samples; i++)
        {
            float a = i * angle;

            Vector2 position = new Vector2();
            position.x = Mathf.Sin(a);
            position.y = Mathf.Cos(a);
            result[i] = position;
        }

        return result;
    }
}
