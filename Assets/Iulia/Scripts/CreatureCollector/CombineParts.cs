using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineParts : MonoBehaviour
{
    [SerializeField] GameObject magneticParts, glideParts, fireParts, allParts;
    [SerializeField] bool magneticPartsCollected, glidePartsCollected, firePartsCollected, allPartsCollected;
    // Start is called before the first frame update
    

    public void EnableParts(int partType)
    {
        if (partType==1)
            magneticParts.SetActive(true);
        if (partType == 2)
            glideParts.SetActive(true);
        if (partType == 3)
            fireParts.SetActive(true);
        if (partType == 4)
            allParts.SetActive(true);
    }


    
}
