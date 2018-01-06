using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour {

    private static NetController instance;
    private static GameObject container;
    public static NetController GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "NetController";
            instance = container.AddComponent(typeof(NetController)) as NetController;
        }
        return instance;
    }

    public void SendWeakKey()
    {

    }
}
