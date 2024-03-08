using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireAbility : MonoBehaviour
{
    [SerializeField] float overlapRadius = 5;
    LayerMask maskFire;
    public bool isOnPlatform;
    public Collider platformUnderPlayer;
    
    Transform initialParent;
    [SerializeField] float abilityMaxDistance = 30;

    [SerializeField]GameObject pressText;

    private void Start()
    {
        maskFire = LayerMask.GetMask("Fireplace");
        isOnPlatform = false;
        initialParent = transform.parent;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);


        RaycastHit hit;

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, abilityMaxDistance, maskFire))
        {
            pressText.SetActive(true);
            LightFire(hit);
        }
        else pressText.SetActive(false);


        if (isOnPlatform)
            transform.parent = platformUnderPlayer.transform;
        else
            transform.parent = initialParent;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Camera.main.transform.forward * abilityMaxDistance);
    }

    void LightFire(RaycastHit firePlace)
    {
        ParticleSystem fireParticles = firePlace.transform.GetComponentInChildren<ParticleSystem>();

        if (!fireParticles.isPlaying && Input.GetKey(KeyCode.F))
        {
            fireParticles.Play();

            //Get all platforms moved by the fireplace
            //foreach (Transform platform in firePlace.transform.parent)
            //{
            //    print(firePlace.transform.parent.name);
            //    if (platform.gameObject.layer == LayerMask.NameToLayer("FirePlatform"))
            //        MovePlatform(platform, fireParticles);
            //}

            Transform platform = firePlace.transform.parent;

            if (platform.gameObject.layer == LayerMask.NameToLayer("FirePlatform"))
                MovePlatform(platform, fireParticles);
        }
        
    }

    void MovePlatform(Transform platform, ParticleSystem fireParticles)
    {
        PlatformSettings platformSettings = platform.GetComponent<PlatformSettings>();

        Vector3 initPos = platform.transform.localPosition;
        LeanTween.moveLocal(platform.gameObject, platformSettings.GetMovePoint(), platformSettings.GetSpeed())
            .setDelay(platformSettings.GetDelayStart())
            .setOnComplete(delegate () {
                //Stop the flame and move the platform back when platform reach its destination
                LeanTween.moveLocal(platform.gameObject, initPos, platformSettings.GetSpeed())
                .setDelay(platformSettings.GetDelayEnd())
                .setOnComplete(delegate() { fireParticles.Stop(); }) ;
                
            });
       
        
    }

}
