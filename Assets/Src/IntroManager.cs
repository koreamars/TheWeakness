using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

    public GameObject IntroImgae;

    private SpriteRenderer IntroImgaeRenderer;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(MainSceneLoad());
    }

    IEnumerator MainSceneLoad()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("Main Scene Change.");

        IntroImgaeRenderer = IntroImgae.GetComponent<SpriteRenderer>();

        //iTween.ColorTo(IntroImgae, iTween.Hash("from", Color.white, "to", Color.red, "time", 1, "OnComplete", "IntroImageMotionEnd"));
        Hashtable tweenParams = new Hashtable();
        tweenParams.Add("from", IntroImgaeRenderer.color);
        tweenParams.Add("to", Color.clear);
        tweenParams.Add("time", 1f);
        tweenParams.Add("onupdate", "OnColorUpdated");
        tweenParams.Add("OnComplete", "IntroImageMotionEnd");

        //iTween.ValueTo(IntroImgae, tweenParams);
        iTween.ValueTo(this.gameObject, tweenParams);
        //iTween.MoveBy(IntroImgae, iTween.Hash("x", 10, "time", 10f, "easeType", iTween.EaseType.linear));

        //OnColorUpdated(Color.clear);
        Debug.Log("iTween.ValueTo");
    }

    private void OnColorUpdated(Color color)
    {
        Debug.Log("IntroManager.OnColorUpdated.");
        IntroImgaeRenderer.color = color;
    }

    private void IntroImageMotionEnd()
    {
        Debug.Log("IntroManager.IntroImageMotionEnd");
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
