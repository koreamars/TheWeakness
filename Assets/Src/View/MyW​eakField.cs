using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyW​eakField : BaseView {

    public GameObject WeakRoot;
    public GameObject[] WeakValueList;

    private RectTransform _rootRect;

	// Use this for initialization
	void Start () {
        if (isUnitTest == true)
        {
            MyWkFieldModel fakeModel = new MyWkFieldModel();
            SetData(fakeModel);
            Show();
            Appear(null);
        }
    }

    public override void Show()
    {
        Logger.Log("MyW​eakField.Show");
        _rootRect = WeakRoot.GetComponent<RectTransform>();

        ResetValue();
    }

    public override void Appear(Action endCall)
    {
        Logger.Log("MyW​eakField.Appear");
        _appearCallback = endCall;
        UpdateWeakRoot(new Vector2(0, -67));
        
    }

    public virtual void UpdateValue()
    {
        Logger.Log("MyW​eakField.UpdateValue");
        int idx = 0;
        foreach (GameObject weakValue in WeakValueList)
        {
            Text weakTxt = weakValue.GetComponent<Text>();
            if(GameData.currBattleRoomModel.currMyWeakList.Count > idx)
            {
                if (GameData.currBattleRoomModel.currAttackedKeys.Count >= 3)
                {
                    // 상대 공격에 의한 판정 보여주기.
                    int enemyKey = Convert.ToInt32(GameData.currBattleRoomModel.currAttackedKeys[idx]);
                    int checkValue = Convert.ToInt32(GameData.currBattleRoomModel.currMyWeakList[idx]);
                    if(checkValue.ToString() == enemyKey.ToString())
                    {
                        weakTxt.color = Color.red;
                    }


                } else
                {
                    weakTxt.text = GameData.currBattleRoomModel.currMyWeakList[idx] + "";
                }
            } else
            {
                weakTxt.text = "?";
            }
            idx++;
        }
    }

    public void ResetValue()
    {
        Logger.Log("MyW​eakField.ResetValue");
        foreach (GameObject weakValue in WeakValueList)
        {
            Text weakTxt = weakValue.GetComponent<Text>();
            weakTxt.text = "?";
        }
    }

    public void UpdateWeakRoot(Vector2 position)
    {
        Logger.Log("MyW​eakField.UpdateWeakRoot");
        iTween.EaseType easeType = iTween.EaseType.easeOutBack;
        float moveTime = 0.5f;

        iTween.ValueTo(WeakRoot, iTween.Hash(
         "from", _rootRect.anchoredPosition,
         "to", position,
         "time", moveTime,
         "easeType", easeType,
         "onupdatetarget", this.gameObject,
         "onupdate", "_updateWeakRootPos",
         "onComplete", "_weakRootMoveEnd",
         "onCompleteTarget", this.gameObject));
    }

    private void _updateWeakRootPos(Vector2 pos)
    {
        Logger.Log("MyW​eakField._updateWeakRootPos");
        _rootRect.anchoredPosition = pos;
    }

    protected virtual void _weakRootMoveEnd()
    {
        Logger.Log("MyW​eakField._weakRootMoveEnd >" + _appearCallback);
        if(_appearCallback != null)
        {
            _appearCallback();
            _appearCallback = null;
        }
    }

}
