using System.Collections;
using System.Collections.Generic;
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

    public void ShowPopup(string popupType)
    {

    }

    public void HidePopup(string popupType)
    {

    }
}
