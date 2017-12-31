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

    private string _prevSceneType = "";
    private GameObject _lobbyMainBtns = null;
    private GameObject _keyPadField = null;
    private GameObject _myWeaknessField = null;
    private GameObject _rivalWeaknessField = null;

    public void Init()
    {
        Logger.Log("GameManager.Init");
        

    }

    public void SceneUpdate(string sceneType)
    {
        Logger.Log("GameManager.SceneUpdate sceneType:" + sceneType);

        Destroy(_lobbyMainBtns);
        if (_prevSceneType != SceneType.READY_FOR_BATTLE) Destroy(_keyPadField);
        if (_prevSceneType != SceneType.READY_FOR_BATTLE) Destroy(_myWeaknessField);

        switch (sceneType)
        {
            case SceneType.LOBBY:
                LobbyMainBtnsModel mainBtnModel = new LobbyMainBtnsModel();
                mainBtnModel.userMatchClickCall = _UserMatchFunc;
                mainBtnModel.friendMatchClickCall = _FriendMatchFunc;
                _lobbyMainBtns = ViewFactory.GetInstance().GetViewObj(ViewType.LOBBY_MAINBTNS, mainBtnModel);

                break;
            case SceneType.READY_FOR_BATTLE:
                KeyPadFieldModel keyPadFieldModel = new KeyPadFieldModel();
                keyPadFieldModel.sendBtnCall = _SendMyWeakFunc;
                _keyPadField = ViewFactory.GetInstance().GetViewObj(ViewType.KEY_PAD_FIELD, keyPadFieldModel);

                MyW​eakFieldModel myW​eakFieldModel = new MyW​eakFieldModel();
                _myWeaknessField = ViewFactory.GetInstance().GetViewObj(ViewType.MY_WEAK_FIELD, myW​eakFieldModel);
                break;
            case SceneType.DURING_BATTLE:
                RiW​eakFieldModel rivalWeakFieldModel = new RiW​eakFieldModel();
                _rivalWeaknessField = ViewFactory.GetInstance().GetViewObj(ViewType.RIVAL_WEAK_FIELD, rivalWeakFieldModel);

                break;
        }

        _prevSceneType = sceneType;

    }

    private void _UserMatchFunc()
    {
        Logger.Log("GameManager._UserMatchFunc");
        SceneUpdate(SceneType.READY_FOR_BATTLE);
    }

    private void _FriendMatchFunc()
    {
        Logger.Log("GameManager._FriendMatchFunc");
    }

    private void _SendMyWeakFunc()
    {
        Logger.Log("GameManager._SendMyWeakFunc");
        SceneUpdate(SceneType.DURING_BATTLE);
    }
         

}
