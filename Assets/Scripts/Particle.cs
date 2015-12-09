using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour 
{
	public Vector3 Position;
	public Vector3 Velocity;
	Vector3 Acceleration;
	public Vector3 Force;
	public float mass;

	void Start()
	{
		Velocity = new Vector3 (0,0,0);
		Position = transform.position;
	}

	public void EulerIntergration()
	{
		Acceleration = Force / mass;
		Velocity = Velocity + Acceleration * Time.deltaTime;
		transform.position = Position + Velocity * Time.deltaTime;
	}
}
