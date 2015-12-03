using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public Vector3 Position;
	public Vector3 Velocity;
	Vector3 Acceleration;
	public Vector3 Force;
	public float mass;

	void Start()
	{
		Position = transform.position;
		Force = new Vector3 (0f,-9.8f,0f) * mass;
		Acceleration = Force / mass;
	}

}
