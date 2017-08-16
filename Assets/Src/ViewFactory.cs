using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFactory : MonoBehaviour
{

    private static ViewFactory instance;
    private static GameObject container;
    public static ViewFactory GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "ViewFactory";
            instance = container.AddComponent(typeof(ViewFactory)) as ViewFactory;
        }
        return instance;
    }

    public GameObject GetView(string type, BaseModel baseModel)
    {
        Logger.Log("ViewFactory.GetView " + type);
        GameObject prefab = Resources.Load("Prefabs/" + type) as GameObject;
        GameObject returnViewObj = MonoBehaviour.Instantiate(prefab) as GameObject;
        BaseView baseView = returnViewObj.GetComponent<BaseView>();
        baseView.SetData(baseModel);
        return returnViewObj;
    }
}
