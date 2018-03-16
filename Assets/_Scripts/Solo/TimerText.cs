using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimerText : MonoBehaviour {

    Text text;
    public Text finishTime1, finishTime2, finishTime3;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

	}
    public void UpdateText(string txt) {
        text.text = txt;

    }
    public void UpdateFinishTime(int stage, float time) {
        switch (stage)
        {
            case 1:
                finishTime1.text = time.ToString();
                break;
            case 2:
                finishTime2.text = time.ToString();
                break;
            case 3:
                finishTime3.text = time.ToString();
                break;
        }
    }
}
