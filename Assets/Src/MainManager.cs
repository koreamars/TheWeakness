using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Logger.Log("MainManager.Start");
        
        GameManager.GetInstance().SceneUpdate(SceneType.LOBBY);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
