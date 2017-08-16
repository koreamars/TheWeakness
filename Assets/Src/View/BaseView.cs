using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour {

    protected BaseModel _baseModel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void SetData(BaseModel baseModel)
    {
        _baseModel = baseModel;
    }

    public virtual void Show()
    {

    }

    public virtual void Hide()
    {

    }
}
