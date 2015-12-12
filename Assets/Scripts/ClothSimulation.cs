using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothSimulation : MonoBehaviour 
{
	public Particle ParticlesPreb; 
    public SpringDamper SpringsPreb;
    public Triangle TrianglePreb;
   
    List<Particle> particles = new List<Particle>();
	List<SpringDamper> springDampers = new List<SpringDamper>();
    List<Triangle> triangles = new List<Triangle>();

	public int rows; // how many particle for row
	public int cols; // how many particle for column
	public int width, height; // width and height of the Grid entirely 

    ClothGUI clothgui;

    void Start()
    {
        clothgui = GetComponent<ClothGUI>();
    }

    void FixedUpdate()
    {
        // Compute Forces
        // For each particle apply gravity
        foreach (Particle o in particles)
        {
            if (o)
            {
                Vector3 gravityForce = new Vector3(0f, -9.8f, 0f) * o.mass;
                o.Force = gravityForce;
            }
        }

        //For each Spring - Damper compute and apply Forces
        foreach (SpringDamper s in springDampers)
        {
            if (s)
            {
                s.SpringConstant = clothgui.SpringConstant.value;
                s.DampingFactor = clothgui.DampingFactor.value;
                s.Restlength = clothgui.RestLength.value;
                s.computeForce();
            }
        }

        // for each triangle compute and apply Aerodynamic Force
        foreach (Triangle t in triangles)
        {
            if (t)
            {
                t.airVelocity.z = clothgui.AirBlow.value;
                t.computeForce();
            }
        }

        //Intergrate Motion
        // For each particle apply Euler Intergration
        foreach (Particle o in particles)
        {
            if (o)
            { o.EulerIntegration(); }
        }
    }


    void Update()
    {
        // Credit: Mathhew
        // Update spring as the particle position change
        int i = 0;
        foreach (SpringDamper s in springDampers)
        {
            if (s)
            {
                LineRenderer spring = s.GetComponent<LineRenderer>();
                spring.SetPosition(0, springDampers[i].p1.Position);
                spring.SetPosition(1, springDampers[i].p2.Position);
                i++;
            }
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
                Triangle firstTriangle = Instantiate(TrianglePreb);
                firstTriangle.transform.parent = transform;
                firstTriangle.makeTriangle(particles[i], particles[i + 1], particles[i + rows]);
                triangles.Add(firstTriangle);

                Triangle secondTriangle = Instantiate(TrianglePreb);
                secondTriangle.transform.parent = transform;
                secondTriangle.makeTriangle(particles[i], particles[i + 1], particles[i + rows + 1]);
                triangles.Add(secondTriangle);

                Triangle thirdTriangle = Instantiate(TrianglePreb);
                thirdTriangle.transform.parent = transform;
                thirdTriangle.makeTriangle(particles[i + 1], particles[i + rows], particles[i + rows + 1]);
                triangles.Add(thirdTriangle);

                Triangle fourTriangle = Instantiate(TrianglePreb);
                fourTriangle.transform.parent = transform;
                fourTriangle.makeTriangle(particles[i], particles[i + rows], particles[i + rows + 1]);
                triangles.Add(fourTriangle);
            }
        }

        SetAnchor();
    }

    void SetAnchor()
    {
        particles[0].isAnchor = true;
        particles[cols - 1].isAnchor = true;
        //particles[rows * cols - 1].isAnchor = true;
        //particles[rows * cols - cols].isAnchor = true;
    }

    public void DestroyCloth()
    {
        foreach (Particle p in particles)
        {
            Destroy(p.gameObject);
        }
        foreach (SpringDamper s in springDampers)
        {
            Destroy(s.gameObject);
        }

        foreach (Triangle t in triangles)
        {
            Destroy(t.gameObject);
        }

        springDampers = new List<SpringDamper>();
        particles = new List<Particle>();
        triangles = new List<Triangle>();
    }
}
