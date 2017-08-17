using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseDialog : BaseDialog {

	public Button soundButton;
	public Image bgm;
	public Image sfx;
	public GameObject content;

	protected void Awake()
	{
		SoundManager.Instance.SetupSound(bgm, 1);
		SoundManager.Instance.SetupSound(sfx, 3);
	}

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		EffectShow();
	}

	public void OnClickHome()
	{
		EffectClose(GameManager.Instance.EndGame);
	}

	public void OnClickContinue()
	{
		EffectClose(GameManager.Instance.ContinueGame);
	}

	public void OnClickSfx()
	{
		SoundManager.Instance.SetupSound(sfx, 3, true);
	}

	public void OnClickBgm()
	{
		SoundManager.Instance.SetupSound(bgm, 1, true);
	}

	public virtual void EffectClose(System.Action callback)
	{
		if (checkClick)
		{
			return;
		}
		checkClick = true;

		content.transform.DOScale(0, 0.5f)
			.OnComplete(delegate { base.OnCloseDialog(); callback(); });
	}

	public virtual void EffectShow()
	{
		checkClick = true;
		content.transform.localScale = Vector3.zero;
		content.transform.DOScale(1, 0.5f)
			.OnComplete(delegate { checkClick = false; }); ;
	}
}
