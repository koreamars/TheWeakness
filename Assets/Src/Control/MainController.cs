using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour {

    private GameObject _canvasObj;
    private ViewFactory _viewFactory;

    private GameObject _loginView;
    private GameObject _selectView;
    private GameObject _characterViewObj;
    private CharacterView _characterView;
    private GameObject _topUIViewObj;
    private TopUIView _topUIView;
    private GameObject _lobbyViewObj;
    private LobbyView _lobbyView;

    private Action _loginComCall = null;
    private Action _selectComCall = null;
    private Action _showLobbyComCall = null;


    public void Init()
    {
        _canvasObj = GameObject.Find("Canvas") as GameObject;
        _viewFactory = ViewFactory.GetInstance();
    }
	
	public void ShowLoginView(Action endCall)
    {
        Logger.Log("MainController.ShowLoginView ");
        _loginComCall = endCall;
        LoginViewModel loginFieldModel = new LoginViewModel();
        loginFieldModel.LoginClickCall = _firstLoginCheck;
        _loginView = _viewFactory.GetView(ViewType.LoginField, loginFieldModel) as GameObject;
        SetParent(_loginView, "LoginView");

        LoginView loginView = _loginView.GetComponent<LoginView>();
        loginView.Show();

    }

    private void _firstLoginCheck(string type)
    {
        Logger.Log("MainController._firstLoginCheck " + type);

        Destroy(_loginView);

        //_showChacraterSelectView();
        if(_loginComCall != null)
        {
            _loginComCall();
        }
    }

    public void ShowChacraterSelectView(Action endCall)
    {
        Logger.Log("MainController.ShowChacraterSelectView ");
        _selectComCall = endCall;
        SelectViewModel selectViewModel = new SelectViewModel();
        selectViewModel.selectClickCall = _characterSave;
        _selectView = _viewFactory.GetView(ViewType.SelectField, selectViewModel) as GameObject;
        SetParent(_selectView, "SelectView");

        SelectView selectView = _selectView.GetComponent<SelectView>();
        selectView.Show();

        _showCharaterObj();

        _characterView.SetPosition(new Vector2(0, 0), true);

    }

    private void _showCharaterObj()
    {
        CharacterViewModel characterViewModel = new CharacterViewModel();
        _characterViewObj = _viewFactory.GetView(ViewType.CharacterField, characterViewModel) as GameObject;
        SetParent(_characterViewObj, "CharacterView");

        _characterView = _characterViewObj.GetComponent<CharacterView>();
        _characterView.Show();
        
    }

    private void _characterSave(string type)
    {
        Logger.Log("MainController._characterSave " + type);

        Destroy(_selectView);

        //_showLobby();
        if (_selectComCall != null)
        {
            _selectComCall();
        }
    }

    public void ShowLobby()
    {
        Logger.Log("MainController._showLobby ");

        if(_characterViewObj == null)
        {
            _showCharaterObj();
            //_characterView.SetPosition(new Vector2(-3, -4), false);
            _characterView.SetPosition(new Vector2(-2, -3), false);
        } else
        {
            _characterView.SetPosition(new Vector2(-2, -3), true);
        }

        TopUIViewModel topUIViewModel = new TopUIViewModel();
        _topUIViewObj = _viewFactory.GetView(ViewType.TopUIField, topUIViewModel) as GameObject;
        SetParent(_topUIViewObj, "TopUIView");

        _topUIView = _topUIViewObj.GetComponent<TopUIView>();
        _topUIView.Show();

        LobbyViewModel lobbyViewModel = new LobbyViewModel();
        _lobbyViewObj = _viewFactory.GetView(ViewType.LobbyField, lobbyViewModel) as GameObject;
        SetParent(_lobbyViewObj, "LobbyView");

        _lobbyView = _lobbyViewObj.GetComponent<LobbyView>();
        _lobbyView.Show();

    }
    
    private void SetParent(GameObject obj, string name)
    {
        obj.name = name;
        obj.transform.parent = _canvasObj.transform;
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    
}
