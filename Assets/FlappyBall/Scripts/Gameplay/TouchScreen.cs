using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour {

	protected BoxCollider2D boxCollider;

	protected void Start()
	{
		if (boxCollider = GetComponent<BoxCollider2D>())
		{
			boxCollider.size = new Vector2(GameManager.Instance.info.gameWidth, GameManager.Instance.info.gameHeight);
		}		
	}

	public void OnMouseDown()
	{
		if (GameManager.Instance.state != GameState.PLAY)
		{
			return;
		}

		GameManager.Instance.ballManager.AddForce();
	}
}
