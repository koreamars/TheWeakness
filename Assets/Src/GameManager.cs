using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private static GameObject container;
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "GameManager";
            instance = container.AddComponent(typeof(GameManager)) as GameManager;
        }
        return instance;
    }

    private GameObject _canvas = null;
    private GameObject _mainViewObj = null;
    private MainView _mainView = null;
    private string _currSceneType = "";

    private int _battleReadyTime = 0;
         

    private ArrayList _currWeakList = new ArrayList();
    
    public void Init()
    {
        Logger.Log("GameManager.Init");
        

    }

    public void SceneUpdate(string sceneType)
    {
        Logger.Log("GameManager.SceneUpdate sceneType:" + sceneType);

        if(_canvas == null) _canvas = GameObject.Find("Canvas") as GameObject;
        if (_mainViewObj == null) {
            MainViewModel mainViewModel = new MainViewModel();
            _mainViewObj = ViewFactory.GetInstance().GetViewObj(_canvas, ViewType.MAIN_VIEW, mainViewModel);
            _mainView = _mainViewObj.GetComponent<MainView>();
            _mainView.Show();
        }

        _mainView.SceneUpdate(sceneType);

        _currSceneType = sceneType;

    }

    public void UserMatchFunc()
    {
        Logger.Log("GameManager.UserMatchFunc");
        SceneUpdate(SceneType.READY_FOR_BATTLE);
    }

    public void FriendMatchFunc()
    {
        Logger.Log("GameManager.FriendMatchFunc");
    }

    public void SaveMyWeakKey(int keyValue)
    {
        Logger.Log("GameManager.SaveMyWeakKey keyValue:" + keyValue);

        if(_currWeakList.Count < 3)
        {
            _currWeakList.Add(keyValue);
        }

        _mainView.UpdateMyWeakValue(_currWeakList);
    }

    public void CancelWeakKey()
    {
        Logger.Log("GameManager.CancelWeakKey");
        _currWeakList.Clear();
        _mainView.ResetMyWeakValue();
    }

    public void SendMyWeak()
    {
        Logger.Log("GameManager.SendMyWeak");

        if (_currSceneType == SceneType.READY_FOR_BATTLE)
        {
            if(_currWeakList.Count < 3)
            {
                AlertPopupModel alertPopupModel = new AlertPopupModel();
                alertPopupModel.title = "alert";
                alertPopupModel.comment = "you are fire!!!";
                PopupController.GetInstance().ShowPopup(PopupType.AlertPopup, alertPopupModel);
                return;
            }
            SceneUpdate(SceneType.DURING_BATTLE);
        }
        else
        {
            ResultPopupModel resultPopupModel = new ResultPopupModel();
            PopupController.GetInstance().ShowPopup(PopupType.ResultPopup, resultPopupModel);

            _currWeakList.Clear();
        }
    }

    public void BattleApearEnd()
    {
        Logger.Log("GameManager.SendMyWeak");

        BattleReadyPopupModel battleReadyPopupModel = new BattleReadyPopupModel();
        PopupController.GetInstance().ShowPopup(PopupType.BattleReadyPopup, battleReadyPopupModel);

        _battleReadyTime = 5;
        InvokeRepeating("_BattleReadyTIme", 0f, 1f);
        
    }

    private void _BattleReadyTIme()
    {
        Logger.Log("GameManager._BattleReadyTIme");

        if(_battleReadyTime < 0)
        {
            CancelInvoke();
            PopupController.GetInstance().HidePopup();
        } else
        {
            GameObject popupObj = PopupController.GetInstance().CurrentPopupObj;
            BattleReadyPopup battleReadyPopup = popupObj.GetComponent<BattleReadyPopup>();
            battleReadyPopup.UpdateTime(_battleReadyTime);
            _battleReadyTime--;
        }
        


    }
         

}
