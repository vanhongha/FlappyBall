using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SFX
{
	Crash, Click, Jump, Popup, InNest, Lose, Highscore
}

public class SoundManager : MonoSingleton<SoundManager> {

	protected bool music = true;
	protected bool sfx = true;

	public Sprite BgmOn_1;
	public Sprite BgmOff_1;
	public Sprite BgmOn_2;
	public Sprite BgmOff_2;
	public Sprite SfxOn_1;
	public Sprite SfxOff_1;
	public Sprite SfxOn_2;
	public Sprite SfxOff_2;

	public AudioSource backgroundMusic;

	public AudioSource crash;
	public AudioSource click;
	public AudioSource jump;
	public AudioSource popup;
	public AudioSource inNest;
	public AudioSource lose;
	public AudioSource highScore;

	public bool IsBackgroundPlaying()
	{
		return this.music;
	}

	public bool IsSfxPlaying()
	{
		return this.sfx;
	}

	public void ToggleSfx(bool isOn)
	{
		this.sfx = isOn;
		ToggleCertainSfx(crash, 0, 0.25f);
		ToggleCertainSfx(click, 0, 1);
		ToggleCertainSfx(jump, 0, 0.5f);
		ToggleCertainSfx(popup, 0, 1);
		ToggleCertainSfx(inNest, 0, 1f);
		ToggleCertainSfx(lose, 0, 1f);
		ToggleCertainSfx(highScore, 0, 1f);
	}

	public void ToggleCertainSfx(AudioSource sfx, float min, float max) 
	{
		if (sfx != null)
		{
			if (this.sfx)
			{
				sfx.volume = max;
			}
			else
			{
				sfx.volume = min;
			}
		}
	}

	public void ToggleMusic(bool isOn)
	{
		this.music = isOn;

		if (this.music)
		{
			backgroundMusic.volume = 0.2f;
		}
		else
		{
			backgroundMusic.volume = 0f;
		}
	}

	public void PlaySfx(SFX type)
	{
		switch (type)
		{
		case SFX.Crash: PlaySfx(crash, false); break;
		case SFX.Click:	PlaySfx(click); break;
		case SFX.Jump: PlaySfx(jump); break;
		case SFX.Popup: PlaySfx(popup); break;
		case SFX.InNest: PlaySfx(inNest); break;
		case SFX.Lose: PlaySfx(lose); break;
		case SFX.Highscore: PlaySfx(highScore); break;
		}
	}

	public void PlaySfx(AudioSource audio, bool stop = true)
	{
		if (audio != null)
		{
			if (audio.isPlaying && stop)
			{
				audio.Stop();
			}
			audio.Play();
		}
	}

	public void SetupSound(Image sprite, int type, bool toggle = false)
	{
		if (toggle)
		{
			if (type == 0 || type == 1)
			{
				ToggleMusic(!IsBackgroundPlaying());
			}
			else
			{
				ToggleSfx(!IsSfxPlaying());
			}
		}
		
		switch (type)
		{
		case 0: sprite.sprite = IsBackgroundPlaying() ? BgmOn_1 : BgmOff_1; break;
		case 1: sprite.sprite = IsBackgroundPlaying() ? BgmOn_2 : BgmOff_2; break;
		case 2: sprite.sprite = IsSfxPlaying() ? SfxOn_1 : SfxOff_1; break;
		case 3: sprite.sprite = IsSfxPlaying() ? SfxOn_2 : SfxOff_2; break;
		}
	}

	public void Start()
	{
		backgroundMusic.Play();
		ToggleSfx(true);
		ToggleMusic(true);
	}
}
