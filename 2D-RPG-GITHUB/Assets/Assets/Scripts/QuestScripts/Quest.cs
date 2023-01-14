using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int gatherGoal;
    public int gathers;
    public string gatherType;
    public int killGoal;
    public int kills;
    public string killType;
    public int objGoal;
    public int objectives;
    public string objType;
    public bool complete;
    public GameObject[] rewards;
    public int goldReward;
    public int expReward;
    public string questInfo;
    public string goalInfo;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void CheckKill(string kType){
        if(kType == killType){
            kills++;
            CheckComplete();
        }
    }

    public void CheckGather(GameObject gather){
        if(gather.CompareTag(gatherType)){
            gathers++;
            CheckComplete();   
        }
    }

    public void CheckObjective(GameObject obj){
        if(obj.CompareTag(objType)){
            objectives++;
            CheckComplete();
        }
    }

    public void CheckComplete(){
        if(gathers == gatherGoal && kills == killGoal && objectives == objGoal){
            complete = true;
        }
    }
}
