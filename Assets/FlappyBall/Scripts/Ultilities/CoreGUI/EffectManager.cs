using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager> {

	public GameObject inNest;
	public GameObject tap;
	public GameObject lose;
	public GameObject crash;

	public void InNest(Vector3 position, float scale = 1)
	{
		GameObject effect = (GameObject)Instantiate(inNest, position, Quaternion.identity, transform);
		effect.transform.localScale = new Vector3(scale, scale, scale);
	}

	public void Tap(Vector3 position, float scale = 1)
	{
		GameObject effect = (GameObject)Instantiate(tap, position, Quaternion.identity, transform);
		effect.transform.localScale = new Vector3(scale, scale, scale);
	}

	public void Lose(Vector3 position, float scale = 1)
	{
		GameObject effect = (GameObject)Instantiate(tap, position, Quaternion.identity, transform);
		effect.transform.localScale = new Vector3(scale, scale, scale);
	}

	public void Crash(Vector3 position, float scale = 1)
	{
		GameObject effect = (GameObject)Instantiate(crash, position, Quaternion.identity, transform);
		effect.transform.localScale = new Vector3(scale, scale, scale);
	}

}
