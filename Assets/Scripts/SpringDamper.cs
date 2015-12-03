using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour {

    public float SpringContants;
    public float DampingFactor;
    Vector3 RestLength;

    public Particle p1;
    public Particle p2;

    public void ComputeForce()
    {
		RestLength = p1.Pos - p2.Pos;
		
		Vector3 spring = -SpringContants * RestLength;
		Vector3 damp = -DampingFactor * (p1.Vel - p2.Vel);
    }
	
}
