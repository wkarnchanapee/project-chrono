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
    {   //checks if there is power in the object and sets on and off accordingly.
        powerCheck();

        // Set the colour if the obj is on.
        if (on)
        {
            m_Material.color = Color.red;
        } else
        {
            m_Material.color = Color.white;
        }

        // drain the power

    }
    void powerCheck() 
    {
        if (power > 0f) 
        {
            on = true;
            
        } else 
        {
            on = false;
        }    
    }
   
}
