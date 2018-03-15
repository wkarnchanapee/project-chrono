using UnityEngine;
using System.Collections;

public class TimePickup : MonoBehaviour
{

    public float setRewindLimit = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoloPlayerController.main.rewindLimit = setRewindLimit;
        }
    }
}
