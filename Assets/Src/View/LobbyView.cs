using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyView : BaseView {

    public GameObject pveBtn;
    public GameObject pvpBtn;

    void Start()
    {
        //Show();
    }

    public override void Show()
    {
        base.Show();

        iTween.MoveTo(pveBtn, iTween.Hash("x", 0f, "time", 1));
        iTween.MoveTo(pvpBtn, iTween.Hash("x", 0f, "time", 1));
    }

    public void pveClick()
    {

    }

    public void pvpClick()
    {

    }
}
