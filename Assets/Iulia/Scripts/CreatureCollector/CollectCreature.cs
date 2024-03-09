using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCreature : MonoBehaviour
{
    [SerializeField] TMP_Text collectText;
    [SerializeField] float collectRange = 5;

    private void FixedUpdate()
    {
        RaycastHit hit;
        

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, collectRange)
            && hit.transform.CompareTag("creature"))
        {
            collectText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                print("trieggerd");
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
