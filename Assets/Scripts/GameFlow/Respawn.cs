using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    private Vector3 spawnPoint;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = spawnPoint;
    }
}
