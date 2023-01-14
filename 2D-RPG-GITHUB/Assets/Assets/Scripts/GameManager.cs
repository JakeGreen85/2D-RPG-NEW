using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>Reference to player game object</summary>
    public GameObject player;
    /// <summary>Reference to the display of current gold owned by player. This is the text game object</summary>
    public GameObject goldDisplay;
    /// <summary>Reference to all the stats text gameobjects in the character UI</summary>
    public GameObject[] statsText;
    /// <summary>Reference to the UI for giving quests</summary>
    public GameObject QuestPrefab;
    /// <summary>Reference to the UI for completing quests</summary>
    public GameObject QuestComplete;
    /// <summary>Reference to workshop UI</summary>
    public GameObject WorkshopUI;
    /// <summary>Reference to the camera used to create the minimap</summary>
    public GameObject MapCam;
    /// <summary>Reference to the Gameobject that makes up the tooltip for equipment</summary>
    public GameObject ToolTipUI;
    /// <summary>Reference to the shop UI</summary>
    public GameObject ShopUI;
    /// <summary>Reference to the buttons in the shop (buying from the shop)</summary>
    public GameObject[] ShopButtons;
    
    /// <summary>Singleton instance of the Game Manager (private field)</summary>
    private static GameManager _instance;
    /// <summary>Public instance for access from other classes and gameobjects</summary>
    public static GameManager Instance{
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
        MapCam = GameObject.Find("MapCam");

        HideUI();

        // Don't destory this object on load
        DontDestroyOnLoad(gameObject);
    }

    public void OpenBuyBack(){
        FindClosestShop().OpenBuyBack();
    }

    public void OpenShop(){
        FindClosestShop().OpenShop();
    }

    public Shop FindClosestShop(){
        Shop[] shops = GameObject.FindObjectsOfType<Shop>();
        Shop curr = null;
        foreach(Shop s in shops){
            if(Vector3.Distance(s.transform.position, GameObject.FindWithTag("Player").transform.position)<5){
                curr = s;
            }
        }
        if(curr != null){
            return curr;
        }
        return null;
    }

    /// <summary>Hides all UI elements (for starting the game)</summary>
    void HideUI(){
        QuestPrefab.SetActive(false);
        QuestComplete.SetActive(false);
        WorkshopUI.SetActive(false);
        MapCam.SetActive(false);
        ToolTipUI.SetActive(false);
        ShopUI.SetActive(false);
    }

    void Update()
    {
        DisplayUIComponents();
        DisplayStats();
    }

    /// <summary>Display permanent overlay (UI that is never hidden, like the gold display)</summary>
    void DisplayUIComponents(){
        goldDisplay.GetComponent<TextMeshProUGUI>().text = (player.GetComponent<PlayerController>().gold.ToString());
    }

    /// <summary>Display stats in character UI </summary>
    public void DisplayStats(){
        statsText[0].GetComponent<TextMeshProUGUI>().text = "Attack: " + player.GetComponent<PlayerController>().atk.ToString();
        statsText[1].GetComponent<TextMeshProUGUI>().text = "Atk Range: " + player.GetComponent<PlayerController>().attackRange.ToString();
        statsText[2].GetComponent<TextMeshProUGUI>().text = "Atk Speed: " + player.GetComponent<PlayerController>().attackSpeed.ToString();
        statsText[3].GetComponent<TextMeshProUGUI>().text = "Max Health: " + player.GetComponent<PlayerController>().maxHealth.ToString();
        statsText[4].GetComponent<TextMeshProUGUI>().text = "Max Mana: " + player.GetComponent<PlayerController>().maxmana.ToString();
        statsText[5].GetComponent<TextMeshProUGUI>().text = "Level: " + player.GetComponent<PlayerController>().level.ToString();
        statsText[6].GetComponent<TextMeshProUGUI>().text = "Speed: " + player.GetComponent<PlayerController>().speed.ToString();
    }

    /// <summary>Function to load a new scene, when entering a different room in the world. newScene is the name of the scene to be loaded</summary>
    /// <param name="newScene">Scene to be loaded</param>
    public void EnterRoom(string newScene){
        SceneManager.LoadScene(newScene);
        //SceneManager.MoveGameObjectToScene(GameObject.Find("InventoryManager"), newScene);
        //SceneManager.MoveGameObjectToScene(gameObject, newScene);
        //SceneManager.MoveGameObjectToScene(GameObject.Find("QuestManager"), newScene);
        //SceneManager.MoveGameObjectToScene(player, newScene);
        //SceneManager.MoveGameObjectToScene(GameObject.Find("UI"), newScene);
        // SceneManager.SetActiveScene(newScene);
    }
}
