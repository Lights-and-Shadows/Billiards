using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBall : MonoBehaviour
{
    public Transform spawnPoint, outOfBoundsPoint;
    public GameWatcher gw;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.sleepThreshold = 0.01f;
        rb.angularDrag = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bounds")
        {
            transform.position = outOfBoundsPoint.position;
        }

        if (other.tag == "Pocket")
        {
            Debug.Log("Ball Pocketed: " + gameObject.name);
            transform.position = gw.spawns[gw.index].position;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gw.index++;
        }
    }
}
