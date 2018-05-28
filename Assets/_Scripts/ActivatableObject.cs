using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour
{
    public bool on;
    public float timeout = 0f;
    public float timeoutTime = 1f;
    public bool startCondition;
    public float power = 0f;
    Vector3 startPosition;
    Quaternion startRotation;

    private void Awake()
    {
        startCondition = on;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
}
