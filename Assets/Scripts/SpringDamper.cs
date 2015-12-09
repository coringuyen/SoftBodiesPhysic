using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour {

	public float SpringConstant;
	public float DampingFactor;
	public float Restlength;

	private GameObject p1;
	private GameObject p2;

	void Start()
	{

	}


    public void SetSpring(GameObject _p1, GameObject _p2)
    {
        p1 = _p1;
        p2 = _p2;
        transform.parent = _p2.transform;
    }

	public void computeForce()
	{
		float particlesDistance = Vector3.Distance(p1.transform.position ,p2.transform.position);
		Vector3 particlesDiff = p2.transform.position - p1.transform.position;
		Vector3 normalizeParticlesDiff = particlesDiff.normalized;
		
		float springForce = - SpringConstant * (Restlength - particlesDistance);

		float particle1Vel = Vector3.Dot (normalizeParticlesDiff, p1.GetComponent<Particle>().Velocity); 
		float particle2Vel = Vector3.Dot (normalizeParticlesDiff, p2.GetComponent<Particle>().Velocity);

		float dampForce = - DampingFactor * (particle1Vel - particle2Vel);

		float springDamper = springForce + dampForce;

		Vector3 force1 = springDamper * normalizeParticlesDiff;
		Vector3 force2 = -force1;

		p1.GetComponent<Particle>().Force += force1;
		p2.GetComponent<Particle>().Force += force2;
	}
}
