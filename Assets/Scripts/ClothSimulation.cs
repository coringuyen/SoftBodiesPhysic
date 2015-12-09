using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public GameObject ParticlesPreb; 
    public GameObject RowSpringsPreb;
    public GameObject ColSpringsPreb;
    public GameObject LeftDSpringsPreb;
    public GameObject RightDSpringsPreb;
    List<GameObject> particles = new List<GameObject> ();
	List<GameObject> springDampers = new List<GameObject> ();

	public int rows; // how many particle for row
	public int cols; // how many particle for column
	public int width, height; // width and height of the Grid entirely 

    // Generate a Grid
    void Start()
	{
        // Spawn Particle
        // particle position go by row then for each column with the gap from width and height
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

        // Spawn Spring
		for(int i = 0; i < rows * cols; ++i)
		{
            // Spring for the row
            if (i + rows < rows * cols)
            {
                GameObject RowSpring = Instantiate(RowSpringsPreb) as GameObject;
                RowSpring.transform.position = median_of_two_particles(particles[i], particles[i + rows]);
                RowSpring.transform.localScale = SpringScale(particles[i], particles[i + rows]);
                RowSpring.GetComponent<SpringDamper>().SetSpring(particles[i], particles[i + rows]);
            }
            
            // Spring for the colum
			if((i % cols) != rows - 1)
			{
				GameObject ColSpring = Instantiate(ColSpringsPreb) as GameObject;
				ColSpring.transform.position = median_of_two_particles(particles[i], particles[i + 1]);
                ColSpring.transform.localScale = SpringScale(particles[i], particles[i + 1]);
                ColSpring.GetComponent<SpringDamper>().SetSpring(particles[i], particles[i + 1]);

                // Right diagonal spring
                if (i + 1 < rows * cols && i + rows < rows * cols)
                {
                    GameObject RightDSpring = Instantiate(RightDSpringsPreb) as GameObject;
                    RightDSpring.transform.position = median_of_two_particles(particles[i + 1], particles[i + rows]);
                    RightDSpring.transform.localScale = SpringScale(particles[i + 1], particles[i + rows]);
                    RightDSpring.GetComponent<SpringDamper>().SetSpring(particles[i + 1], particles[i + rows]);
                }

                // Left diagonal spring
                if(i + rows + 1 < rows * cols)
                {
                    GameObject LeftDSpring = Instantiate(LeftDSpringsPreb) as GameObject;
                    LeftDSpring.transform.position = median_of_two_particles(particles[i], particles[i + rows + 1]);
                    LeftDSpring.transform.localScale = SpringScale(particles[i], particles[i + rows + 1]);
                    LeftDSpring.GetComponent<SpringDamper>().SetSpring(particles[i], particles[i + rows + 1]);
                }

            }

		}
	
	}

    /// <summary>
    /// Return a Vector3 median position  
    /// </summary>
    /// <param name="p1"> First Particle </param>
    /// <param name="p2"> Second Particle </param>
    /// <returns></returns>
    Vector3 median_of_two_particles(GameObject p1, GameObject p2)
    {
        Vector3 median = (p1.transform.position + p2.transform.position) / 2;
        return median;
    }

    /// <summary>
    /// Return a Vector3 for Spring Scale
    /// </summary>
    /// <param name="p1"> First Particle </param>
    /// <param name="p2"> Second Particle </param>
    /// <returns></returns>
    Vector3 SpringScale(GameObject p1, GameObject p2)
    {
        float Yscale = Vector3.Distance(p1.transform.position, p2.transform.position) / 2;
        return new Vector3(0.05f, Yscale, 0.05f);
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
