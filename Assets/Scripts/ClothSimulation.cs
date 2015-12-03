using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public GameObject Particles;
	List<GameObject> particles = new List<GameObject> ();
	List<GameObject> springDampers = new List<GameObject> ();

	void Start()
	{
		for (int i = 0; i < 20; ++i) 
		{
			GameObject particle = Instantiate(Particles) as GameObject;
			particle.transform.position = new Vector3();
			particles.Add(particle);
		}
	}

	void FixedUpdate()
	{
		foreach (GameObject o in particles) 
		{
			// Apply gravity force to all the particle
			o.GetComponent<Particle>().Position += o.GetComponent<Particle>().Force;
		}


		foreach (GameObject s in springDampers) 
		{
			//s.GetComponent<SpringDamper>().computeForce();
		}
	}

}
