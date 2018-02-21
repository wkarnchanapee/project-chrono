using UnityEngine;
using System.Collections;

public class WireBehaviour : ActivatableObject
{

    Material m_Material;
    [SerializeField] GameObject entry,exit;

    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (on)
        {
            m_Material.color = Color.red;

            // Check for timeout
            if (timeout > timeoutTime)
            {
                on = false;
                timeout = 0f;
            } else
            {
                timeout += Time.deltaTime;
            }
        } else
        {
            m_Material.color = Color.white;
        }
    }

    
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "wire-exit" && on == false)
        {
 
            if (other.GetComponent<ActivatableObject>().on == true)
            {
                on = true;
            }
        }
    }
   
}
