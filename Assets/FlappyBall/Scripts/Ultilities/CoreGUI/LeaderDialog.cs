using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderDialog : BaseDialog {

	public Text gameName;

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		gameName.text = UserProfile.Instance.gameName;
	}
}
