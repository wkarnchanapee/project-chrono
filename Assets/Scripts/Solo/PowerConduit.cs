using UnityEngine;
using System.Collections;

public class PowerConduit : ActivatableObject
{
    public ActivatableObject wire;
    public ActivatableObject connectedObj;

    public bool isExit = false;

    private void Start()
    {
        wire = transform.parent.GetComponent<ActivatableObject>();
        
    }
    private void Update()
    {

    }

	private void OnCollisionEnter(Collision collision)
    {
        print(name + " collided with " + collision.gameObject.name);

        if (collision.gameObject.tag == "conduit-entry" && isExit == true)
        {
            connectedObj = collision.transform.parent.GetComponent<WireBehaviour>();
            
        }
	}
	private void OnCollisionStay(Collision collision)
	{
        if (collision.gameObject.tag == "conduit-entry" && isExit == true)
        {
            print("stuffs hapenin man");

            if (wire.power >= 0.5f) 
            {
                connectedObj.power += 0.5f;
                wire.power -= 0.5f;    
            } else if (wire.power > 0f && wire.power < 0.5f )
            {
                connectedObj.power += wire.power;
                wire.power = 0f;
            }

        }
	}
	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.tag == "conduit-entry" && isExit == true) 
        {
            connectedObj = null;
        }

	}
}

