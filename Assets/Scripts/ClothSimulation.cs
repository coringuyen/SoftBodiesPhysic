using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public GameObject ParticlesPreb;
	public GameObject SpringsPreb;
	List<GameObject> particles = new List<GameObject> ();
	List<GameObject> springDampers = new List<GameObject> ();

	public int rows; // how many particle for row
	public int cols; // how many particle for col
	public int width, height; // width and height of the Grid entirely 

    // Generate a Grid
    void Start()
	{
        // generate Particle
        GameObject particle;
		for (int i = 0; i < rows; ++i) 
		{
			for (int j = 0; j < cols; ++j) 
			{
				particle = Instantiate (ParticlesPreb) as GameObject;
				particle.transform.position = new Vector3 (i * width / rows, j * height / cols, 0);
				particles.Add (particle);
			}
		}

        // set the spring
		for(int i = 0; i < rows * cols; ++i)
		{
            if (i + rows < rows * cols)
            {
                GameObject RowSpring = Instantiate(SpringsPreb) as GameObject;
                RowSpring.transform.position = median_of_two_particles(particles[i], particles[i + rows]);

                RowSpring.GetComponent<SpringDamper>().p1 = particles[i];
                RowSpring.GetComponent<SpringDamper>().p2 = particles[i + rows];
                RowSpring.transform.parent = particles[i].transform;
            }

            
			if((i % cols) != rows - 1)
			{
				GameObject ColSpring = Instantiate(SpringsPreb) as GameObject;
				ColSpring.transform.position = median_of_two_particles(particles[i], particles[i + 1]);
				ColSpring.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

				ColSpring.GetComponent<SpringDamper>().p1 = particles[i];
				ColSpring.GetComponent<SpringDamper>().p2 = particles[i + 1];
				ColSpring.transform.parent = particles[i].transform;
			}
		}
	
	}

    // find the mid point from the 2 particles position to set the Spring position
    Vector3 median_of_two_particles(GameObject p1, GameObject p2)
    {
        Vector3 median = (p1.transform.position + p2.transform.position) / 2;
        return median;
    }

	void FixedUpdate()
	{
		// Compute Forces
		// For each particle apply gravity
		foreach (GameObject o in particles) 
		{
			Vector3 gravityForce = new Vector3(0f , -0.2f , 0f) * o.GetComponent<Particle>().mass; 
			o.GetComponent<Particle>().Force += gravityForce;
		}

		// For each Spring-Damper compute and apply Forces
		//foreach (GameObject s in springDampers) 
		//{
		//	s.GetComponent<SpringDamper>().computeForce();
		//}

		// Intergrate Motion
		// For each particle apply Euler Intergration
		//foreach (GameObject o in particles) 
		//{
		//	o.GetComponent<Particle>().EulerIntergration();
		//}
	}

}
