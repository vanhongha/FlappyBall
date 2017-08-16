using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Nest : MonoBehaviour {

	public float distance;
	public GameObject obstaclePrefab;
	protected BoxCollider2D boxCollider;

	private bool scored;
	private Vector3 triggerPoint;
	private float farRight;
	private Vector3 finalPosition;
	private GameObject obstacle_1, obstacle_2;
	private LineRenderer line;
	[HideInInspector]
	public bool moving = false;

	public void OnStart(bool moving = false)
	{
		this.moving = moving;
		scored = false;
		triggerPoint = Vector3.zero;

		obstacle_1 = Instantiate(obstaclePrefab, 
			new Vector2(transform.position.x - distance / 2, transform.position.y),
			Quaternion.identity, transform);

		obstacle_2 = Instantiate(obstaclePrefab,
			new Vector2(transform.position.x + distance / 2, transform.position.y),
			Quaternion.identity, transform);

		line = GetComponent<LineRenderer>();
		line.positionCount = 3;
		line.SetPositions(new Vector3[] { obstacle_1.transform.localPosition, Vector3.zero, obstacle_2.transform.localPosition });

		if (boxCollider = GetComponent<BoxCollider2D>())
		{
			boxCollider.size = new Vector2(distance, 0.1f);
		}
		farRight = transform.position.x + distance + 0.1f;
		finalPosition = transform.position;
		transform.position = new Vector3(transform.position.x, GameManager.Instance.info.gameHeight + 1, transform.position.z);
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
		if (transform.position.x < Camera.main.transform.position.x + GameManager.Instance.info.gameWidth / 2)
		{
			transform.DOPath(new Vector3[] { finalPosition }, 0.25f);
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
			EffectManager.Instance.InNest(transform.position);
			GameManager.Instance.AddScore();
			obstacle_1.GetComponent<BoxCollider2D>().enabled = false;
			obstacle_2.GetComponent<BoxCollider2D>().enabled = false;
			obstacle_1.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.25f)
				.OnComplete(delegate () { Destroy(obstacle_1); });
			obstacle_2.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.25f)
				.OnComplete(delegate () { Destroy(obstacle_2); });
			line.enabled = false;
			scored = true;
		}
	}
}
