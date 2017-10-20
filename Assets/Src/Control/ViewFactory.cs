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

    public GameObject getViewPrefab(string type, BaseModel baseModel = null)
    {
        Logger.Log("ViewFactory.getViewPrefab type:" + type);
        GameObject canvas = GameObject.Find("Canvas") as GameObject;
        GameObject prefab = Resources.Load("Prefabs/" + type) as GameObject;

        Logger.Log("ViewFactory.getViewPrefab prefab:" + prefab);
        GameObject fieldObject = MonoBehaviour.Instantiate(prefab) as GameObject;
        fieldObject.name = type; // name을 변경
        fieldObject.transform.parent = canvas.transform;
        fieldObject.transform.localScale = new Vector3(1f, 1f, 1f);

        LoginView loginView = fieldObject.GetComponent<LoginView>();
        if(baseModel != null) loginView.SetData(baseModel);
        loginView.Show();

        return fieldObject;
    }


}
