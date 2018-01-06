using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFactory : MonoBehaviour {

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

    public GameObject GetViewObj(GameObject parent, string prefabName, BaseViewModel baseModel)
    {
        Logger.Log("ViewFactory.GetViewObj " + prefabName);
        
        GameObject prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
        GameObject baseViewObj = MonoBehaviour.Instantiate(prefab) as GameObject;

        Logger.Log("baseViewObj " + baseViewObj);
        if (baseViewObj != null)
        {
            BaseView baseView = baseViewObj.GetComponent<BaseView>();
            baseView.isUnitTest = false;
            baseView.SetData(baseModel);

            RectTransform myRectTransform = baseViewObj.GetComponent<RectTransform>();
            myRectTransform.SetParent(parent.GetComponent<RectTransform>());
            myRectTransform.localScale = new Vector3(1, 1, 1);
            Logger.Log("myRectTransform.localScale " + myRectTransform.localScale);
        }

        return baseViewObj;
    }

    public GameObject GetOnlyObjType(GameObject parent, string prefabName)
    {
        Logger.Log("ViewFactory.GetOnlyObjType " + prefabName);

        GameObject prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
        GameObject baseViewObj = MonoBehaviour.Instantiate(prefab) as GameObject;

        Logger.Log("baseViewObj " + baseViewObj);
        if (baseViewObj != null)
        {
            RectTransform myRectTransform = baseViewObj.GetComponent<RectTransform>();
            myRectTransform.SetParent(parent.GetComponent<RectTransform>());
            myRectTransform.localScale = new Vector3(1, 1, 1);
            Logger.Log("myRectTransform.localScale " + myRectTransform.localScale);
        }

        return baseViewObj;
    }
}
