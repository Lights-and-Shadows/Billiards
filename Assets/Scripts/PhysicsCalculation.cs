using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCalculation
{

    public float G = 6.67408f * Mathf.Pow(10, -11); // Gravitational constant

    public float ComputeForce(PhysicalObject obj)
    {
        return (obj.mass * 9.81f);
    }

    public float ComputeForce(PhysicalObject obj, float value)
    {
        return (obj.mass * value);
    }

    public float ComputeGravity(PhysicalObject o1, PhysicalObject o2)
    {
        float r2 = Mathf.Sqrt(Vector3.Distance(o1.transform.position, o2.transform.position));

        float m1 = o1.mass;
        float m2 = o2.mass;

        float force = G * ((m1 * m2) / r2);



        return force;
    }


}
