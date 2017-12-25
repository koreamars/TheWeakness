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

    public GameObject GetViewObj(string type, BaseViewModel baseModel)
    {
        Logger.Log("ViewFactory.GetViewObj " + baseModel);
        
        GameObject prefab = Resources.Load("Prefabs/" + type) as GameObject;
        GameObject baseViewObj = MonoBehaviour.Instantiate(prefab) as GameObject;
        GameObject canvas = GameObject.Find("Canvas") as GameObject;
        Logger.Log("baseViewObj " + baseViewObj);
        if (baseViewObj != null)
        {
            BaseView baseView = baseViewObj.GetComponent<BaseView>();
            baseView.isUnitTest = false;
            baseView.SetData(baseModel);

            RectTransform myRectTransform = baseViewObj.GetComponent<RectTransform>();
            myRectTransform.SetParent(canvas.GetComponent<RectTransform>());
            myRectTransform.localScale = new Vector3(1, 1, 1);
            Logger.Log("myRectTransform.localScale " + myRectTransform.localScale);
        }

        return baseViewObj;
    }
}
