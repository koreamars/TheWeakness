﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectView : BaseView {

    public GameObject CharacterImg;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject SelectBtn;

	// Use this for initialization
	void Start () {
        Show();
    }

    public override void Show()
    {
        Logger.Log("SelectView.Show");
        base.Show();

        iTween.MoveTo(CharacterImg, iTween.Hash("y", 0, "time", 1f));
        iTween.MoveTo(this.gameObject, iTween.Hash("y", 0, "time", 0.5f, "oncomplete", "_showArrow"));
    }

    private void _showArrow()
    {
        Logger.Log("SelectView._showArrow");

        iTween.MoveTo(LeftArrow, iTween.Hash("x", -2.7f, "time", 1));
        iTween.MoveTo(RightArrow, iTween.Hash("x", 2.7f, "time", 1));

        iTween.MoveTo(this.gameObject, iTween.Hash("y", 0, "time", 0.5f, "oncomplete", "_showSelectBtn"));
    }

    private void _showSelectBtn()
    {
        Logger.Log("SelectView._showSelectBtn");

        iTween.MoveTo(SelectBtn, iTween.Hash("y", -2.7f, "time", 1, "OnComplete", "_selectReady", "oncompletetarget", this.gameObject));
    }

    private void _selectReady()
    {

    }

    public void OnArrowBtn(string type)
    {
        Logger.Log("SelectView.OnArrowBtn");

    }

    public void OnSelectBtn()
    {
        Logger.Log("SelectView.OnSelectBtn");

    }
    
}