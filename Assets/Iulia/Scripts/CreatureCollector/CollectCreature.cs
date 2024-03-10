using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectCreature : MonoBehaviour
{
    [SerializeField] TMP_Text collectText;
    [SerializeField] float collectRange = 5;
    [SerializeField] float nextSceneDelay;
    bool hasPressed = false;

    private void FixedUpdate()
    {
        if(hasPressed)
        {
            nextSceneDelay -= Time.deltaTime;
        }
        if(nextSceneDelay<0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        RaycastHit hit;
        

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, collectRange)
            && hit.transform.CompareTag("creature"))
        {
            collectText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                print("triggered");
                hit.transform.GetComponent<Animator>().SetTrigger("Collect");
                hit.transform.GetComponentInChildren<ParticleSystem>().Play();
                hit.collider.GetComponent<Collectable>().collectCReature.Invoke();
                hit.collider.enabled = false;

                            
            }
        }
        else collectText.gameObject.SetActive(false);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    
    //    if (collision.transform.CompareTag("creature"))
    //    {
    //        print("trieggerd");
    //        collision.gameObject.GetComponent<Animator>().SetTrigger("Collect");
    //        collision.transform.GetComponentInChildren<ParticleSystem>().Play();
    //        collision.collider.enabled = false;
    //    }
    //}
}
