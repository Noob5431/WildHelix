using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GlideController gc;
    public float shaking = 0.5f;


    private void LateUpdate()
    {
        float mod_shaking = shaking * gc.rotationPercentage;
        transform.localPosition = new Vector3(Random.Range(-mod_shaking, mod_shaking), Random.Range(-mod_shaking, mod_shaking), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
