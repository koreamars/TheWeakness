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
        _mainController.ShowLoginView(_characterSelect);

        //_showLobby();
    }

    private void _characterSelect()
    {
        _mainController.ShowChacraterSelectView(_showLobby);
    }

    private void _showLobby()
    {
        _mainController.ShowLobby();
    }
}
