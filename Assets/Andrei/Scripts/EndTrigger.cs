using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    finisshmenu finishScript;
    public bool isFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        isFinished = true;
        gameObject.SetActive(false);
    }
    public void Finish()
    {
        finishScript.isFinished = true;
    }
}
