using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : BaseView {

    public GameObject CharacterImg;

    void Start()
    {
        
    }

    public override void Show()
    {
        Logger.Log("SelectView.Show");
        base.Show();
        
    }

    public void SetPosition(Vector2 newPos, bool isMove)
    {
        
        if(isMove == true)
        {
            iTween.MoveTo(CharacterImg, iTween.Hash("x", newPos.x, "y", newPos.y, "time", 1f));
        } else
        {
            iTween.MoveTo(CharacterImg, iTween.Hash("x", newPos.x, "y", newPos.y, "time", 0f));
        }
    }
}
