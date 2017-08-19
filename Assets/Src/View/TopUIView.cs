using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUIView : BaseView {

    public GameObject MainUI;
    public GameObject SubUI;

    void Start()
    {
        //Show();
    }

    public override void Show()
    {
        Logger.Log("TopUIView.Show");
        base.Show();

        iTween.MoveTo(MainUI, iTween.Hash("y", 4.4f, "time", 1));
        iTween.MoveTo(SubUI, iTween.Hash("y", 4.0f, "time", 1));
        
    }
}
