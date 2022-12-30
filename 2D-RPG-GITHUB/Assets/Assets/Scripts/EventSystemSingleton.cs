using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemSingleton : MonoBehaviour
{
    private static EventSystemSingleton _instance;
    public static EventSystemSingleton Instance{
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
}
