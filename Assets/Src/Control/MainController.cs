using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    void Start () {
        
    }
	
	public void ShowLoginView()
    {
        GameObject canvas = GameObject.Find("Canvas") as GameObject;
        GameObject prefab = Resources.Load("Prefabs/LoginField") as GameObject;

        GameObject loginField = MonoBehaviour.Instantiate(prefab) as GameObject;
        loginField.name = "LoginField"; // name을 변경
        loginField.transform.parent = canvas.transform;
        loginField.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
