using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PopupController : MonoBehaviour {

    private static PopupController instance;
    private static GameObject container;
    public static PopupController GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "PopupController";
            instance = container.AddComponent(typeof(PopupController)) as PopupController;
        }
        return instance;
    }

    private GameObject _popupBg = null;
    public GameObject CurrentPopupObj = null;
    private GameObject _canvas = null;

    public void ShowPopup(string popupType, BasePopupModel popupModel, Action appearEndCall = null)
    {
        Logger.Log("PopupController.ShowPopup " + popupType);
        if (CurrentPopupObj != null)
        {
            HidePopup();
        }

        if(_canvas == null) _canvas = GameObject.Find("Canvas") as GameObject;

        _popupBg = ViewFactory.GetInstance().GetOnlyObjType(_canvas, "PopupBg");

        CurrentPopupObj = ViewFactory.GetInstance().GetViewObj(_canvas, "popup/" + popupType, popupModel);
        BasePopup currPopup = CurrentPopupObj.GetComponent<BasePopup>();
        currPopup.Show();
        currPopup.Appear(appearEndCall);
    }

    public void HidePopup()
    {
        Logger.Log("PopupController.ShowPopup " + CurrentPopupObj);
        BasePopup currPopup = CurrentPopupObj.GetComponent<BasePopup>();
        currPopup.Disappear(_HidePoupCom);
        

    }

    private void _HidePoupCom()
    {
        Logger.Log("PopupController._HidePoupCom " + CurrentPopupObj);
        Destroy(CurrentPopupObj);
        CurrentPopupObj = null;
        Destroy(_popupBg);
        _popupBg = null;
    }
}
