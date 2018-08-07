using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GenericMovement {

	public Rigidbody2D enemy;
    protected float direction;

	// Use this for initialization
	protected override void Start () {
		enemy = GetComponent<Rigidbody2D>();
		direction = 1.0f;

		//Changeing direction of enemy moving
		InvokeRepeating ("ChangeDirection", 2.0f, 2.0f);

		base.Start ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Moving of enemys
		enemy.velocity = new Vector2 (direction * speed, enemy.velocity.y);
		if (direction == -1.0f && !directionR) {
			Flip ();
		} else if (direction != -1.0f && directionR) {
			Flip ();
		}
	}

	void ChangeDirection() {
		if (direction == 1.0f) {
			direction = -1.0f;
		} else if (direction != 1.0f) {
			direction = 1.0f;
		}
	}

	protected override void Flip()
	{
		base.Flip ();
	}
}
