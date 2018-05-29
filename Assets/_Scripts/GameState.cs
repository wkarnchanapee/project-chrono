using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameState : ScriptableObject {

    
    public string state = "";
    public UnityEvent resetEvent;

    public void Awake()
    {
        state = "idle";
    }
    public void OnDestroy()
    {
    }
    public void ResetState()
    {
        resetEvent.Invoke();
        Debug.Log("Reset Triggered");
    }
}
