using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Player movement variables
    float horizontalInput;
    public SceneManager SM;
    float verticalInput;
    public float speed = 5f;
    float offset = 0.05f;
    public Vector2 movementDir;
    public Vector2 mousePos;
    public Vector2 lookDir;
    public float nextAttack = 0f;
    public float attackSpeed = 2f;

    // Spawn position of the player object
    public Vector2 spawnPos;

    private Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public HealthBar healthbar;
    public HealthBar manabar;
    public HealthBar expbar;

    // Player stats
    public int health;
    public float attackRange = 1f;
    public int maxHealth = 100;
    public int mana;
    public int maxmana = 100;
    public int experience = 0;
    public int expToGo = 100;
    public int gold = 50;
    public int level = 1;
    public int atk = 20;

    public GameObject[] spells;
    public InventoryManager invManager;
    public GameObject mapCam;
    public GameManager GM;

    private static PlayerController _instance;
    public static PlayerController Instance{
        get{
            return _instance;
        }
    }

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }

    void Start()
    {
        // Set up some variables when launching the game
        expbar = GameObject.Find("ExpBar").GetComponent<HealthBar>();
        manabar = GameObject.Find("ManaBar").GetComponent<HealthBar>();
        healthbar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        GM = GameManager.Instance;
        // transform.position = spawnPos;
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthbar.SetMaxValue(maxHealth);
        healthbar.SetValue(health);
        mana = maxmana;
        manabar.SetMaxValue(maxmana);
        manabar.SetValue(mana);
        expbar.SetMaxValue(expToGo);
        expbar.SetValue(experience);
        invManager = InventoryManager.Instance;
        DontDestroyOnLoad(gameObject);
    }

    [System.Obsolete]
    void Update()
    {
        GetInput();

        // Check for objects in the current moving direction using raycasting
        int count = rb.Cast(
            movementDir,
            movementFilter,
            castCollisions,
            speed * Time.deltaTime + offset
        );
        // If there are no objects in the raycast above, then the player can move
        if (count == 0){
            MovePlayer();
        }

        // Play player animation
        GetComponent<PlayerAnimation>().MovePlayer(horizontalInput, verticalInput, lookDir);

        // If the player gains enough experience, he will level up
        if (experience >= expToGo){
            LevelUp();
        }
    }

    /// <summary>Gets all input from the player. Including: movement, abilities, interactions, bag, etc.</summary>
    [System.Obsolete]
    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        // MOVEMENT INPUTS - WORKS
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDir = new Vector2(horizontalInput, verticalInput).normalized;
        mousePos = new Vector2((Input.mousePosition.x - Screen.width/2), (Input.mousePosition.y - Screen.height/2));
        lookDir = mousePos / (new Vector2(Screen.width/2, Screen.height/2));

        // COMBAT INPUTS - WORKS
        if(Input.GetKeyDown(KeyCode.Alpha1) && mana > 0 && Time.time > nextAttack){
            UseAbility1();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            UseAbility2();
        }

        // Temporary test for health bar (TO BE REMOVED)
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            UseAbility3();
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            UseAbility4();
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            // Use health potion
        }

        if(Input.GetKeyDown(KeyCode.B)){
            InventoryManager.Instance.EnableInventory();
        }

        if(Input.GetKeyDown(KeyCode.C)){
            InventoryManager.Instance.EnableEquipment();
        }

        if(Input.GetKeyDown(KeyCode.M)){
            mapCam.SetActive(!mapCam.activeInHierarchy);
        }


        if(Input.GetKeyDown(KeyCode.E)){
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach(Collider2D col in cols){
                if(col.CompareTag("Chest") && col.GetComponent<Animator>().enabled == false){
                    // Loot chest
                    gold += col.GetComponent<ChestOpen>().OpenChest();

                    // Play open chest animation
                    col.GetComponent<Animator>().enabled = true;
                    // Disable the script
                    col.GetComponent<ChestOpen>().enabled = false;
                }
                if(col.CompareTag("Door")){
                    // Open door and go to the corresponding scene
                    Debug.Log("Door opened");
                    col.GetComponent<OpenDoor>().Enter();
                    transform.position = Vector3.zero;
                }
                if(col.CompareTag("EnemyLoot")){
                    // Loot the enemy
                    Debug.Log("Enemy looted");
                    col.GetComponent<LootEnemy>().DropLoot();
                }
                if(col.CompareTag("Sign")){
                    col.GetComponent<SignReader>().ReadText();
                }
                if(col.CompareTag("QuestGiver")){
                    col.GetComponent<QuestGiver>().InterAct();
                }
                if(col.CompareTag("Shop")){
                    GameManager.Instance.OpenShop();
                }
                if(col.CompareTag("Workshop")){
                    col.GetComponent<Workshop>().ToggleUI();
                }
                foreach(GameObject q in GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests){
                    if(q != null && col.tag != null){
                        q.GetComponent<Quest>().CheckObjective(col.gameObject);
                    }
                }
                
            }
        }
    }
 
    /// <summary>Moves the player according to the user input</summary>
    public void MovePlayer()
    {
        transform.Translate(movementDir * Time.deltaTime * speed);
    }

    /// <summary>Function for basic attack by the player. The function plays the animation, changes mana, checks for collisions with enemies, and if any, will subtract health from the enemy based on the player's attack stat. Also, checks for environment (trees, rocks, etc.)</summary>
    public void Attack(){
        // Play player attacking animation
        GetComponent<PlayerAnimation>().PlayerAttack();

        // Subtract energy
        mana-=10;
        manabar.SetValue(mana);

        // Check for enemies in range of the player
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Run through each collision detected in the OverlapCircleAll
        foreach(Collider2D col in cols){

            // Check if the collider has "enemy" tagged on the game object
            if(col.CompareTag("Enemy")){

                // Check if the enemy is in front of the player (180 degrees), using the dot product of the vector from the player to the enemy and the current look direction of the player
                if(Vector2.Dot(new Vector2(col.transform.position.x, col.transform.position.y) - new Vector2(transform.position.x, transform.position.y), lookDir)>0){

                    // The enemy takes damage based on player attack
                    col.GetComponent<EnemyController>().TakeDamage(atk);

                    // For future implementation of spells
                    // Instantiate(spells[0], col.transform.position, col.transform.rotation);
                }
            }

            if(col.CompareTag("Gather Material")){

                // Check if the material (tree) is in front of the player (180 degrees), using the dot product of the vector from the player to the material and the current look direction of the player
                if(Vector2.Dot(new Vector2(col.transform.position.x, col.transform.position.y) - new Vector2(transform.position.x, transform.position.y), lookDir)>0){

                    // The material takes damage based on player attack
                    col.GetComponent<GatherController>().TakeDamage(atk);
                }
            }
        }
    }

    /// <summary>Subtracts health from the player, and checks for death. Called from external source, when colliding with enemy</summary>
    public void TakeDamage(int damage){
        health -= damage;
        healthbar.SetValue(health);
        if(health <= 0){
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerAnimation>().PlayerDeath();
            Invoke("Die", 2f);
        }
    }
    /// <summary>Simple function for player death. Moves player back to spawn, and resets health and mana.</summary>
    void Die(){
        transform.position = spawnPos;
        health = maxHealth;
        mana = maxmana;
        healthbar.SetValue(health);
        manabar.SetValue(mana);
        GetComponent<PlayerController>().enabled = true;
    }

    /// <summary>Called when the player experience reaches the experience to go. Increases player level and attack stat</summary>
    public void LevelUp(){
        level++;
        experience = 0;
        expToGo += 50;
        atk *= 2;
        expbar.SetValue(experience);
        expbar.SetMaxValue(expToGo);
    }

    /// <summary>Gives player gold and experience from dead enemy. Called from external source</summary>
    public void enemyDead(int expGained, int goldGained){
        experience += expGained;
        expbar.SetValue(experience);
        gold += goldGained;
    }

    /// <summary>Virtual function for ability 1. Setup for future implementation of multiple classes</summary>
    public virtual void UseAbility1(){}
    /// <summary>Virtual function for ability 2. Setup for future implementation of multiple classes</summary>
    public virtual void UseAbility2(){}
    /// <summary>Virtual function for ability 3. Setup for future implementation of multiple classes</summary>
    public virtual void UseAbility3(){}
    /// <summary>Virtual function for ability 4. Setup for future implementation of multiple classes</summary>
    public virtual void UseAbility4(){}
}
