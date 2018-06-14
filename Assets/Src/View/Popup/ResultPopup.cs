using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : BasePopup {

    public Text commentTxt;

    public override void Show()
    {
        base.Show();

        ResultPopupModel currModel = _baseModel as ResultPopupModel;
        if(currModel.isWin == true)
        {
            commentTxt.color = Color.blue;
            commentTxt.text = "WIN GAME";
        } else
        {
            commentTxt.color = Color.red;
            commentTxt.text = "LOST GAME";
        }
        commentTxt.text += "\n enemy key :" + currModel.enemyKeyList[0] + "/" + currModel.enemyKeyList[1] + "/" + currModel.enemyKeyList[2];
    }

    public void OnResultConfirm()
    {
        PopupController.GetInstance().HidePopup();
        GameManager.GetInstance().SceneUpdate(SceneType.LOBBY);
    }
}
