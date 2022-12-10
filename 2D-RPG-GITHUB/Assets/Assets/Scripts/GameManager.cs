using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goldDisplay;
    public GameObject[] statsText;
    public GameObject QuestPrefab;
    public GameObject QuestComplete;
    public GameObject WorkshopUI;
    public GameObject MapCam;
    // Start is called before the first frame update
    void Start()
    {
        QuestPrefab = GameObject.Find("QuestGiven");
        QuestComplete = GameObject.Find("QuestComplete");
        WorkshopUI = GameObject.Find("WorkshopUI");
        goldDisplay = GameObject.Find("GoldDisplayText");
        QuestPrefab.SetActive(false);
        QuestComplete.SetActive(false);
        WorkshopUI.SetActive(false);
        MapCam.SetActive(false);
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
}
