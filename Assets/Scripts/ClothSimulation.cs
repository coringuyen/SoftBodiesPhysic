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
		for (int i = 0; i < 5; ++i) 
            for(int j = 0; j < 5; ++j)
		    {
			    GameObject particle = Instantiate(Particles) as GameObject;
			    particle.transform.position = new Vector3(i * 20 / 5, j * 20 / 5, 0);
			    particles.Add(particle);
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
