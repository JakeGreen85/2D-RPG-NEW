using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IQuest : MonoBehaviour
{
    public bool complete;
    public GameObject[] rewards;
    public int goldReward;
    public int expReward;
    public string questInfo;
    public string goalInfo;

    private void Start() {}

    public virtual void CheckKill(string kType){}

    public virtual void CheckGather(GameObject gather){}

    public virtual void CheckObjective(GameObject obj){}

    public virtual void CheckComplete(){}
}
