using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    private MainController _mainController;

    void Start()
    {

        _mainController = new MainController();

        GameObject canvas = GameObject.Find("Canvas") as GameObject;
        GameObject prefab = Resources.Load("Prefabs/LoginField") as GameObject;

        GameObject loginField = MonoBehaviour.Instantiate(prefab) as GameObject;
        loginField.name = "LoginField"; // name을 변경
        loginField.transform.parent = canvas.transform;
        loginField.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}
