using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCreature : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.transform.CompareTag("creature"))
        {
            print("trieggerd");
            collision.gameObject.GetComponent<Animator>().SetTrigger("Collect");
            collision.transform.GetComponentInChildren<ParticleSystem>().Play();
            collision.collider.enabled = false;
        }
    }
}
