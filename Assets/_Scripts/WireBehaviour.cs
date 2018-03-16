using UnityEngine;
using System.Collections;

public class WireBehaviour : ActivatableObject
{
    
    Material m_Material;
    [SerializeField] ActivatableObject entry,exit;
    [SerializeField] bool defaultBehaviour = true;

    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        entry = transform.GetChild(0).GetComponent<ActivatableObject>();
        exit = transform.GetChild(1).GetComponent<ActivatableObject>();
    }

    private void Update()
    {   
        if (entry.on)
        {
            on = true;
            exit.on = true;
        } else
        {
            on = false;
            exit.on = false;
        }
        // Set the colour if the obj is on.
        if (defaultBehaviour) {
            if (on)
            {
                m_Material.color = Color.red;
            }
            else
            {
                m_Material.color = Color.white;
            }
        } else {
            if (on)
            {
                m_Material.color = Color.white;
            }
            else
            {
                m_Material.color = Color.red;
            }
        }

        

    }
    
   
}
