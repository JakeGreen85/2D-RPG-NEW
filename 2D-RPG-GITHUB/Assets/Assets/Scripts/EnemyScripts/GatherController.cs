using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherController : MonoBehaviour
{
    public int goldDrop;
    public int expDrop;
    public int maxHealth = 100;
    public int health;
    GameObject player;
    public GameObject lootBag;
    public string gatherType;
    bool dead = false;
    public Sprite deadSprite;
    // Start is called before the first frame update
    void Start()
    {
        // Set up variables when enemy is spawned
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Subtract health from enemy
    public void TakeDamage(int damage){
        health -= damage;

        // If enemy is dead
        if(health <= 0 && dead == false){
            // dead = true;

            // foreach(GameObject q in GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests){
            //     if(q != null){
            //         q.GetComponent<Quest>().CheckGather(gatherType);
            //     }
            // }

            // Death animation
            GetComponent<GatherController>().enabled = false;
            Instantiate(lootBag, transform.position, transform.rotation);

            // Drop loot
            player.GetComponent<PlayerController>().enemyDead(goldDrop, expDrop);

            // Start timer to spawn new tree
            Destroy(gameObject);
        }
    }
}
