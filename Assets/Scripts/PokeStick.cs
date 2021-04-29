using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeStick : MonoBehaviour
{
    public Transform cueBall;

    public Transform pivotPoint;

    public Transform cueOutOfWayPosition;

    public bool IsInUse = true;
    public bool FollowCueBall = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInUse)
        {
            return;
        }

        if (FollowCueBall)
        {
            MoveToCueBall();
        }

        Vector3 cueVector = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        cueVector = Input.mousePosition - cueVector;

        float distance = cueVector.magnitude;
        cueVector.z = cueVector.y;
        cueVector.y = 0;

        gameObject.transform.forward = cueVector;
        Vector3 pullBack = Vector3.zero;
        pullBack.z = distance / 200f;
        pivotPoint.localPosition = pullBack;

    }

    public void MoveToCueBall()
    {
        gameObject.transform.position = cueBall.position;
    }

    public void MoveCueOutOfWay()
    {
        IsInUse = false;

        gameObject.transform.position = cueOutOfWayPosition.position;
        gameObject.transform.rotation = Quaternion.identity;
    }
}
