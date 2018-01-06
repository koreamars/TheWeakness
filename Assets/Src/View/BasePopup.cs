using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : BaseView {

    private RectTransform _popupRect;

    public override void Appear(Action endCall)
    {
        _appearCallback = endCall;
        
        _popupRect = this.gameObject.GetComponent<RectTransform>();
        
        iTween.EaseType easeType = iTween.EaseType.easeOutBack;
        float moveTime = 0.3f;

        iTween.ValueTo(this.gameObject, iTween.Hash(
         "from", new Vector3(0f, 0f, 0f),
         "to", new Vector3(1f, 1f, 1f),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_UpdatePopupScale",
         "onComplete", "_PopupAppearCom",
         "onCompleteTarget", this.gameObject));
    }

    public override void Disappear(Action endCall)
    {
        _disappearCallback = endCall;

        iTween.EaseType easeType = iTween.EaseType.easeInBack;
        float moveTime = 0.2f;

        iTween.ValueTo(this.gameObject, iTween.Hash(
         "from", new Vector3(1f, 1f, 1f),
         "to", new Vector3(0f, 0f, 0f),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_UpdatePopupScale",
         "onComplete", "_PopupDisappearCom",
         "onCompleteTarget", this.gameObject));
    }

    private void _UpdatePopupScale(Vector3 scale)
    {
        Logger.Log("LobbyMainBtns._UpdatePopupScale");
        _popupRect.localScale = scale;
    }

    private void _PopupAppearCom()
    {
        Logger.Log("LobbyMainBtns._PopupAppearCom");
        if(_appearCallback != null)
        {
            _appearCallback();
        }
    }

    private void _PopupDisappearCom()
    {
        Logger.Log("LobbyMainBtns._PopupDisappearCom");
        if (_disappearCallback != null)
        {
            _disappearCallback();
        }
    }
    
}
