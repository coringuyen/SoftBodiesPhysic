using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public GameObject Particles;
	public GameObject Particles1;
	public GameObject Particles2;
	List<GameObject> particles = new List<GameObject> ();
	List<GameObject> springDampers = new List<GameObject> ();



	void Start()
	{
		float row = 5;
		float col = 5;
		// Making a Grid
		for (int i = 0; i < row; ++i) 
		{
			for (int j = 0; i < col; ++i) 
			{
				GameObject particle = Instantiate (Particles) as GameObject;
				particle.transform.position = new Vector3 (i - 10f, 1.5f, 0f);
				particles.Add (particle);
			}
		}
	}

	void FixedUpdate()
	{
		// Compute Forces
		// For each particle apply gravity
		foreach (GameObject o in particles) 
		{
			o.GetComponent<Particle>().Position += o.GetComponent<Particle>().Force;
		}

		// For each Spring-Damper compute and apply Forces
		foreach (GameObject s in springDampers) 
		{
			s.GetComponent<SpringDamper>().computeForce();
		}

		// Intergrate Motion
		// For each particle apply Euler Intergration
		foreach (GameObject o in particles) 
		{
			o.GetComponent<Particle>().EulerIntergration();
		}
	}

}
