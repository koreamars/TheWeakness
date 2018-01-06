using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertPopup : BasePopup
{

    public Text titleTxt;
    public Text commentTxt;
	// Use this for initialization
	void Start () {
		
	}

    public override void Show()
    {
        Logger.Log("AlertPopup.Show");
        AlertPopupModel currModel = _baseModel as AlertPopupModel;
        titleTxt.text = currModel.title;
        commentTxt.text = currModel.comment;

    }

    public void OnOkClick()
    {
        Logger.Log("AlertPopup.OnOkClick");
        PopupController.GetInstance().HidePopup();
    }

}
