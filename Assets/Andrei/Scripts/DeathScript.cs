using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    EndTrigger endTrigger;

    private void Start()
    {
        endTrigger = GameObject.Find("EndSoul").GetComponent<EndTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (endTrigger.isFinished)
            endTrigger.Finish();
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
