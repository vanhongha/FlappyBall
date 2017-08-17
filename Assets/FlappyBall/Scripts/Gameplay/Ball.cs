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
		GetComponent<SpriteRenderer>().sprite = Storage.Instance.GetBallSprite(UserProfile.Instance.GetCurrentBall());
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
		EffectManager.Instance.Tap(transform.position, 0.5f);
	}

	public void FixedUpdate()
	{
		if (GameManager.Instance.state == GameState.PAUSE)
		{
			return;
		}

		if (rigidBody.velocity.x < speed)
		{
			rigidBody.AddForce(Vector2.right * speed * 3);
		}
		else if (rigidBody.velocity.x > speed)
		{
			rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
		}
	}

	public void LateUpdate()
	{
		float x = transform.position.x + GameManager.Instance.info.gameWidth / 2 - 1.5f;
		Vector3 temp = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(x, 0, Camera.main.transform.position.z);
		GameManager.Instance.play.ScrollBackground(Camera.main.transform.position - temp, 3f);
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		EffectManager.Instance.Crash(collision.contacts[0].point);
		if (collision.collider.CompareTag("Border"))
		{
			GameManager.Instance.EndGame();
		}
	}

	public void ChangeSpeed(float speed)
	{
		this.speed = speed;
	}
}
