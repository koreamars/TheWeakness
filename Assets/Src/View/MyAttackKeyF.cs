using UnityEngine;
using UnityEngine.UI;

public class MyAttackKeyF : BaseView
{
    public Text StateText;

    public override void Show()
    {
        Logger.Log("MyAttackKeyF.Show");
        StateText.text = "";
    }

    public void UpdateValue()
    {
        Logger.Log("MyAttackKeyF.UpdateValue");
        int idx = 0;
        string valueStr = "";
        Logger.Log("GameData.currBattleRoomModel.currAttackKeys " + GameData.currBattleRoomModel.currAttackKeys);
        foreach (int keyValue in GameData.currBattleRoomModel.currAttackKeys)
        {
            if(idx == 0)
            {
                valueStr += keyValue.ToString();
            } else
            {
                valueStr += " / " + keyValue.ToString();
            }
            idx++;
        }
        StateText.text = valueStr;
    }
}