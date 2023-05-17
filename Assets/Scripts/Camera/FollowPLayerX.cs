using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPLayerX : MonoBehaviour
{
    public GameObject player;
    float startZpos;
    float startYpos;
    // Start is called before the first frame update
    void Awake()
    {
         startZpos = transform.position.z;
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y ,startZpos);
    }
}
