using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Logger.Log("MainManager.Start");

        LobbyMainBtnsModel mainBtnModel = new LobbyMainBtnsModel();
                
        ViewFactory.GetInstance().GetViewObj(ViewType.LOBBY_MAINBTNS, mainBtnModel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
