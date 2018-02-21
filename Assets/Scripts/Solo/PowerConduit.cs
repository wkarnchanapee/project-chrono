using UnityEngine;
using System.Collections;

public class PowerConduit : ActivatableObject
{
    ActivatableObject parentOn;

    private void Start()
    {
        parentOn = transform.parent.GetComponent<ActivatableObject>();
    }
    private void Update()
    {
        if (gameObject.tag == "wire-exit")
        {
            on = parentOn
            if ()
            // Check for timeout
            if (timeout > timeoutTime)
            {
                on = false;
                timeout = 0f;
            }
            else
            {
                timeout += Time.deltaTime;
            }
        }
        else
        {
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wire-exit" && on == false)
        {
             = other.GetComponent<ActivatableObject>().on;
        }
    }
}
