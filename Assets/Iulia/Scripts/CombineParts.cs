using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineParts : MonoBehaviour
{
    [SerializeField] GameObject magneticParts, glideParts, fireParts, allParts;
    [SerializeField] bool magneticPartsCollected, glidePartsCollected, firePartsCollected, allPartsCollected;
    // Start is called before the first frame update
    

    void EnableParts()
    {
        if (magneticPartsCollected)
            magneticParts.SetActive(true);
        if (glidePartsCollected)
            glideParts.SetActive(true);
        if (firePartsCollected)
            fireParts.SetActive(true);
        if (allPartsCollected)
            allParts.SetActive(true);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        EnableParts();
    }
   
}
