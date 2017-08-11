using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	protected float speed = 2f;
	protected Rigidbody2D rigidBody;
	protected Vector2 tempVelocity;
	protected float time;

	public void OnStart(float speed)
	{
		this.speed = speed;
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity += Vector2.right * speed;
		rigidBody.gravityScale = 1.5f;
	}

	public void OnPause()
	{
		tempVelocity = rigidBody.velocity;
		rigidBody.velocity = Vector2.zero;
		rigidBody.gravityScale = 0f;
	}

	public void OnContinue()
	{
		rigidBody.velocity = tempVelocity;
		rigidBody.gravityScale = 1.5f;
	}

	public void OnEnd()
	{
		Destroy(gameObject);
	}

	public void AddForce(float force)
	{
		rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
		rigidBody.AddForce(new Vector2(0f, force * 50));
	}

	public void FixedUpdate()
	{
		if (GameManager.Instance.state == GameState.PAUSE)
		{
			return;
		}

		if (rigidBody.velocity.x < speed)
		{
			rigidBody.AddForce(Vector2.right * speed);
		}
		else if (rigidBody.velocity.x > speed)
		{
			rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
		}
	}

	public void LateUpdate()
	{
		float x = transform.position.x + GameManager.Instance.info.gameWidth / 2 - 1f;
		Camera.main.transform.position = new Vector3(x, 0, Camera.main.transform.position.z);
	}
}
