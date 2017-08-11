using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Ball"))
		{
			GameManager.Instance.EndGame();
		}
	}
}
