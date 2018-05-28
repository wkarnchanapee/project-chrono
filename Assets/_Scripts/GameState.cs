using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject {

    public string state = "";
    public void Awake()
    {
        state = "idle";
    }
    public void OnDestroy()
    {
    }
    public void ResetState()
    {
        Debug.Log("Reset every state");
    }
}
