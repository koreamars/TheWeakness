using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPopup : BasePopup {

	public void OnResultConfirm()
    {
        PopupController.GetInstance().HidePopup();
        GameManager.GetInstance().SceneUpdate(SceneType.LOBBY);
    }
}
