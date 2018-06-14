using System.Collections;
using System;

public class BattleRoomModel : BaseDataModel
{
    public string currBattleType = "";          // 현재 전투 타입.
    public ArrayList currMyWeakList = null;     // 내 WeakKey 정보.
    public ArrayList currAttackKeys = null;     // 내 공격키 모음.
    public ArrayList currAttackedKeys = null;   // 상대 공격키 모음.
    public ArrayList currRivalWeakStates = null;// 상대 약점 상태 정보.
    public ArrayList currRivalAnswerList = null;//  상대 약점 정보.

}