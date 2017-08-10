using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    private List<BaseDialog> baseDialogs;
    public Transform transfDialog;
    void Awake()
    {
        this.baseDialogs = new List<BaseDialog>();
    }

	public T OnShowDialog<T>(string path, object data = null) where T : BaseDialog
    {
        T target = ((GameObject)Instantiate(Resources.Load(path))).GetComponent<T>();
        if(target != null)
        {
            target.gameObject.SetActive(true);
            target.OnShow(this.transfDialog, data);
            this.baseDialogs.Add(target);
        }
        return target;
    }

    public void OnHideDialog(BaseDialog dialog)
    {
        dialog.OnHide();
        if(this.baseDialogs.Contains(dialog))
        {
            this.baseDialogs.Remove(dialog);
        }
    }
    public void OnHideAllDialog()
    {
        foreach(BaseDialog dialog in this.baseDialogs)
        {
            if (dialog != null)
                dialog.OnHide();
        }
    }
    public void ShowMessageBox(string title, string content, MESSAGETYPE type, MessageBox.CallbackOk callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("")) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(this.transfDialog);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        MessageBox message = obj.GetComponent<MessageBox>();
        if (message != null)
        {
            message.ShowMessageBox(title, content, type, callback);
        }
    }
    public void ShowMessageBox(string content, MESSAGETYPE type, MessageBox.CallbackOk callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("")) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(this.transfDialog);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        MessageBox message = obj.GetComponent<MessageBox>();
        if (message != null)
        {
            message.transform.SetParent(this.transform);
            message.transform.localScale = Vector3.one;
            message.transform.localPosition = Vector3.zero;
            message.ShowMessageBox(content, type, callback);
        }
    }
    public void CloseMessageBox(MessageBox message)
    {
        message.OnHide();
    }
}
