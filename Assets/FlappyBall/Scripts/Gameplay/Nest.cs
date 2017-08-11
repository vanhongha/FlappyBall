using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour {

	public float distance;
	public GameObject obstaclePrefab;
	protected BoxCollider2D boxCollider;

	private bool scored;
	private Vector3 triggerPoint;
	private float farRight;

	public void OnStart()
	{
		scored = false;
		triggerPoint = Vector3.zero;

		GameObject obstacle_1 = Instantiate(obstaclePrefab, 
			new Vector2(transform.position.x - distance / 2, transform.position.y),
			Quaternion.identity, transform);

		GameObject obstacle_2 = Instantiate(obstaclePrefab,
			new Vector2(transform.position.x + distance / 2, transform.position.y),
			Quaternion.identity, transform);

		if (boxCollider = GetComponent<BoxCollider2D>())
		{
			boxCollider.size = new Vector2(distance, 0.1f);
		}

		farRight = transform.position.x + distance + 0.1f;
	}

	public void OnEnd()
	{
		Destroy(gameObject);
	}

	public void Update()
	{
		if (transform.position.x < Camera.main.transform.position.x - GameManager.Instance.info.gameWidth / 2 - 2f)
		{
			GameManager.Instance.nestManager.SpawnNest();
			OnEnd();
		}
		if (!scored && GameManager.Instance.ballManager.BallPosition().x > farRight)
		{
			GameManager.Instance.EndGame();
		}
	}

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Ball") && !scored)
		{
			triggerPoint = col.transform.position;
		}
	}

	protected void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Ball") && triggerPoint != Vector3.zero && triggerPoint.y > col.transform.position.y && !scored)
		{
			GameManager.Instance.AddScore();
			scored = true;
		}
	}
}
