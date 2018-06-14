using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    public GameObject IntroImgae;

    private Image IntroImgaeRenderer;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(MainSceneLoad());
    }

    IEnumerator MainSceneLoad()
    {
        yield return new WaitForSeconds(2);

        Logger.Log("Main Scene Change.");

        IntroImgaeRenderer = IntroImgae.GetComponent<Image>();
        
        Hashtable tweenParams = new Hashtable();
        tweenParams.Add("from", IntroImgaeRenderer.color);
        tweenParams.Add("to", Color.clear);
        tweenParams.Add("time", 1f);
        tweenParams.Add("onupdate", "OnColorUpdated");
        tweenParams.Add("OnComplete", "IntroImageMotionEnd");
        
        iTween.ValueTo(this.gameObject, tweenParams);

        //OnColorUpdated(Color.clear);
        Logger.Log("iTween.ValueTo");

        GameManager.GetInstance().Init();

    }

    private void OnColorUpdated(Color color)
    {
        Logger.Log("IntroManager.OnColorUpdated.");
        IntroImgaeRenderer.color = color;
    }

    private void IntroImageMotionEnd()
    {
        Logger.Log("IntroManager.IntroImageMotionEnd");
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
