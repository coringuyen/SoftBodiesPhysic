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
		float rows = 5;
		float cols = 5;
		// Making a Grid
		for (int i = 0; i < row; ++i) 
		{
			for (int j = 0; i < col; ++i) 
			{
				GameObject particle = Instantiate (Particles) as GameObject;
				particle.transform.position = new Vector3 (i * 20 / rows, j * 20 / cols, 0);
				particles.Add (particle);
			}
		}
	}

    void Update()
    {
        for (int i = 0; i < 5; ++i)
        {
            Debug.DrawLine(new Vector3(i, 0f, 0f), new Vector3(i + 5f, 0f, 0f), Color.red);
            for (int j = 0; j < 5; ++j)
            {
                Debug.DrawLine(new Vector3(0f, j, 0f), new Vector3(0, j + 1, 0f), Color.red);
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
