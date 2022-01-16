using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoFade : MonoBehaviour {

    public Material tutMat, lobMat;
    public float totalTime = 3f;

    private Color tutCol, lobCol;

	// Use this for initialization
	void Start () {

        tutCol = tutMat.color;
        lobCol = lobMat.color;
        StartCoroutine(FadeIn());

    }
	
	// Update is called once per frame
	void Update () {
        

    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            lobMat.color = Color.Lerp(tutCol, lobCol, (elapsedTime / totalTime));
            yield return null;

        }

        yield return new WaitForSeconds(1);
        Destroy(this);
    }
}
