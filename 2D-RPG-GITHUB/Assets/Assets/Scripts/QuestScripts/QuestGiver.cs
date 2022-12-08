using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameObject[] quests;
    public int questsCompleted = 0;
    public int questsGiven = 0;
    public GameObject QM;
    // Start is called before the first frame update
    void Start()
    {
        QM = GameObject.Find("QuestManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InterAct(){
        if(questsGiven>questsCompleted){
            if(QM.GetComponent<QuestManager>().CompleteQuest(quests[questsGiven-1])){
                questsCompleted++;
            }
        }else if(questsGiven==quests.Length){
            return;
        }else{
            QM.GetComponent<QuestManager>().GiveQuest(quests[questsGiven]);
            questsGiven++;
        }
    }
}
