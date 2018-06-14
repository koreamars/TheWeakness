using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetController : MonoBehaviour {

    private static NetController instance;
    private static GameObject container;
    public static NetController GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "NetController";
            DontDestroyOnLoad(container);
            instance = container.AddComponent(typeof(NetController)) as NetController;
        }
        return instance;
    }

    private GameObject _connectingVIewObj = null;

    public Action GoInRoomComplete = null;
    public Action MyKeySaveComplete = null;
    public Action<ArrayList, ArrayList> KeyConfirmComplete = null;
    public Action<ArrayList> RivalAttackPush = null;
    public Action<bool, ArrayList> GameEndPush = null;

    private ArrayList _fakeEnemyKeys = null;
    private int _enemyCkKey1 = 0;
    private int _enemyCkKey2 = 0;
    private int _enemyCkKey3 = 0;
    private int _enemyLastCount = 0;
    private int _enemyEndCount = 0;
    private int _enemyEndMax = 0;

    public void GoInRoom()
    {
        Logger.Log("NetController.GoInRoom GoInRoomComplete:" + GoInRoomComplete);

        _ShowConnectView();
        Invoke("_InComeRoom", 1);
    }

    public void SaveMyWeakKeys(ArrayList myKeyList)
    {
        Logger.Log("NetController.SendMyWeakKeys myKeyList:" + myKeyList);
        _ShowConnectView();
        Invoke("_SaveKey", 1);
    }

    public void SendAttackKey(int key)
    {
        Logger.Log("NetController.SendWeakKeys");

    }

    public void SendWeakConfirm()
    {
        Logger.Log("NetController.SendWeakConfirm");
        _ShowConnectView();
        Invoke("_KeyConfirmComplete", 0.1f);
    }

    private void _InComeRoom()
    {
        Logger.Log("NetController._InComeRoom GoInRoomComplete:" + GoInRoomComplete);

        // TODO 임시 상대 키 설정.
        _fakeEnemyKeys = new ArrayList();
        _fakeEnemyKeys.Add(UnityEngine.Random.Range(1, 12));
        _fakeEnemyKeys.Add(UnityEngine.Random.Range(1, 12));
        _fakeEnemyKeys.Add(UnityEngine.Random.Range(1, 12));

        _enemyEndMax = UnityEngine.Random.Range(0, 6);
        _enemyEndCount = 0;
        _enemyLastCount = 0;
        _enemyCkKey1 = 0;
        _enemyCkKey2 = 0;
        _enemyCkKey3 = 0;

        _HideConnectView();
        if (GoInRoomComplete != null) GoInRoomComplete();
    }

    private void _SaveKey()
    {
        Logger.Log("NetController._SaveKey MyKeySaveComplete:" + MyKeySaveComplete);
        _HideConnectView();
        if (MyKeySaveComplete != null) MyKeySaveComplete();

        // TODO 임시 적 인터발 공격 시작.
        Invoke("_EnemyInterval", 5.5f);
    }

    private void _KeyConfirmComplete()
    {
        Logger.Log("NetController._KeyConfirmComplete");
        _HideConnectView();

        Logger.Log("result : " + _fakeEnemyKeys[0] + "/" + _fakeEnemyKeys[1] + "/" + _fakeEnemyKeys[2]);

        //TODO 페이스 데이터 설정.
        ArrayList resultList = new ArrayList();
        ArrayList answerList = new ArrayList();

        ArrayList userList = GameData.currBattleRoomModel.currAttackKeys;
        int idx = 0;
       foreach (int userKey in userList)
        {
            bool isNotMatching = false;

            int chValue = Convert.ToInt32(_fakeEnemyKeys[idx]);
            if (userKey.ToString() == chValue.ToString())
            {
                isNotMatching = true;
                resultList.Add(KeyConfirmType.ALL_MATCHING);
                answerList.Add(chValue);
                Logger.Log("KeyConfirmType.ALL_MATCHING " + userKey);
                idx++;
                continue;
            }

            foreach (int enemyKey in _fakeEnemyKeys)
            {
                if(userKey.ToString() == enemyKey.ToString())
                {
                    isNotMatching = true;
                    resultList.Add(KeyConfirmType.POS_MATCHING);
                    answerList.Add(0);
                    Logger.Log("KeyConfirmType.POS_MATCHING " + userKey);
                    break;
                }
            }

            if (!isNotMatching)
            {
                resultList.Add(KeyConfirmType.NOT_MATCHING);
                answerList.Add(0);
                Logger.Log("KeyConfirmType.NOT_MATCHING " + userKey);
            }

            idx++;
        }

        if (KeyConfirmComplete != null) KeyConfirmComplete(resultList, answerList);

        // game end 체크
        if(resultList[0].ToString() == KeyConfirmType.ALL_MATCHING && resultList[1].ToString() == KeyConfirmType.ALL_MATCHING && resultList[2].ToString() == KeyConfirmType.ALL_MATCHING)
        {
            _IsGameEnd(true);
        }
    }

    private void _ShowConnectView()
    {
        Logger.Log("NetController._ShowConnectView");
        if (_connectingVIewObj == null)
        {
            GameObject canvas = GameObject.Find("Canvas") as GameObject;
            _connectingVIewObj = ViewFactory.GetInstance().GetOnlyObjType(canvas, ViewType.NET_CONNECT);
        }
    }

    private void _HideConnectView()
    {
        Logger.Log("NetController._HideConnectView");
        if (_connectingVIewObj != null)
        {
            Destroy(_connectingVIewObj);
            _connectingVIewObj = null;
        }
    }

    private void _IsGameEnd(bool isWin)
    {
        Logger.Log("NetController._IsGameEnd isWin:" + isWin);
        CancelInvoke();
        if (GameEndPush != null) GameEndPush(isWin, _fakeEnemyKeys);
    }

    private void _EnemyInterval()
    {
        Logger.Log("NetController._EnemyInterval");
        float intervalTime = UnityEngine.Random.Range(1f, 3f);
        Invoke("_EnemyIntervalEnd", intervalTime);
    }

    private void _EnemyIntervalEnd()
    {
        Logger.Log("NetController._EnemyIntervalEnd");

        ArrayList enemyAttackKeys = new ArrayList();
        if(_enemyCkKey1 > 0)
        {
            enemyAttackKeys.Add(_enemyCkKey1);
        } else
        {
            _GetEnemyKey(enemyAttackKeys, 0);
        }
        if (_enemyCkKey2 > 0)
        {
            enemyAttackKeys.Add(_enemyCkKey2);
        }
        else
        {
            _GetEnemyKey(enemyAttackKeys, 1);
        }
        if (_enemyCkKey3 > 0)
        {
            enemyAttackKeys.Add(_enemyCkKey3);
        }
        else
        {
            _GetEnemyKey(enemyAttackKeys, 2);
        }

        _enemyLastCount = 0;

        int enemyKey1 = Convert.ToInt32(enemyAttackKeys[0]);
        int enemyKey2 = Convert.ToInt32(enemyAttackKeys[1]);
        int enemyKey3 = Convert.ToInt32(enemyAttackKeys[2]);

        int myKey1 = Convert.ToInt32(GameData.currBattleRoomModel.currMyWeakList[0]);
        int myKey2 = Convert.ToInt32(GameData.currBattleRoomModel.currMyWeakList[1]);
        int myKey3 = Convert.ToInt32(GameData.currBattleRoomModel.currMyWeakList[2]);

        // TODO 적군 판정 체크
        bool isEnemyWin = false;
        bool isMatch1Key = false;
        bool isMatch2Key = false;
        bool isMatch3Key = false;
        if (enemyKey1.ToString() == myKey1.ToString())
        {
            isMatch1Key = true;
            _enemyCkKey1 = enemyKey1;
            _enemyLastCount++;
        }
        if (enemyKey2.ToString() == myKey2.ToString())
        {
            isMatch2Key = true;
            _enemyCkKey2 = enemyKey2;
            _enemyLastCount++;
        }
        if (enemyKey3.ToString() == myKey3.ToString())
        {
            isMatch3Key = true;
            _enemyCkKey3 = enemyKey3;
            _enemyLastCount++;
        }
        if(isMatch1Key == true && isMatch2Key == true && isMatch3Key == true)
        {
            isEnemyWin = true;
        }

        if (RivalAttackPush != null) RivalAttackPush(enemyAttackKeys);
        
        if(isEnemyWin == true)
        {
            _IsGameEnd(false);
        } else
        {
            _EnemyInterval();
        }
        
    }

    private void _GetEnemyKey(ArrayList enemyAttackKeys, int slotNum)
    {
        int enemyKey = UnityEngine.Random.Range(1, 12);

        if(_enemyLastCount >= 2)
        {
            if(_enemyEndCount >= _enemyEndMax)
            {
                enemyKey = Convert.ToInt32(GameData.currBattleRoomModel.currMyWeakList[slotNum]);
            } else
            {
                _enemyEndCount++;
            }
        }
        
        enemyAttackKeys.Add(enemyKey);

    }
    


}
