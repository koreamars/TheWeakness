using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReadyPopup : BasePopup {

    public Text Comment;
    
    public override void Show()
    {
        Comment.text = "Battle Ready!";
        base.Show();
    }
    
    public void UpdateTime(int timeValue)
    {
        Comment.text = "ETA " + timeValue + "sec";
    }
}
