using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LookAtTarget))]
public class EnemyState : MonoBehaviour {

    public enum State
    {
        Off = 0,
        Alert = 1,
        Aware = 2,
        Searching = 3,
        Dead = 4
    }
    [SerializeField] State state;

    public StringEvent StateUpdate;

    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 3f;
    float fireTimer = 0f;
    [SerializeField] GameObject target;
    [SerializeField] float sightDistance = 5f;
    [SerializeField] Transform gunMountPoint;
	void Start () {

        state = State.Alert;

	}
	void Update () {

        switch (state)
        {
            case State.Off:
                break;
            case State.Alert:
                break;
            case State.Aware:
                Ray ray = new Ray(transform.position, Vector3.Normalize(target.transform.position - transform.position));
                Debug.DrawRay(ray.origin, ray.direction * sightDistance,Color.cyan);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, sightDistance))
                {
                    if (hit.transform == target.transform && fireTimer <= Time.time )
                    {
                        FireWeapon();
                    }
                }
                break;
            case State.Searching:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

    public void TriggerHit(GameObject obj)
    {
        if (state == State.Alert)
        {
            state = State.Aware;
            target = obj;
        }
    }

    void FireWeapon()
    {
        print("kapow");

        Destroy(Instantiate(bullet, gunMountPoint.position, gunMountPoint.rotation),5f);
        fireTimer = Time.time + fireRate;
    }
}
