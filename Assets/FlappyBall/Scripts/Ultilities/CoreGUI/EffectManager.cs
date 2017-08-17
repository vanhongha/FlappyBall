using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager> {

	public GameObject inNest;
	public GameObject tap;
	public GameObject lose;
	public GameObject crash;
	protected List<GameObject> effects = new List<GameObject>();

	public void InNest(Vector3 position, float scale = 1)
	{
		ShowEffect(inNest, position, scale);
		SoundManager.Instance.PlaySfx(SFX.InNest);
	}

	public void Tap(Vector3 position, float scale = 1)
	{
		ShowEffect(tap, position, scale);
		SoundManager.Instance.PlaySfx(SFX.Jump);
	}

	public void Lose(Vector3 position, float scale = 1)
	{
		ShowEffect(lose, position, scale);
	}

	public void Crash(Vector3 position, float scale = 1)
	{
		ShowEffect(crash, position, scale);
		SoundManager.Instance.PlaySfx(SFX.Crash);
	}

	public void ShowEffect(GameObject effect , Vector3 position, float scale)
	{
		GameObject effectInstance = (GameObject)Instantiate(effect, position, Quaternion.identity, transform);
		effectInstance.transform.localScale = new Vector3(scale, scale, scale);
		effects.Add(effectInstance);
	}

	public void Clear()
	{
		if (effects != null)
		{
			foreach (GameObject effect in effects)
			{
				if (effect != null)
				{
					Destroy(effect);
				}
			}
			effects.Clear();
		}
	}
}
