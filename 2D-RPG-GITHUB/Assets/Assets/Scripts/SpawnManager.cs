using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    private bool trigger;
    private bool spawned;
    [SerializeField] int numEnemies;
    private GameObject player;
    public int xRange;
    public int yRange;

    System.Random rand = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
        trigger = true;
        spawned = true;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is close enough to the spawner (To not load too many game objects when starting the game)
        if (Vector3.Distance(player.transform.position, transform.position)<10)
        {
            trigger = true;
        }
        else
        {
            trigger = false;
            // spawned = false;
            // Despawn();
        }
        if (trigger && !spawned)
        {
            SpawnEnemies();
            spawned = true;
        }
    }

    // Spawns enemies in the game
    void SpawnEnemies()
    {
        int j = 0;
        for(int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemies[j], (transform.position + new Vector3(rand.Next(xRange), rand.Next(yRange), 0)), new Quaternion(0,0,0,0));
            j++;
            if (j >= enemies.Count)
            {
                j = 0;
            }
        }
    }

    // Despawns enemies when the player is too far away (Optimization)
    void Despawn()
    {
        // NOT IMPLEMENTED YET
    }
}
