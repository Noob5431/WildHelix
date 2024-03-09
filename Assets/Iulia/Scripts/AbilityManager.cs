using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private bool isMagneticCollected, isFireCollected, isGlideCollected, isAllCollected;
    public KeyCode magneticKey= KeyCode.Mouse0, fireKey=KeyCode.F, glideKey=KeyCode.Space;
    [SerializeField] GameObject player;
    [SerializeField] GameObject showcase;


    public void CollectMagnetic()
    {
        isMagneticCollected = true;

        PlatformDetection comp = player.GetComponent<PlatformDetection>();
        if (comp != null)
        {
            comp.keyToUse = magneticKey;
            print("magnetic collected");

        }

        showcase.GetComponent<CombineParts>().EnableParts(1); 
    }

    public void CollectGlide()
    {
        isMagneticCollected = true;

        GlideAbility comp = player.GetComponent<GlideAbility>();
        if (comp != null)
            comp.keyToUse = glideKey;

        showcase.GetComponent<CombineParts>().EnableParts(2);
    }

    public void CollectFire()
    {
        isFireCollected = true;
        FireAbility comp = player.GetComponent<FireAbility>();
        if (comp != null)
            comp.keyToUse = fireKey;

        showcase.GetComponent<CombineParts>().EnableParts(3);
    }

    public void CollectAll()
    {
        print("you collected all");
        showcase.GetComponent<CombineParts>().EnableParts(4);
    }

   
}
