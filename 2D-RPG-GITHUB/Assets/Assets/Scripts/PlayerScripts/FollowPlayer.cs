using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
