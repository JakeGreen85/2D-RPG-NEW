using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goldDisplay;
    public GameObject[] statsText;
    public GameObject QuestPrefab;
    public GameObject QuestComplete;
    public GameObject WorkshopUI;
    public GameObject MapCam;
    public GameObject ToolTipUI;
    public GameObject ShopUI;
    
    private static GameManager _instance;
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
        QuestPrefab = GameObject.Find("QuestGiven");
        QuestComplete = GameObject.Find("QuestComplete");
        WorkshopUI = GameObject.Find("WorkshopUI");
        goldDisplay = GameObject.Find("GoldDisplayText");
        player = GameObject.Find("Player");
        goldDisplay = GameObject.Find("GoldDisplayText");
        statsText[0] = GameObject.Find("Attack");
        statsText[1] = GameObject.Find("Range");
        statsText[2] = GameObject.Find("Atk Speed");
        statsText[3] = GameObject.Find("Max Health");
        statsText[4] = GameObject.Find("Max Mana");
        statsText[5] = GameObject.Find("Level");
        statsText[6] = GameObject.Find("Speed");
        MapCam = GameObject.Find("MapCam");
        DontDestroyOnLoad(gameObject);
        QuestPrefab.SetActive(false);
        QuestComplete.SetActive(false);
        WorkshopUI.SetActive(false);
        MapCam.SetActive(false);
        ToolTipUI.SetActive(false);
        ShopUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayUIComponents();
        DisplayStats();
    }

    void DisplayUIComponents(){
        goldDisplay.GetComponent<TextMeshProUGUI>().text = (player.GetComponent<PlayerController>().gold.ToString());
    }

    public void DisplayStats(){
        statsText[0].GetComponent<TextMeshProUGUI>().text = "Attack: " + player.GetComponent<PlayerController>().atk.ToString();
        statsText[1].GetComponent<TextMeshProUGUI>().text = "Atk Range: " + player.GetComponent<PlayerController>().attackRange.ToString();
        statsText[2].GetComponent<TextMeshProUGUI>().text = "Atk Speed: " + player.GetComponent<PlayerController>().attackSpeed.ToString();
        statsText[3].GetComponent<TextMeshProUGUI>().text = "Max Health: " + player.GetComponent<PlayerController>().maxHealth.ToString();
        statsText[4].GetComponent<TextMeshProUGUI>().text = "Max Mana: " + player.GetComponent<PlayerController>().maxmana.ToString();
        statsText[5].GetComponent<TextMeshProUGUI>().text = "Level: " + player.GetComponent<PlayerController>().level.ToString();
        statsText[6].GetComponent<TextMeshProUGUI>().text = "Speed: " + player.GetComponent<PlayerController>().speed.ToString();
    }

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
