using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public GameObject Particles;
	public GameObject Springs;
	List<GameObject> particles = new List<GameObject> ();
	List<GameObject> springDampers = new List<GameObject> ();

	public int rows;
	public int cols;
	public int width, height;

	void Start()
	{
		// Generate a Grid
		GameObject particle;

		// generate Particle
		for (int i = 0; i < rows; ++i) 
		{
			for (int j = 0; j < cols; ++j) 
			{
				particle = Instantiate (Particles) as GameObject;
				particle.transform.position = new Vector3 (i * width / rows, j * height / cols, 0);
				particles.Add (particle);
			}
		}

		for(int i = 0; i < rows * cols; ++i)
		{
			Vector3 rowsmedian = (particles[i].transform.position + particles[i + rows].transform.position) / 2;
			GameObject Rowspring = Instantiate(Springs) as GameObject;
			Rowspring.transform.position = rowsmedian;
			Rowspring.GetComponent<SpringDamper>().p1 = particles[i];
			Rowspring.GetComponent<SpringDamper>().p2 = particles[i + rows];
			Rowspring.transform.parent = particles[i].transform;

			if((i % cols) != rows - 1)
			{
				Vector3 colsmedian = (particles[i].transform.position + particles[i + 1].transform.position) / 2;
				GameObject Colsspring = Instantiate(Springs) as GameObject;
				Colsspring.transform.position = colsmedian;
				Colsspring.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
				Colsspring.GetComponent<SpringDamper>().p1 = particles[i];
				Colsspring.GetComponent<SpringDamper>().p2 = particles[i + 1];
				Colsspring.transform.parent = particles[i].transform;
			}
		}
	
	}

	void FixedUpdate()
	{
		// Compute Forces
		// For each particle apply gravity
		foreach (GameObject o in particles) 
		{
			Vector3 gravityForce = new Vector3(0f , -9.8f , 0f) * o.GetComponent<Particle>().mass; 
			o.GetComponent<Particle>().Force += gravityForce;
		}

		// For each Spring-Damper compute and apply Forces
		//foreach (GameObject s in springDampers) 
		//{
			//s.GetComponent<SpringDamper>().computeForce();
		//}

		// Intergrate Motion
		// For each particle apply Euler Intergration
		foreach (GameObject o in particles) 
		{
			//o.GetComponent<Particle>().EulerIntergration();
		}
	}

}
