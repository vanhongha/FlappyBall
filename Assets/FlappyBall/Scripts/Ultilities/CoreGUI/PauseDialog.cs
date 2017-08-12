using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseDialog : BaseDialog {

	public Button soundButton;

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		UserProfile.Instance.SetupSound(soundButton, 1);
	}

	public void OnClickRestart()
	{
		GameManager.Instance.RestartGame();
		OnCloseDialog();
	}

	public void OnClickHome()
	{
		GameManager.Instance.EndGame();
		OnCloseDialog();
	}

	public void OnClickContinue()
	{
		GameManager.Instance.ContinueGame();
		OnCloseDialog();
	}

	public void OnClickSound()
	{
		SoundManager.Instance.ToggleMusic(!SoundManager.Instance.IsBackgroundPlaying());
		UserProfile.Instance.SetupSound(soundButton, 1);
	}
}
