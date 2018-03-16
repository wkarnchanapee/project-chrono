using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageGate : MonoBehaviour {

    public UnityEvent startGateTriggered,finishGateTriggered;
    public bool isStart = true;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (isStart) {
                startGateTriggered.Invoke();
            } else {
                finishGateTriggered.Invoke();
            }

        }
    }
    
}
