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
    [SerializeField] private int numberOfRounds = 3;
    [SerializeField] float startShootingDelay = 0.5f;
    [SerializeField] float shootingRate = 0.5f;
    [SerializeField] float bulletRotation = -20;

    private void Awake()
    {
        InvokeRepeating("GenerateCircle", startShootingDelay, shootingRate);
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
        if (numberOfRounds > 0)
        {
            numberOfRounds--;
            Vector2[] positions = Generate();

            for (int i = 0; i < positions.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, positions[i], Quaternion.identity);
                Vector2 direction = positions[i] - (Vector2)transform.position;
                bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * bulletForce);
            }
            for (int i = 0; i < positions.Length - 1; i++)
            {
                Vector2 start = positions[i];
                Vector2 end = positions[i + 1];
                Debug.DrawLine(start, end, Color.magenta, 2);
            }

            offset -= bulletRotation;
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
}
