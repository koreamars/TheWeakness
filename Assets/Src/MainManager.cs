using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    private MainController _mainController;

    void Start()
    {

        _mainController = new MainController();
        _mainController.Init();
        _mainController.ShowLoginView();
        
    }
}
