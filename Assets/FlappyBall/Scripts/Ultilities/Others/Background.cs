using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	private MeshRenderer meshRenderer;
	private MeshFilter meshFilter;
	private Vector2 offset;
	public float scrollSpeed = 5f;

	// ------------------ MONO_BEHAVIOR -------------------- //

	protected void Start()
	{
		Init();
	}

	protected void Update()
	{
		
	}

	protected void LateUpdate()
	{
		//if (GameManager.Instance.state != GameState.PLAY)
		//{
		//	return;
		//}

		//Vector3 pos = Camera.main.transform.position;
		//transform.position = new Vector3(pos.x, pos.y);
	}

	// ------------------ GAME INVOLVES --------------------- //

	public void OnStart()
	{
		//meshRenderer.enabled = true;
	}

	public void OnEnd()
	{
		//meshRenderer.enabled = false;
	}

	// ---------------------- OTHERS ------------------------- //

	public void Init()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();

		float h = Camera.main.orthographicSize;
		float w = Camera.main.aspect * h;
		Vector3 tl = new Vector3(-w, h, 0);
		Vector3 tr = new Vector3(w, h, 0);
		Vector3 bl = new Vector3(-w, -h, 0);
		Vector3 br = new Vector3(w, -h, 0);
		Vector3[] verticles = new Vector3[4] { bl, tr, br, tl };

		meshFilter.mesh.vertices = verticles;
		offset = Vector2.zero;
	}

	public void Scroll(Vector2 offset, float speed)
	{
		meshRenderer.material.mainTextureOffset += offset * Time.deltaTime * speed;
	}
}
