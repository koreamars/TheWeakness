using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RivalAttackKeyF : BaseView
{
    public Text StateText;

    public override void Show()
    {
        Logger.Log("RivalAttackKeyF.Show");
        StateText.text = "";
    }

    public void UpdateAttackKey(ArrayList arrayList)
    {
        string keyStr = "";
        foreach(object key in arrayList)
        {
            keyStr += "/" + key;
        }

        StateText.text = keyStr;
    }
    
}