using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    private GameObject _canvasObj;
    private ViewFactory _viewFactory;

    private GameObject _loginView;
    private GameObject _selectView;
    
    public void Init()
    {
        _canvasObj = GameObject.Find("Canvas") as GameObject;
        _viewFactory = ViewFactory.GetInstance();
    }
	
	public void ShowLoginView()
    {
        Logger.Log("ShowLoginView ");
        LoginFieldModel loginFieldModel = new LoginFieldModel();
        loginFieldModel.LoginClickCall = _firstLoginCheck;
        _loginView = _viewFactory.GetView(ViewType.LoginField, loginFieldModel) as GameObject;
        _loginView.name = "LoginField"; // name을 변경
        _loginView.transform.parent = _canvasObj.transform;
        _loginView.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    private void _firstLoginCheck(string type)
    {
        Logger.Log("_firstLoginCheck " + type);

        Destroy(_loginView);
        
    }
    
    
}
