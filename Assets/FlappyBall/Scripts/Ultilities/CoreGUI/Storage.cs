using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoSingleton<Storage> {

	public List<Sprite> ballSprites;

	public Sprite GetBallSprite(int index)
	{
		if (index < ballSprites.Count && index >= 0)
		{
			return ballSprites[index];
		}
		else
		{
			return null;
		}
	}
}
