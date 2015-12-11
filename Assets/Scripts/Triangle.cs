using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour {

    Particle p1;
    Particle p2;
    Particle p3;

    Vector3 airVelocity;
    public float airDensity;
    float dragCoefficient;

    void Start()
    {
        airVelocity = new Vector3(0, 0, 1);
        airDensity = 2f;
        dragCoefficient = 0.5f;
    }

    public void makeTraingle(Particle _p1, Particle _p2, Particle _p3)
    {
        p1 = _p1;
        p2 = _p2;
        p3 = _p3;
    }

    public void computeForce()
    {
        Vector3 part1 = p2.Position - p1.Position;
        Vector3 part2 = p3.Position - p1.Position;

        Vector3 averageVel = (p1.Velocity + p2.Velocity + p3.Velocity) / 3;
        averageVel -= airVelocity;

        Vector3 crossProductof3points = Vector3.Cross(part1, part2);
        Vector3 Trianglenormal = crossProductof3points / crossProductof3points.magnitude;

        Vector3 v2_an = ((0.5f * Vector3.Dot(averageVel, Trianglenormal) * averageVel.magnitude) / crossProductof3points.magnitude) * crossProductof3points;

        Vector3 aeroForce = -0.5f * airDensity * dragCoefficient * v2_an;

        aeroForce /= 3;

        p1.Force += aeroForce;
        p2.Force += aeroForce;
        p3.Force += aeroForce;
    }
}
