using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginView : BaseView {

    public GameObject FaceBookBtn;
    public GameObject GuestBtn;
    
    public override void SetData(BaseModel baseModel)
    {
        base.SetData(baseModel);
    }

    public override void Show()
    {
        base.Show();

        iTween.MoveTo(FaceBookBtn, iTween.Hash("x", 0, "time", 1));
        iTween.MoveTo(GuestBtn, iTween.Hash("x", 0, "time", 1));
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
        
        iTween.MoveTo(FaceBookBtn, iTween.Hash("x", -7, "time", 1, "oncomplete", "_OutMotionEnd", "oncompletetarget", this.gameObject));
        iTween.MoveTo(GuestBtn, iTween.Hash("x", 7, "time", 1));
    }

    private void _OutMotionEnd()
    {
        Logger.Log("LoginView._OutMotionEnd ");
        LoginViewModel loginViewModel = _baseModel as LoginViewModel;
        if(loginViewModel.LoginClickCall != null)
        {
            loginViewModel.LoginClickCall("test");
        }
    }
}
