using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour {

    public bool isUnitTest = true;
    protected BaseViewModel _baseModel = null;

	virtual public void SetData(BaseViewModel baseModel)
    {
        Logger.Log("BaseView.SetData");
        _baseModel = baseModel;
    }

    virtual public void Show()
    {
        Logger.Log("BaseView.Show");
    }
}
