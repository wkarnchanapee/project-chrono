using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {
    [SerializeField] GameState gameState;

    Vector3 startPos;
    Quaternion startRot;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
        // register with reset control
        gameState.resetEvent.AddListener(TriggerReset);
	}

    public void TriggerReset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        print(gameObject.name + "Reset.");
    }
}
