using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour {

	public float SpringConstant;
	public float DampingFactor;
	public float Restlength;

	Particle p1;
	Particle p2;

	void Start()
	{
		p1 = GetComponent<Particle> ();
		p2 = GetComponent<Particle> ();
	}

	public void computeForce()
	{
		float particlesDistance = Vector3.Distance(p1.Position ,p2.Position);
		Vector3 particlesDiff = p2.Position - p1.Position;
		Vector3 normalizeParticlesDiff = particlesDiff.normalized;
		
		float springForce = - SpringConstant * (Restlength - particlesDistance);

		float particle1Vel = Vector3.Dot (normalizeParticlesDiff, p1.Velocity); 
		float particle2Vel = Vector3.Dot (normalizeParticlesDiff, p2.Velocity);

		float dampForce = - DampingFactor * (particle1Vel - particle2Vel);

		float springDamper = springForce + dampForce;

		Vector3 force1 = springDamper * normalizeParticlesDiff;
		Vector3 force2 = -force1;

		p1.Force += force1;
		p2.Force += force2;
	}
}
