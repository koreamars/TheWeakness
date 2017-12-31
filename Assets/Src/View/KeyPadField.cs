using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadField : BaseView {

	public void OnSendBtnClick()
    {
        Logger.Log("KeyPadField.OnSendBtnClick");
        KeyPadFieldModel currentModel = _baseModel as KeyPadFieldModel;

        if(currentModel.sendBtnCall != null)
        {
            currentModel.sendBtnCall();
        }
    }
}
