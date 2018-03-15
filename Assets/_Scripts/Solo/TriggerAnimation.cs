using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        target.GetComponent<Animator>().SetBool("escapeanim", true);
    }
}
