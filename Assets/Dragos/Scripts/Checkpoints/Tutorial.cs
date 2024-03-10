using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TMPro.TMP_Text TutorialText;
    public string TutorialString = "";
    public float TimeToWait = 5f;

    private bool mShown = false;

    // Start is called before the first frame update
    void Start()
    {
        TutorialText.gameObject.SetActive(false);
    }

    private bool _isPlayer(GameObject objectToCheck)
    {
        if (objectToCheck == null) return false;
        if (objectToCheck.GetComponent<Movement>() != null)
            return true;

        return false;
    }

    IEnumerator showTutorial()
    {
        TutorialText.gameObject.SetActive(true);
        if (TutorialString != "")
        {
            TutorialText.text = TutorialString;
        }
        yield return new WaitForSeconds(TimeToWait);
        TutorialText.gameObject.SetActive(false);
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_isPlayer(other.gameObject))
            return;

        if (mShown == false)
        {
            mShown = true;
            StartCoroutine(showTutorial());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
