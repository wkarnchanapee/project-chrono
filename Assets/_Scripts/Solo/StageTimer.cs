using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StageTimer : MonoBehaviour
{
    [SerializeField] float timer = 0f;
    [SerializeField] float finishTime;

    string state = "idle";

    public FloatEvent TimerUpdate;
    public FloatEvent Finish;

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
                TimerUpdate.Invoke(timer);
                break;
            case "finish":
                // show finish time
                finishTime = timer;
                //timerTxtObj.UpdateText(finishTime.ToString("F3"));
                Finish.Invoke(finishTime);
                state = "end";
                break;
            case "end":
                break;
        }
	}

    public void StartTimer()
    {
        state = "start";
    }
    public void StopTimer()
    {
        state = "finish";
    }
    public void ResetTimer()
    {
        state = "idle";
        timer = 0f;
        finishTime = 0f;
        TimerUpdate.Invoke(0f);
        
    }
}
