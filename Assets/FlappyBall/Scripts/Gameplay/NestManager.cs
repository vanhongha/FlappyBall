using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestManager : MonoBehaviour {

	public GameObject nestPrefab;
	public float minDistance = 2f;
	public float maxDistance = 3f;
	public float minHeight = -3f;
	public float maxHeight = 3f;
	public float minRotate = 0f;
	public float maxRotate = 45f;

	public float speed = 1f;
	public int maxCount = 2;
	public bool touchBall = false;

	protected List<Nest> nests = new List<Nest>();
	protected GameInfo info;

	protected void Start()
	{
		info = GameManager.Instance.info;
	}

	public void OnStart()
	{
		SpawnNest();
	}

	public void OnEnd()
	{
		for (int i = 0; i < nests.Count; i++)
		{
			if (nests[i] != null)
			{
				nests[i].OnEnd();
			}
		}
		nests.Clear();
	}

	public void SpawnNest()
	{
		while (transform.childCount < maxCount)
		{
			Vector3 position;
			if (nests.Count < 1)
			{
				position = new Vector3(
					transform.position.x + Random.Range(minDistance, maxDistance),
					Random.Range(minHeight, maxHeight), 0f);
			}
			else
			{
				position = new Vector3(
					nests[nests.Count - 1].transform.position.x + Random.Range(minDistance, maxDistance),
					Random.Range(minHeight, maxHeight), 0f);
			}
			Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minRotate, maxRotate));

			GameObject nestObject = Instantiate(nestPrefab, transform);
			nests.Add(nestObject.GetComponent<Nest>());
			nestObject.transform.position = position;
			nests[nests.Count - 1].OnStart();
			nestObject.transform.rotation = rotation;
		}
	}
}
