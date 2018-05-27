using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPropulsion : MonoBehaviour {

    [SerializeField] float propulsionSpeed;
    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

	void Update () {
        rb.velocity = transform.forward * propulsionSpeed;
	}
}
