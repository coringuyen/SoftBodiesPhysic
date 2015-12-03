using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour {

	public float SpringConstant;
	public float DampingFactor;
	Vector3 Restlength;

	public void computeForce(Particle p1, Particle p2)
	{
		Restlength = p1.Position - p2.Position;

		Vector3 spring = - SpringConstant * Restlength;
		Vector3 damp = - DampingFactor * (p1.Velocity - p2.Velocity);
	}
}
