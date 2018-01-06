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

    public void UpdateValue(ArrayList weakList)
    {
        Logger.Log("MyW​eakField.UpdateValue");
        int idx = 0;
        foreach (GameObject weakValue in WeakValueList)
        {
            Text weakTxt = weakValue.GetComponent<Text>();
            if(weakList.Count > idx)
            {
                weakTxt.text = weakList[idx] + "";
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

    virtual protected void _weakRootMoveEnd()
    {
        Logger.Log("MyW​eakField._weakRootMoveEnd >" + _appearCallback);
        if(_appearCallback != null)
        {
            _appearCallback();
            _appearCallback = null;
        }
    }

}
