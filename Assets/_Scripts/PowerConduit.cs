using UnityEngine;
using System.Collections;

public class PowerConduit : ActivatableObject
{
    public ActivatableObject wire;
    public ActivatableObject connectedObj = null;
    public bool isExit = false;

    private void Start()
    {
        wire = transform.parent.GetComponent<ActivatableObject>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isExit)
        {
            
        } else
        {
            switch (other.tag)
            {
                case "pressure-plate":
                    connectedObj = other.GetComponent<ActivatableObject>();
                    break;
                case "wire-exit":
                    connectedObj = other.GetComponent<ActivatableObject>();
                    break;
            }
        }        
    }
    private void OnTriggerStay(Collider other)
    {   if (connectedObj != null)
        {
            switch (connectedObj.tag)
            {
                case "pressure-plate":
                    on = connectedObj.on;
                    break;
                case "wire-exit":
                    on = connectedObj.on;
                    break;
            }
        }
        
	}
	private void OnTriggerExit(Collider other)
	{
        if (connectedObj != null)
        {
            if (other.gameObject == connectedObj.gameObject)
            {
                on = false;
                connectedObj = null;
            }
        }
        

	}
}

