using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StageTimerControl : MonoBehaviour
{
    public float timer = 0;
    public float finishTime;
    public int stage = 0;

    public float stageTime1, stageTime2, stageTime3;
    string state = "idle";
    TimerText timerTxtObj;

	private void Start()
	{
        timerTxtObj = GameObject.FindWithTag("TimerText").GetComponent<TimerText>();
	}

	// Update is called once per frame
	void Update()
	{
			switch (state)
        {   
            case "idle":
                break;
            case "start":
                timer = 0f;
                state = "live";
                break;
            case "live":
                timer += Time.deltaTime;
                timerTxtObj.UpdateText(timer.ToString("F3"));

                break;
            case "finish":
                // show finish time
                finishTime = timer;
                timerTxtObj.UpdateText(finishTime.ToString("F3"));
                timerTxtObj.UpdateFinishTime(stage, finishTime);
                state = "end";
                break;
            case "end":
                break;
        }
	}

    public void StartTimer() {
        state = "start";
        print("timer start");
    }

    public void StopTimer() {
        state = "finish";
    }

}
