using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MESSAGETYPE
{
    OK = 0,
    YES_NO = 1,
}
public class MessageBox : MonoBehaviour {

    public Text txtTitle;
    public Text txtContent;
    public Button btnOk;
    public Button btnLeft;
    public Button btnRight;
    public string title;
    public string content;
    public MESSAGETYPE type;
    public delegate void CallbackOk();
    public CallbackOk callback;
    public void ShowMessageBox(string content, MESSAGETYPE type, CallbackOk callback = null)
    {
        OnShow();
        this.title = "";
        this.content = content;
        this.type = type;
        this.txtTitle.text = this.title;
        this.txtContent.text = this.content;
        this.callback = callback;
        Init();
    }
    public void ShowMessageBox(string title, string content, MESSAGETYPE type, CallbackOk callback = null)
    {
        OnShow();
        this.title = title;
        this.content = content;
        this.type = type;
        this.txtTitle.text = this.title;
        this.txtContent.text = this.content;
        this.callback = callback;
        Init();
    }
    public void CloseMessageBox()
    {
        GUIManager.Instance.CloseMessageBox(this);
    }
    public void OnShow()
    {
        this.transform.localPosition = Vector3.zero;
        this.transform.localScale = Vector3.one;

    }
    private void onComplete()
    {
        Debug.LogWarning("On complete Tween");
    }
    private void onStart()
    {
        Debug.LogWarning("On Start Tween");
    }
    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
    private void Init()
    {
        switch (this.type)
        {
            case MESSAGETYPE.OK:
                this.btnLeft.gameObject.SetActive(false);
                this.btnRight.gameObject.SetActive(false);
                this.btnOk.gameObject.SetActive(true);
                this.btnOk.onClick.RemoveAllListeners();
                this.btnOk.onClick.AddListener(() => this.CloseMessageBox());
                break;
            case MESSAGETYPE.YES_NO:
                this.btnLeft.gameObject.SetActive(true);
                this.btnRight.gameObject.SetActive(true);
                this.btnOk.gameObject.SetActive(false);
                this.btnLeft.onClick.RemoveAllListeners();
                this.btnRight.onClick.AddListener(() => this.CloseMessageBox());
                if (callback != null)
                {
                    this.btnLeft.onClick.AddListener(() => callback.Invoke());
                    this.btnLeft.onClick.AddListener(() => this.CloseMessageBox());
                }
                break;
        }
    }
}
