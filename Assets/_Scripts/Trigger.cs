using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Trigger : MonoBehaviour {

    [SerializeField] string[] tags = new string[1];
    public GameObjectEvent Enter, Exit, Stay;

    void OnTriggerEnter(Collider other) {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.tag == tags[i])
            {
                Enter.Invoke(other.gameObject); 
                
            }
        }    
    }
    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.tag == tags[i])
            {
                Exit.Invoke(other.gameObject);
                
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (other.tag == tags[i])
            {
                Stay.Invoke(other.gameObject);
                
            }
        }
    }
}
