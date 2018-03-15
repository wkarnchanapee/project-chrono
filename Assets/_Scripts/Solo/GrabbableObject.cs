using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GrabbableObject : MonoBehaviour
{

    Rigidbody rb;
    public bool isGrabbed = false;
    Vector3 startPos;
    Quaternion startRot;
    public Collider col;
    

    private void Start()
    {
        

        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //register with reset event in the player controller
        SoloPlayerController.main.RegisterResetListener(gameObject);
    }
    private void Update()
    {
 
        if (isGrabbed)
        {
            transform.position = SoloPlayerController.main.transform.position + (Vector3.up * 0.5f) + (Camera.main.transform.forward * SoloPlayerController.main.pickupDist);
            //RaycastHit hit;
           // Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f);
            //print(hit.point);
            //transform.position = hit.point;

            rb.velocity = Vector3.zero;
            col.isTrigger = true;
        }
    }
    public void Reset()
    {
        isGrabbed = false;
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
