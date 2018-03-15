using UnityEngine;
using System.Collections;

public class TimefieldController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SoloPlayerController.main.state != "rewinding")
            {
                SoloPlayerController.main.ResetEchoes();
                
            }
        }
    }
}
