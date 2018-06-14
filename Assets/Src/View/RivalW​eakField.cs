using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RivalW​eakField : MyW​eakField
{

    public override void Appear(Action endCall)
    {
        Logger.Log("RivalW​eakField.Appear");
        UpdateWeakRoot(new Vector2(-84, 258));

        _appearCallback = endCall;
    }

    protected override void _weakRootMoveEnd()
    {
        Logger.Log("RivalW​eakField._weakRootMoveEnd");
        base._weakRootMoveEnd();
    }
    
    public override void UpdateValue()
    {
        Logger.Log("RivalW​eakField.UpdateValue");

        int idx = 0;
        foreach (GameObject weakValue in WeakValueList)
        {
            Text weakTxt = weakValue.GetComponent<Text>();
            ArrayList resultStates = GameData.currBattleRoomModel.currRivalWeakStates;
            ArrayList answerList = GameData.currBattleRoomModel.currRivalAnswerList;
            
            string currState = resultStates[idx].ToString();
            
            if (currState == KeyConfirmType.ALL_MATCHING)
            {
                weakTxt.color = Color.blue;
                weakTxt.text = answerList[idx].ToString();

            } else if (currState == KeyConfirmType.POS_MATCHING)
            {
                weakTxt.color = Color.green;
            } else
            {
                weakTxt.color = Color.black;
            }

            idx++;
        }
    }

}
