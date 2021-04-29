using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueControls : MonoBehaviour
{
    public GameWatcher gw;

    public Transform cueBall;

    public Transform pivotPoint;

    public Transform cueOutOfWayPosition;

    public bool IsInUse = true;
    public bool FollowCueBall = true;
    public bool IsRotating = true;
    public bool IsShooting = false;

    Vector3 shotDirection = Vector3.zero;
    float startPosition = 0f;

    Vector3 cueVector;
    public Vector3 initialPos;

    float distance;
    float power;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
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

        if (IsRotating)
        {
            cueVector = GetCueVector();

            distance = cueVector.magnitude;
            cueVector.z = cueVector.y;
            cueVector.y = 0;

            gameObject.transform.forward = -cueVector;
        }

        if (IsShooting)
        {
            // Move the cue back and forth based on current power / mouse location
            power = GetPower(startPosition, GetCueVector().magnitude);
           
            Vector3 pullBack = Vector3.zero;
            pullBack.z = power / 200f;
            pivotPoint.localPosition = pullBack;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Button Down");
            IsRotating = false;
            FollowCueBall = false;
            IsShooting = true;
            shotDirection = GetCueVectorInverse();
            startPosition = shotDirection.magnitude;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Mouse Button Up");
            IsRotating = false;
            FollowCueBall = false;
            IsShooting = false;
            //Vector3 endPoint = GetCueVector();
            Vector3 direction = shotDirection.normalized;
            cueBall.gameObject.GetComponent<Rigidbody>().AddForce((-pivotPoint.forward) * power);

            // Move cue out of the way and start a coroutine for checking when balls stop moving
            MoveCueOutOfWay();
            StartCoroutine(gw.CheckMotion());
        }
        //Vector3 pullBack = Vector3.zero;
        //pullBack.z = distance / 200f;
        //pivotPoint.localPosition = pullBack;

    }

    public float GetPower(float start, float end)
    {
        Debug.Log("Start: " + start.ToString() + ", End: " + end.ToString());
        return Mathf.Abs(start - end);
    }

    public Vector3 GetCueVector()
    {
        Vector3 cue = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        cue = Input.mousePosition - cue;

        return cue;
    }

    public Vector3 GetCueVectorInverse()
    {
        Vector3 cue = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        cue = cue - Input.mousePosition;

        return cue;
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
