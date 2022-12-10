using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int goldDrop;
    public int expDrop;
    public int maxHealth = 100;
    public int health;
    public int attack;
    public float speed = 2f;
    public float nextAttack = 0f;
    public float attackSpeed = 1f;
    public float attackRange = 0.7f;
    private Animator enemyAnim;
    GameObject player;
    public GameObject lootBag;
    public string KillType;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        // Set up variables when enemy is spawned
        health = maxHealth;
        enemyAnim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position)<4 && Vector2.Distance(transform.position, player.transform.position)>0.5f){
            enemyAnim.SetFloat("Speed", 1f);
            transform.Translate(-(transform.position - Vector3.MoveTowards(transform.position, player.transform.position, 1)).normalized * speed * Time.deltaTime);
        }
        else{
            enemyAnim.SetFloat("Speed", 0f);
        }
        if (Vector2.Distance(transform.position, player.transform.position)<attackRange && Time.time > nextAttack){
            Attack();
            nextAttack = Time.time + (1/attackSpeed);
        }
    }

    void Attack(){
        player.GetComponent<PlayerController>().TakeDamage(20);
    }

    // Subtract health from enemy
    public void TakeDamage(int damage){
        health -= damage;

        // If enemy is dead
        if(health <= 0 && dead == false){
            dead = true;

            foreach(GameObject q in GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests){
                if(q != null){
                    q.GetComponent<Quest>().CheckKill(KillType);
                }
            }

            // Death animation
            enemyAnim.SetTrigger("Death");
            GetComponent<EnemyController>().enabled = false;
            Invoke("Die", 1);

            // Drop loot
            player.GetComponent<PlayerController>().enemyDead(goldDrop, expDrop);

            // Start timer to spawn new enemy
        }
        else{
            enemyAnim.SetTrigger("Hit");
        }
    }

    void Die(){
        Instantiate(lootBag, transform.position, transform.rotation);
        GameObject.Destroy(gameObject);
    }
}
