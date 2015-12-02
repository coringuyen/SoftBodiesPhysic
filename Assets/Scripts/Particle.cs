using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

    public Vector3 Pos;
    public Vector3 Vel;
    Vector3 momentum;
    Rigidbody rb;
    public Vector3 force;
    public Vector3 acceleration;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Pos = transform.position;
        Vel = rb.velocity;
	}

    void FixedUpdate()
    {
        acceleration = force / rb.mass;
        momentum = rb.mass * Vel;

        // Euler Intergration
        Vel = Vel + acceleration * Time.deltaTime;
        rb.velocity = Vel;

        Pos = Pos + Vel * Time.deltaTime;
        transform.position = Pos;
    }
}
