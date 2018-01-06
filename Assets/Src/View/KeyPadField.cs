using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadField : BaseView {

    public GameObject[] keyBtnList;

    // Use this for initialization
    void Start()
    {
        if (isUnitTest == true)
        {
            KeyPadFieldModel keyModel = new KeyPadFieldModel();
            SetData(keyModel);
            Show();
            Appear(null);
        }
    }

    public override void Show()
    {
        Logger.Log("KeyPadField.Show");
        int keyValue = 0;
        foreach(GameObject keyBtnObj in keyBtnList)
        {
            keyValue++;
            Logger.Log("keyValue " + keyValue);
            string newKeyName = keyValue + "";
            Button keyBtn = keyBtnObj.GetComponent<Button>();
            keyBtn.onClick.AddListener(delegate {OnKeyBtnClick(newKeyName);});
        }
    }

    public void OnSendBtnClick()
    {
        Logger.Log("KeyPadField.OnSendBtnClick");
        GameManager.GetInstance().SendMyWeak();
    }

    public void OnCancelClick()
    {
        Logger.Log("KeyPadField.OnCancelClick");
        GameManager.GetInstance().CancelWeakKey();
    }
    
    public void OnKeyBtnClick(string value)
    {
        Logger.Log("KeyPadField.OnKeyBtnClick value:" + value);
        GameManager.GetInstance().SaveMyWeakKey(int.Parse(value));
    }
}
