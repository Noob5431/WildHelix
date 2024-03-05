using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lvlselection : MonoBehaviour
{
    public void BBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void leve(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
