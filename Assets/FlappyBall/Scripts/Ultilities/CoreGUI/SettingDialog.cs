using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingDialog : BaseDialog {

	protected GameStartDialog start;
	protected Vector3 offset;
	public GameObject option;
	public Image bgm;
	public Image sfx;

	protected void Awake()
	{
		SoundManager.Instance.SetupSound(bgm, 0);
		SoundManager.Instance.SetupSound(sfx, 2);

	}

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		checkClick = true;
		start = (GameStartDialog)data;
		option.transform.position = new  Vector3(start.settings.transform.position.x, start.settings.transform.position.y);
        offset = new Vector3(0, -175);
        option.transform.localPosition += offset;
        //offset = new Vector3(0, 325f);
        //option.transform.DOLocalPath(new Vector3[] { (option.transform.localPosition + offset) }, 0.5f).
        //OnComplete(() => { checkClick = false; });
        option.transform.localScale = Vector3.zero;
        option.transform.DOScale(1f , 0.5f).
            OnComplete(() => { checkClick = false; });
    }

	public override void OnCloseDialog()
	{
		if (checkClick)
		{
			return;
		}
		checkClick = true;

		//start.HideSetting();
		//option.transform.DOLocalPath(new Vector3[] { (option.transform.localPosition - offset) }, 0.5f)
		//	.OnComplete(delegate { base.OnCloseDialog(); });
        option.transform.DOScale(0f, 0.5f).
            OnComplete(delegate { base.OnCloseDialog(); });
    }

	public void OnClickBgm()
	{
		SoundManager.Instance.SetupSound(bgm, 0, true);
	}

	public void OnClickSfx()
	{
		SoundManager.Instance.SetupSound(sfx, 2, true);
	}
}
