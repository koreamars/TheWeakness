using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseView : MonoBehaviour {

    public bool isUnitTest = true;
    protected BaseViewModel _baseModel = null;
    protected Action _appearCallback = null;
    protected Action _disappearCallback = null;

	virtual public void SetData(BaseViewModel baseModel)
    {
        Logger.Log("BaseView.SetData");
        _baseModel = baseModel;
    }

    virtual public void Show()
    {
        Logger.Log("BaseView.Show");
    }

    virtual public void Appear(Action endCall)
    {
        Logger.Log("BaseView.Appear");

        _appearCallback = endCall;

        if (_appearCallback != null) _appearCallback();
    }

    virtual public void Disappear(Action endCall)
    {
        Logger.Log("BaseView.Appear");

        _disappearCallback = endCall;

        if (_disappearCallback != null) _disappearCallback();
    }
}
