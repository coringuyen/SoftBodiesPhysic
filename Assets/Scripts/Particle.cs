using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour 
{ 
    // this game object position is will assign to Position
	public Vector3 Position
    {
            get { return transform.position; }
            set { transform.position = value; }
    }

	public Vector3 Velocity;
	public Vector3 Force;
	public float mass;
	public bool isAnchor;

	void Start()
	{
		Velocity = new Vector3 (0,0,0);
		Position = transform.position;
	}

	public void EulerIntegration()
	{
        if (!isAnchor)
        {
            Vector3 Acceleration = Force / mass;
            Velocity += Acceleration * Time.deltaTime;
            Position += Velocity * Time.deltaTime;
        }
	}
}
