using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestGiver : MonoBehaviour
{
    public GameObject[] quests;
    public GameObject[] PreReqs;
    public bool preReqs = false;
    public int questsCompleted = 0;
    public int questsGiven = 0;
    public GameObject QM;
    public GameObject icon;
    public Sprite completedSprite;
    public Sprite newQuestSprite;
    public Sprite notCompletedSprite;
    // Start is called before the first frame update
    void Start()
    {
        QM = GameObject.Find("QuestManager");
        preReqs = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(preReqs == false){
            icon.GetComponent<SpriteRenderer>().sprite = null;
            int completedPreReqs = 0;
            foreach(GameObject q in PreReqs){
                if(q.GetComponent<Quest>().complete){
                    completedPreReqs++;
                }
            }
            if(completedPreReqs == PreReqs.Length){
                preReqs = true;
            }
        }
        else{
            if(questsGiven>questsCompleted && quests[questsGiven-1].GetComponent<Quest>().complete){
                icon.GetComponent<SpriteRenderer>().sprite = completedSprite;
            }else if(questsGiven>questsCompleted){
                icon.GetComponent<SpriteRenderer>().sprite = notCompletedSprite;
            }
            else if(questsGiven < quests.Length){
                icon.GetComponent<SpriteRenderer>().sprite = newQuestSprite;
            }else{
                icon.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    public void InterAct(){
        if(preReqs){
            if(questsGiven>questsCompleted){
                if(QM.GetComponent<QuestManager>().CompleteQuest(quests[questsGiven-1])){
                    questsCompleted++;
                }
            }
            else if(questsGiven==quests.Length){
                return;
            }
            else{
                if(quests[questsGiven].GetComponent<Quest>().complete){
                    QM.GetComponent<QuestManager>().AutoComplete(quests[questsGiven]);
                    questsCompleted++;
                }
                else{
                    QM.GetComponent<QuestManager>().GiveQuest(quests[questsGiven]);
                }
                questsGiven++;
            }
        }
    }
}
