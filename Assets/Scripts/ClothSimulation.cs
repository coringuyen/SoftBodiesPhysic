using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public Particle ParticlesPreb; 
    public SpringDamper SpringsPreb;
   
    List<Particle> particles = new List<Particle>();
	List<SpringDamper> springDampers = new List<SpringDamper>();
    List<Triangle> triangles = new List<Triangle>();

	public int rows; // how many particle for row
	public int cols; // how many particle for column
	public int width, height; // width and height of the Grid entirely 

    public Slider SpringConstant;
    public Slider DampingFactor;
    public Slider RestLength;
    public Slider AirBlow;

    void FixedUpdate()
    {
        // Compute Forces
        // For each particle apply gravity
        foreach (Particle o in particles)
        {
            Vector3 gravityForce = new Vector3(0f, -9.8f, 0f) * o.mass;
            o.Force = gravityForce;
        }

        //For each Spring - Damper compute and apply Forces
        foreach (SpringDamper s in springDampers)
        {
            s.SpringConstant = SpringConstant.value;
            s.DampingFactor = DampingFactor.value;
            s.Restlength = RestLength.value;
            s.computeForce();
        }

        // for each triangle compute and apply Aerodynamic Force
        //foreach(Triangle t in triangles)
        //{
        //    //t.airVelocity.z = AirBlow.value;
        //    t.computeForce();
        //}

        //Intergrate Motion
        // For each particle apply Euler Intergration
        foreach (Particle o in particles)
        {
            o.EulerIntergration();
        }
    }


    void Update()
    {
        // Update spring as the particle position change
        int i = 0;
        foreach (SpringDamper s in springDampers)
        {
            LineRenderer spring = s.GetComponent<LineRenderer>();
            spring.SetPosition(0, springDampers[i].p1.Position);
            spring.SetPosition(1, springDampers[i].p2.Position);
            i++;
        }
    }

    // Generate a Grid
    public void ClothSpawn()
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
                spring.SetPosition(0, particles[i].Position);
                spring.SetPosition(1, particles[i + rows].Position);
                RowSpring.SetSpring(particles[i], particles[i + rows]);
                springDampers.Add(RowSpring);
			}
			
			// Spring for the colum
			if((i % cols) != rows - 1)
			{
				SpringDamper ColSpring = Instantiate(SpringsPreb);
                ColSpring.transform.parent = transform;
                LineRenderer spring = ColSpring.GetComponent<LineRenderer>();
                spring.SetPosition(0, particles[i].Position);
                spring.SetPosition(1, particles[i + 1].Position);
                ColSpring.SetSpring(particles[i], particles[i + 1]);
                springDampers.Add(ColSpring);

                // Right diagonal spring
                if (i + 1 < rows * cols && i + rows < rows * cols)
				{
					SpringDamper RightDSpring = Instantiate(SpringsPreb);
                    RightDSpring.transform.parent = transform;
                    LineRenderer springR = RightDSpring.GetComponent<LineRenderer>();
                    springR.SetPosition(0, particles[i + 1].Position);
                    springR.SetPosition(1, particles[i + rows].Position);
                    RightDSpring.SetSpring(particles[i + 1], particles[i + rows]);
                    springDampers.Add(RightDSpring);
                }
				
				// Left diagonal spring
				if(i + rows + 1 < rows * cols)
				{
					SpringDamper LeftDSpring = Instantiate(SpringsPreb);
                    LeftDSpring.transform.parent = transform;
                    LineRenderer springL = LeftDSpring.GetComponent<LineRenderer>();
                    springL.SetPosition(0, particles[i].Position);
                    springL.SetPosition(1, particles[i + rows + 1].Position);
                    LeftDSpring.SetSpring(particles[i], particles[i + rows + 1]);
                    springDampers.Add(LeftDSpring);
                }
				
			}
        }

        // Make Triangle
        for (int i = 0; i < rows * cols; ++i)
        {
            if (i + 1 < rows * cols && i + rows < rows * cols && i + rows + 1 < rows * cols)
            {
                Triangle firstTriangle = GetComponent<Triangle>();
                firstTriangle.makeTraingle(particles[i], particles[i + 1], particles[i + rows]);
                triangles.Add(firstTriangle);

                Triangle secondTriangle = GetComponent<Triangle>();
                secondTriangle.makeTraingle(particles[i], particles[i + 1], particles[i + rows + 1]);
                triangles.Add(secondTriangle);

                Triangle thirdTriangle = GetComponent<Triangle>();
                thirdTriangle.makeTraingle(particles[i + 1], particles[i + rows], particles[i + rows + 1]);
                triangles.Add(thirdTriangle);

                Triangle fourTriangle = GetComponent<Triangle>();
                fourTriangle.makeTraingle(particles[i], particles[i + rows], particles[i + rows + 1]);
                triangles.Add(fourTriangle);
            }
        }

        SetAnchor();
    }

    void SetAnchor()
    {
        particles[9].GetComponent<Particle>().isAnchor = true;
        particles[99].GetComponent<Particle>().isAnchor = true;
        //particles[0].GetComponent<Particle>().isAnchor = true;
        //particles[90].GetComponent<Particle>().isAnchor = true;
    }
}
