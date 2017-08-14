using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginView : BaseView {

    public GameObject FaceBookBtn;
    public GameObject GuestBtn;

	// Use this for initialization
	void Start () {

        iTween.MoveTo(FaceBookBtn, iTween.Hash("x", 0, "time", 1));
        iTween.MoveTo(GuestBtn, iTween.Hash("x", 0, "time", 1));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void SetData(BaseModel baseModel)
    {
        base.SetData(baseModel);
    }

    public void OnFaceBookLogin()
    {
        Logger.Log("LoginView.OnFaceBookLogin");
        _OutMotion();
    }

    public void OnGuestLogin()
    {
        Logger.Log("LoginView.OnGuestLogin");
        _OutMotion();
    }

    private void _OutMotion()
    {
        Logger.Log("LoginView._OutMotion");
        
        iTween.MoveTo(FaceBookBtn, iTween.Hash("x", -7, "time", 1));
        iTween.MoveTo(GuestBtn, iTween.Hash("x", 7, "time", 1));
    }
}
