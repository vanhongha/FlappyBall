using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public class ColorHSV
	{
		public float h;
		public float s;
		public float v;
		public ColorHSV() { }
		public ColorHSV(Color color)
		{
			Color.RGBToHSV(color, out h, out s, out v);
		}
		public Color ToColor()
		{
			return Color.HSVToRGB(h, s, v);
		}
		public Color ToColorOffset(float h, float s, float v)
		{
			return Color.HSVToRGB(this.h + h, this.s + s, this.v + v);
		}
	}

	private MeshRenderer meshRenderer;
	private MeshFilter meshFilter;
	private Vector2 offset;
	private Image image;
	private Color startColor;
	private Color endColor;
	public float scrollSpeed = 5f;

	// ------------------ MONO_BEHAVIOR -------------------- //

	protected void Start()
	{
		image = GetComponent<Image>();
		startColor = image.color;
		endColor = new ColorHSV(startColor).ToColorOffset(0, 0.05f, 0);
	}

	protected void Update()
	{
		image.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 3f));
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
