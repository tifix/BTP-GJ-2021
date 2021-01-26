using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IEnemyBehaviour
{
    public string name = "default";
    public UnityEvent behaviour_trigger; //when this event is called, execute

    public void Execute()
    {

    }


}
