using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    private MainController _mainController;

    void Start()
    {

        _mainController = new MainController();
        
        GameObject loginViewObj = ViewFactory.GetInstance().getViewPrefab(ViewType.LoginField);
        LoginView loginView = loginViewObj.GetComponent<LoginView>();
        loginView.Show();

    }
}
