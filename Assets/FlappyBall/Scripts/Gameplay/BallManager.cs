﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	public GameObject ballPrefab;
	public float speed = 2f;
	public float force = 2f;
	[HideInInspector]
	public float scaleForce = 1f;
	private Ball ball;

	public void OnStart()
	{
		scaleForce = 1f;
		Vector2 startPosition = new Vector2(transform.position.x - GameManager.Instance.info.gameWidth / 2 + 1f, 0);
		GameObject ballObject = Instantiate(ballPrefab, startPosition, Quaternion.identity, transform);
		ball = ballObject.GetComponent<Ball>();
		ball.OnStart(speed);
	}

	public void OnPause()
	{
		if (ball != null)
		{
			ball.OnPause();
		}
	}

	public void OnContinue()
	{
		if (ball != null)
		{
			ball.OnContinue();
		}
	}

	public void OnEnd()
	{
		if (ball != null)
		{
			Destroy(ball.gameObject);
		}
	}

	public void AddForce()
	{
		if (ball != null)
		{
			ball.AddForce(force * scaleForce);
		}
	}

	public Vector3 BallPosition()
	{
		return ball.transform.position;
	}

	public void ChangeSpeed(float scale)
	{
		ball.ChangeSpeed(scale * speed);
	}
}
