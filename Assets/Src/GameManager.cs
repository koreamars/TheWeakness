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

    private GameObject lobbyMainBtns = null;
    private GameObject keyPadField = null;

    public void Init()
    {
        Logger.Log("GameManager.Init");
        

    }

    public void SceneUpdate(string sceneType)
    {
        Logger.Log("GameManager.SceneUpdate sceneType:" + sceneType);

        Destroy(lobbyMainBtns);
        Destroy(keyPadField);

        switch (sceneType)
        {
            case SceneType.LOBBY:
                LobbyMainBtnsModel mainBtnModel = new LobbyMainBtnsModel();
                mainBtnModel.userMatchClickCall = UserMatchFunc;
                mainBtnModel.friendMatchClickCall = FriendMatchFunc;
                lobbyMainBtns = ViewFactory.GetInstance().GetViewObj(ViewType.LOBBY_MAINBTNS, mainBtnModel);

                break;
            case SceneType.READY_FOR_BATTLE:
                KeyPadFieldModel keyPadFieldModel = new KeyPadFieldModel();
                keyPadField = ViewFactory.GetInstance().GetViewObj(ViewType.KEY_PAD_FIELD, keyPadFieldModel);
                break;
        }

    }

    private void UserMatchFunc()
    {
        Logger.Log("GameManager.UserMatchFunc");
        SceneUpdate(SceneType.READY_FOR_BATTLE);
    }

    private void FriendMatchFunc()
    {
        Logger.Log("GameManager.FriendMatchFunc");
    }
         

}
