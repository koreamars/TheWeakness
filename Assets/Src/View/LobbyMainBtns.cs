using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMainBtns : BaseView {

    // Use this for initialization
    void Start()
    {
        if(isUnitTest == true)
        {
            Appear(null);
        }
    }

    public RectTransform _userBtnRect = null;
    public RectTransform _friendBtnRect = null;

    public override void Appear(Action endCall)
    {
        Logger.Log("LobbyMainBtns.Appear");

        _appearCallback = endCall;

        GameObject userBtnObj = this.gameObject.transform.Find("UserMatchBtn").gameObject;
        GameObject friendBtnObj = this.gameObject.transform.Find("FriendMatchBtn").gameObject;
        _userBtnRect = userBtnObj.GetComponent<RectTransform>();
        _friendBtnRect = friendBtnObj.GetComponent<RectTransform>();

        iTween.EaseType easeType = iTween.EaseType.easeOutBack;
        float moveTime = 0.5f;

        iTween.ValueTo(_userBtnRect.gameObject, iTween.Hash(
         "from", _userBtnRect.anchoredPosition,
         "to", new Vector2(0, 77),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_UserBtnMove",
         "onComplete", "_AppearComplete",
         "onCompleteTarget", this.gameObject));

        iTween.ValueTo(_friendBtnRect.gameObject, iTween.Hash(
         "from", _friendBtnRect.anchoredPosition,
         "to", new Vector2(0, -78),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_FriendBtnMove"));

    }

    public void _UserBtnMove(Vector2 position)
    {
        Logger.Log("LobbyMainBtns._UserBtnMove");
        _userBtnRect.anchoredPosition = position;
    }

    public void _FriendBtnMove(Vector2 position)
    {
        Logger.Log("LobbyMainBtns._FriendBtnMove");
        _friendBtnRect.anchoredPosition = position;
    }

    private void _AppearComplete()
    {
        Logger.Log("LobbyMainBtns._AppearComplete");
        if (_appearCallback != null) _appearCallback();
    }

    public override void Disappear(Action endCall)
    {
        Logger.Log("LobbyMainBtns.Disappear");

        _disappearCallback = endCall;

        iTween.EaseType easeType = iTween.EaseType.easeInBack;
        float moveTime = 0.5f;

        iTween.ValueTo(_userBtnRect.gameObject, iTween.Hash(
         "from", _userBtnRect.anchoredPosition,
         "to", new Vector2(-350, 77),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_UserBtnMove",
         "onComplete", "_DisappearComplete",
         "onCompleteTarget", this.gameObject));

        iTween.ValueTo(_friendBtnRect.gameObject, iTween.Hash(
         "from", _friendBtnRect.anchoredPosition,
         "to", new Vector2(350, -78),
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_FriendBtnMove"));

    }

    private void _DisappearComplete()
    {
        Logger.Log("LobbyMainBtns._DisappearComplete");
        if (_disappearCallback != null) _disappearCallback();
    }

    public void UserMatchClick()
    {
        Logger.Log("LobbyMainBtns.UserMatchClick");
        GameManager.GetInstance().UserMatchFunc();
    }

    public void FriendMatchClick()
    {
        Logger.Log("LobbyMainBtns.FriendMatchClick");
        GameManager.GetInstance().FriendMatchFunc();
    }
}
