using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbility : MonoBehaviour
{
    [SerializeField] float overlapRadius = 5;


    void Update()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Fireplace"))
            {
                Transform firePlace = collider.transform;
                ParticleSystem fireParticles = firePlace.GetComponentInChildren<ParticleSystem>();

                if (!fireParticles.isPlaying && Input.GetKey(KeyCode.X))
                {
                    fireParticles.Play();

                    foreach(Transform platform in collider.transform.parent.transform)
                        if(platform.CompareTag("FirePlatform"))
                            MovePlatform(platform, fireParticles);
                }
            }
        }   
    }

    void MovePlatform(Transform platform, ParticleSystem fireParticles)
    {
        PlatformSettings platformSettings = platform.GetComponent<PlatformSettings>();

        transform.parent = platform; //make player child of platform so that the player moves with the platform

        Vector3 initPos = platform.transform.localPosition;
        LeanTween.moveLocal(platform.gameObject, platformSettings.GetMovePoint(), platformSettings.GetSpeed())
            .setDelay(platformSettings.GetDelay())
            .setOnComplete(delegate () {   //Stop the flame and move the platform back when platform reach its destination
                fireParticles.Stop();
                LeanTween.moveLocal(platform.gameObject, initPos, platformSettings.GetSpeed());
            });
       
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
