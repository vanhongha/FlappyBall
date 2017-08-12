using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreDialog : BaseDialog {

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
		for (int i = 0; i < 15; i++)
		{
			GameObject item = Instantiate(itemPrebab, panel);
			item.GetComponent<ItemBall>().Init(i, 20000, group);
		}
	}

	public void OnClickIAP()
	{
		GUIManager.Instance.OnShowDialog<iAPDialog>("iAP");
	}

	public void OnClickHome()
	{
		GUIManager.Instance.OnShowDialog<GameStartDialog>("Start");
		OnCloseDialog();
	}
}
