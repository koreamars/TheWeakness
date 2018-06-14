using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : BaseView {

    private string _currSceneType = "";
    private GameObject _lobbyMainBtnsObj = null;
    private GameObject _keyPadFieldObj = null;
    private GameObject _myWeaknessFieldObj = null;
    private GameObject _rivalWeaknessFieldObj = null;
    private GameObject _rivalAttackKeyFObj = null;
    private GameObject _myAttackKeyFObj = null;

    private LobbyMainBtns _lobbyMainBtns = null;
    private MyAttackKeyF _myAttackKeyF = null;
    private RivalAttackKeyF _rivalAttackKeyF = null;

    public void SceneUpdate(string sceneType)
    {
        Logger.Log("MainView.SceneUpdate sceneType:" + sceneType);

        //Destroy(_lobbyMainBtnsObj);
        if (_currSceneType != SceneType.READY_FOR_BATTLE) Destroy(_keyPadFieldObj);
        if (_currSceneType != SceneType.READY_FOR_BATTLE) Destroy(_myWeaknessFieldObj);
        Destroy(_rivalWeaknessFieldObj);
        Destroy(_myAttackKeyFObj);
        Destroy(_rivalAttackKeyFObj);
        _myAttackKeyF = null;
        _rivalAttackKeyF = null;
        
        switch (sceneType)
        {
            case SceneType.LOBBY:
                LobbyMainBtnsModel mainBtnModel = new LobbyMainBtnsModel();
                _lobbyMainBtnsObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.LOBBY_MAINBTNS, mainBtnModel);
                _lobbyMainBtns = _lobbyMainBtnsObj.GetComponent<LobbyMainBtns>();
                _lobbyMainBtns.Show();
                _lobbyMainBtns.Appear(AppearEnd);

                break;
            case SceneType.READY_FOR_BATTLE:
                _lobbyMainBtns.Disappear(_LobbyDisappearEnd);

                break;
            case SceneType.DURING_BATTLE:
                RiW​eakFieldModel rivalWeakFieldModel = new RiW​eakFieldModel();
                _rivalWeaknessFieldObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.RIVAL_WEAK_FIELD, rivalWeakFieldModel);
                RivalW​eakField rivalWeakField = _rivalWeaknessFieldObj.GetComponent<RivalW​eakField>();
                rivalWeakField.Show();
                rivalWeakField.Appear(_BattleAppearEnd);

                MyAttackKeyFModel myAttackKeyFModel = new MyAttackKeyFModel();
                _myAttackKeyFObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.MY_ATTACK_KEY_F, myAttackKeyFModel);
                _myAttackKeyF = _myAttackKeyFObj.GetComponent<MyAttackKeyF>();
                _myAttackKeyF.Show();

                RivalAttackKeyFModel rivalAttackKeyFModel = new RivalAttackKeyFModel();
                _rivalAttackKeyFObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.RIVAL_ATTACK_KEY_F, rivalAttackKeyFModel);
                _rivalAttackKeyF = _rivalAttackKeyFObj.GetComponent<RivalAttackKeyF>();
                _rivalAttackKeyF.Show();

                MyW​eakField myWeakField = _myWeaknessFieldObj.GetComponent<MyW​eakField>();
                myWeakField.UpdateWeakRoot(new Vector2(-90, -67));

                break;
        }

        _currSceneType = sceneType;
    }

    public override void Disappear(Action endCall)
    {
        Logger.Log("MainView.Disappear");
        _disappearCallback = endCall;

    }

    public void UpdateMyWeakValue()
    {
        Logger.Log("MainView.UpdateMyWeakValue");
        MyW​eakField myWeakField = _myWeaknessFieldObj.GetComponent<MyW​eakField>();
        myWeakField.UpdateValue();
    }

    public void ResetMyWeakValue()
    {
        Logger.Log("MainView.ResetMyWeakValue");
        MyW​eakField myWeakField = _myWeaknessFieldObj.GetComponent<MyW​eakField>();
        myWeakField.ResetValue();
    }

    public void UpdateAttackKey()
    {
        Logger.Log("MainView.UpdateAttackKey");
        _myAttackKeyF.UpdateValue();
    }

    public void RivalViewUpdate()
    {
        Logger.Log("MainView.RivalViewUpdate");

        RivalW​eakField rivalWeakField = _rivalWeaknessFieldObj.GetComponent<RivalW​eakField>();
        rivalWeakField.UpdateValue();

        _myAttackKeyF.UpdateValue();
    }

    public void UpdateRivalAttackKey(ArrayList arrayList)
    {
        Logger.Log("MainView.UpdateRivalAttackKey");
        _rivalAttackKeyF.UpdateAttackKey(arrayList);
    }

    private void AppearEnd()
    {
        Logger.Log("MainView.AppearEnd");
    }

    private void _LobbyDisappearEnd()
    {
        Logger.Log("MainView._LobbyDisappearEnd");

        Destroy(_lobbyMainBtnsObj);

        KeyPadFieldModel keyPadFieldModel = new KeyPadFieldModel();
        _keyPadFieldObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.KEY_PAD_FIELD, keyPadFieldModel);
        KeyPadField keyPadField = _keyPadFieldObj.GetComponent<KeyPadField>();
        keyPadField.Show();

        MyWkFieldModel myW​eakFieldModel = new MyWkFieldModel();
        _myWeaknessFieldObj = ViewFactory.GetInstance().GetViewObj(this.gameObject, ViewType.MY_WEAK_FIELD, myW​eakFieldModel);
        MyW​eakField myWeakField = _myWeaknessFieldObj.GetComponent<MyW​eakField>();
        myWeakField.Show();
        myWeakField.Appear(null);
    }
    
    private void _DisappearEnd()
    {
        Logger.Log("MainView._DisappearEnd");
    }

    private void _BattleAppearEnd()
    {
        Logger.Log("MainView._BattleAppearEnd");
        GameManager.GetInstance().BattleApearEnd();

    }
    
}
