using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreDialog : BaseDialog {

	public Text gameName;
	public Text diamond;
	public Transform panel;
	public ToggleGroup group;
	public GameObject itemPrebab;

	protected void Update()
	{
		diamond.text = UserProfile.Instance.GetDiamond().ToString();
	}

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		gameName.text = UserProfile.Instance.gameName;
		for (int i = 0; i < UserProfile.Instance.ballCount; i++)
		{
			GameObject item = Instantiate(itemPrebab, panel);
			item.GetComponent<ItemBall>().Init(i, 3000, group);
		}
		EffectShow();
	}

	public void OnClickIAP()
	{
		GUIManager.Instance.OnShowDialog<iAPDialog>("iAP");
	}

	public void OnClickHome()
	{
		EffectClose<GameStartDialog>("Start");
	}

	public void EffectClose<T>(string dialog) where T : BaseDialog
	{
		if (checkClick)
		{
			return;
		}
		checkClick = true;

		Vector3 offset = GameManager.Instance.info.canvasHeight * Vector3.down;
		transform.DOLocalPath(new Vector3[] { gameName.transform.localPosition + offset }, 0.75f)
			.OnComplete(delegate { base.OnCloseDialog(); GUIManager.Instance.OnShowDialog<T>(dialog); }); ;
	}

	public void EffectShow()
	{
		Vector3 offset = GameManager.Instance.info.canvasHeight * Vector3.down;
		transform.localPosition += offset ;
		gameObject.transform.DOLocalPath(new Vector3[] { transform.localPosition - offset }, 0.75f);
	}
}
