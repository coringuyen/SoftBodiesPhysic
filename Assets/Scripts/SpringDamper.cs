using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour {

	public float SpringConstant; // stiffness
	public float DampingFactor; // velocity restriction
	public float Restlength; // maintain length

	public Particle p1;
	public Particle p2;

    void Update()
    {
        // Update the string as the position change
        LineRenderer spring = GetComponent<LineRenderer>();
        spring.SetPosition(0, p1.Position);
        spring.SetPosition(1, p2.Position);
    }

    public void SetSpring(Particle _p1, Particle _p2)
    {
        p1 = _p1;
        p2 = _p2;
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
