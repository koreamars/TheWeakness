using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private static GameObject container;
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "GameManager";
            instance = container.AddComponent(typeof(GameManager)) as GameManager;
        }
        return instance;
    }

    public void Test()
    {
        Logger.Log("Test");
    }
    
}
