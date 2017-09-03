using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PreStartDialog : BaseDialog {

    public GameObject effect;

    public override void OnShow(Transform transf, object data)
    {
        base.OnShow(transf, data);
        effect.transform.DOScale(1.2f, 2f).SetLoops(-1, LoopType.Yoyo);
    }

    public override void OnHide()
    {
        System.Action callback = (System.Action)data;
        GameManager.Instance.ContinueGame();
        callback.Invoke();
        base.OnHide();
    }
}
