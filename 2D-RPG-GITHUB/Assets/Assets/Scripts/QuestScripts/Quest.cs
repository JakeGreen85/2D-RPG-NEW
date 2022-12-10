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
    public Quest(int gGoal, string gType, int kGoal, string kType, string info, GameObject[] loot, int gold, int exp, string goalinfo){
        gatherGoal = gGoal;
        killGoal = kGoal;
        questInfo = info;
        rewards = loot;
        goldReward = gold;
        expReward = exp;
        gatherType = gType;
        killType = kType;
        complete = false;
        goalInfo = goalinfo;
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
