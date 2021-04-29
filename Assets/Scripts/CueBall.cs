using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    public bool BallInHand = false;
    public Transform spawnPoint;
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
        if (BallInHand == true)
        {
            transform.position = new Vector3(Input.mousePosition.x, spawnPoint.position.y, Input.mousePosition.z);

            if (Input.GetMouseButton(0))
            {
                BallInHand = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pocket" || other.tag == "Bounds")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = spawnPoint.position;
        }
    }
}
