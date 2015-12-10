using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public Particle ParticlesPreb; 
    public SpringDamper SpringsPreb;
   
    List<Particle> particles = new List<Particle>();
	List<SpringDamper> springDampers = new List<SpringDamper>();

	public int rows; // how many particle for row
	public int cols; // how many particle for column
	public int width, height; // width and height of the Grid entirely 

  
    void Start()
	{
		ClothSpawn();
        SetAnchor();
	}

	// Generate a Grid
	void ClothSpawn()
	{
		// Spawn Particle
		// particle position go by row then for each column with the gap from width and height
		Particle particle;
		for (int i = 0; i < rows; ++i) 
		{
			for (int j = 0; j < cols; ++j) 
			{
				particle = Instantiate (ParticlesPreb);
				particle.transform.position = new Vector3 (i * width / rows, j * height / cols, 0);
                particle.transform.parent = transform;
                particles.Add (particle);
			}
		}
		
		// Spawn Spring
		for(int i = 0; i < rows * cols; ++i)
		{
			// Spring for the row
			if (i + rows < rows * cols)
			{
				SpringDamper RowSpring = Instantiate(SpringsPreb);
                RowSpring.transform.parent = transform;
                LineRenderer spring = RowSpring.GetComponent<LineRenderer>();
                spring.SetPosition(0, particles[i].transform.position);
                spring.SetPosition(1, particles[i + rows].transform.position);
                RowSpring.SetSpring(particles[i], particles[i + rows]);
                springDampers.Add(RowSpring);
			}
			
			// Spring for the colum
			if((i % cols) != rows - 1)
			{
				SpringDamper ColSpring = Instantiate(SpringsPreb);
                ColSpring.transform.parent = transform;
                LineRenderer spring = ColSpring.GetComponent<LineRenderer>();
                spring.SetPosition(0, particles[i].transform.position);
                spring.SetPosition(1, particles[i + 1].transform.position);
                ColSpring.SetSpring(particles[i], particles[i + 1]);
                springDampers.Add(ColSpring);

                // Right diagonal spring
                if (i + 1 < rows * cols && i + rows < rows * cols)
				{
					SpringDamper RightDSpring = Instantiate(SpringsPreb);
                    RightDSpring.transform.parent = transform;
                    LineRenderer springR = RightDSpring.GetComponent<LineRenderer>();
                    springR.SetPosition(0, particles[i + 1].transform.position);
                    springR.SetPosition(1, particles[i + rows].transform.position);
                    RightDSpring.SetSpring(particles[i + 1], particles[i + rows]);
                    springDampers.Add(RightDSpring);
                }
				
				// Left diagonal spring
				if(i + rows + 1 < rows * cols)
				{
					SpringDamper LeftDSpring = Instantiate(SpringsPreb);
                    LeftDSpring.transform.parent = transform;
                    LineRenderer springL = LeftDSpring.GetComponent<LineRenderer>();
                    springL.SetPosition(0, particles[i].transform.position);
                    springL.SetPosition(1, particles[i + rows + 1].transform.position);
                    LeftDSpring.SetSpring(particles[i], particles[i + rows + 1]);
                    springDampers.Add(LeftDSpring);
                }
				
			}
			
		}
	}

    void SetAnchor()
    {
        particles[10].GetComponent<Particle>().isAnchor = true;
        particles[65].GetComponent<Particle>().isAnchor = true;
        particles[120].GetComponent<Particle>().isAnchor = true;
    }

	void FixedUpdate()
	{
		// Compute Forces
		// For each particle apply gravity
		foreach (Particle o in particles) 
		{
			Vector3 gravityForce = new Vector3(0f , -9.8f , 0f) * o.mass; 
			o.Force = gravityForce;
		}

        //For each Spring - Damper compute and apply Forces
        foreach (SpringDamper s in springDampers)
        {
            s.computeForce();
        }

        //Intergrate Motion
        // For each particle apply Euler Intergration
        foreach (Particle o in particles)
        {
            o.EulerIntergration();
        }
    }

}
