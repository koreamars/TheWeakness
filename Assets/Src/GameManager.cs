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
            DontDestroyOnLoad(container);
            instance = container.AddComponent(typeof(GameManager)) as GameManager;
        }
        return instance;
    }

    private GameObject _canvas = null;
    private GameObject _mainViewObj = null;
    private MainView _mainView = null;
    private string _currSceneType = "";
    private string _currBattleType = "";

    private int _battleReadyTime = 0;
    
    public void Init()
    {
        Logger.Log("GameManager.Init");

        NetController.GetInstance().GoInRoomComplete = _GoInRoomComplete;
        NetController.GetInstance().MyKeySaveComplete = _SaveMyKeyComplete;
        NetController.GetInstance().KeyConfirmComplete = _KeyConfirmComplete;
        NetController.GetInstance().GameEndPush = _GameEndPush;
        NetController.GetInstance().RivalAttackPush = _RIvalAttackKeysPush;
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

        _currBattleType = BattleType.USER_TYPE;

        _SetRoomData();

        NetController.GetInstance().GoInRoom();
    }

    public void FriendMatchFunc()
    {
        Logger.Log("GameManager.FriendMatchFunc");

        _currBattleType = BattleType.FRIEND_TYPE;

        _SetRoomData();

        NetController.GetInstance().GoInRoom();
    }

    public void SaveMyWeakKey(int keyValue)
    {
        Logger.Log("GameManager.SaveMyWeakKey keyValue:" + keyValue);

        if(_currSceneType == SceneType.DURING_BATTLE)
        {   // 공격할 키를 설정한다.
            if (GameData.currBattleRoomModel.currAttackKeys.Count < 3)
            {
                GameData.currBattleRoomModel.currAttackKeys.Add(keyValue);
            }

            _mainView.UpdateAttackKey();
            NetController.GetInstance().SendAttackKey(keyValue);
            return;
        }

        // 내 약점키를 설정한다.
        if(GameData.currBattleRoomModel.currMyWeakList.Count < 3)
        {
            GameData.currBattleRoomModel.currMyWeakList.Add(keyValue);
        }

        _mainView.UpdateMyWeakValue();
    }

    public void CancelWeakKey()
    {
        Logger.Log("GameManager.CancelWeakKey");
        if (_currSceneType == SceneType.READY_FOR_BATTLE)
        {
            GameData.currBattleRoomModel.currMyWeakList.Clear();
            _mainView.ResetMyWeakValue();
        }
        else if (_currSceneType == SceneType.DURING_BATTLE)
        {
            GameData.currBattleRoomModel.currAttackKeys.Clear();
            _mainView.UpdateAttackKey();
        }
        else
        {

        }
    }

    public void SendMyWeak()
    {
        Logger.Log("GameManager.SendMyWeak");

        if (_currSceneType == SceneType.READY_FOR_BATTLE)
        {
            if(GameData.currBattleRoomModel.currMyWeakList.Count < 3)
            {
                AlertPopupModel alertPopupModel = new AlertPopupModel();
                alertPopupModel.title = "alert";
                alertPopupModel.comment = "you are fire!!!";
                PopupController.GetInstance().ShowPopup(PopupType.AlertPopup, alertPopupModel);
                return;
            }

            NetController.GetInstance().SaveMyWeakKeys(GameData.currBattleRoomModel.currMyWeakList);

        }
        else if (_currSceneType == SceneType.DURING_BATTLE)
        {
            if (GameData.currBattleRoomModel.currAttackKeys.Count < 3)
            {
                return;
            }
            else
            {
                NetController.GetInstance().SendWeakConfirm();
            }
        }
        else
        {
            
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

    private void _SetRoomData()
    {
        Logger.Log("GameManager._SetRoomData");
        
        GameData.currBattleRoomModel = new BattleRoomModel();
        GameData.currBattleRoomModel.currBattleType = _currBattleType;
        GameData.currBattleRoomModel.currMyWeakList = new ArrayList();
        GameData.currBattleRoomModel.currAttackKeys = new ArrayList();
        GameData.currBattleRoomModel.currAttackedKeys = new ArrayList();

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

    /**
     * 서버로 부터 방입장 완료를 확인함.
     * */
    private void _GoInRoomComplete()
    {
        Logger.Log("GameManager._GoInRoomComplete");
        SceneUpdate(SceneType.READY_FOR_BATTLE);

    }

    /**
     * 서버에 내 키를 저장을 완료함.
     * */
    private void _SaveMyKeyComplete()
    {
        Logger.Log("GameManager._SaveMyKeyComplete");
        SceneUpdate(SceneType.DURING_BATTLE);

    }

    /**
     * 서버에 내 키를 검증함.
     * */
    private void _KeyConfirmComplete(ArrayList resultList, ArrayList answerList)
    {
        Logger.Log("GameManager._KeyConfirmComplete");

        GameData.currBattleRoomModel.currRivalWeakStates = resultList;
        GameData.currBattleRoomModel.currRivalAnswerList = answerList;

        GameData.currBattleRoomModel.currAttackKeys.Clear();

        _mainView.RivalViewUpdate();
    }

    /**
     * 상대의 공격 시도함.
     * */
    private void _RIvalAttackKeysPush(ArrayList enemyKeys)
    {
        Logger.Log("GameManager._RIvalAttackKeysPush");
        GameData.currBattleRoomModel.currAttackedKeys = enemyKeys;
        Logger.Log("enemyKeys " + enemyKeys[0] + "/" + enemyKeys[1] + "/" + enemyKeys[2]);

        _mainView.UpdateMyWeakValue();
        _mainView.UpdateRivalAttackKey(enemyKeys);
    }

    /** 
     * 서버에서 게임 완료 통신 옴
     * */
    private void _GameEndPush(bool isWin, ArrayList enemyKeys)
    {
        Logger.Log("GameManager._GameEndPush");
        ResultPopupModel resultPopupModel = new ResultPopupModel();
        resultPopupModel.isWin = isWin;
        resultPopupModel.enemyKeyList = enemyKeys;
        PopupController.GetInstance().ShowPopup(PopupType.ResultPopup, resultPopupModel);

        GameData.currBattleRoomModel.currMyWeakList.Clear();
        if(GameData.currBattleRoomModel.currRivalWeakStates != null) GameData.currBattleRoomModel.currRivalWeakStates.Clear();
        if(GameData.currBattleRoomModel.currRivalAnswerList != null) GameData.currBattleRoomModel.currRivalAnswerList.Clear();

        if(GameData.currBattleRoomModel.currAttackKeys != null) GameData.currBattleRoomModel.currAttackKeys.Clear();
        if(GameData.currBattleRoomModel.currAttackedKeys != null) GameData.currBattleRoomModel.currAttackedKeys.Clear();

    }
         
         

}
