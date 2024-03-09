using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutUI : MonoBehaviour
{
    CanvasGroup canvasRenderer;
    [SerializeField] float fadeSpeed=4;
    // Start is called before the first frame update
    void Start()
    {
        canvasRenderer = GetComponent<CanvasGroup>();

        
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2);
        while (canvasRenderer.alpha > 0)
        {
            canvasRenderer.alpha = canvasRenderer.alpha - Time.deltaTime / fadeSpeed;
            //canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() - Time.deltaTime / fadeSpeed);
            print(canvasRenderer.alpha);
            yield return null;

        }
    }
}

