using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShoots : MonoBehaviour
{

    [SerializeField] private int samples;
    [SerializeField] private float yRadius=1;
    [SerializeField] private float xRadius=1;
    [SerializeField] private float circumference=1;
    [SerializeField] private float offset=1;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] float bulletForce; 

    private void Awake()
    {
        InvokeRepeating("GenerateCircle", 0.5f, 0.3f);
        GenerateCircle();  
    }
  
    public Vector2[] Generate()
    {
        Vector2[] result = new Vector2[samples];

        float angle = circumference / samples;
        for(int i = 0; i<samples; i++)
        {
            float a = i * angle;

            Vector2 position = new Vector2();
            position.x = Mathf.Sin(offset + a) * xRadius;
            position.y = Mathf.Cos(offset + a) * yRadius;
            position += (Vector2)transform.position;
            result[i] = position;
        }

        return result;
    }

    void GenerateCircle()
    {
        Vector2[] positions = Generate();

        for(int i  = 0; i <positions.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, positions[i], Quaternion.identity);
            Vector2 direction = (Vector2)transform.position - positions[i];
            bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized  * bulletForce);
        }
        for (int i = 0; i < positions.Length - 1; i++)
        {
            Vector2 start = positions[i];
            Vector2 end = positions[i + 1];
            Debug.DrawLine(start, end, Color.magenta, 2);
        }

        offset -= 10;
    }
}
