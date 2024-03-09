using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController
{
    public float sensitivity { get; set; }
    public void Update(MovingPlatform movingPlatform) {
        movingPlatform.UpdateGrabDistance(Input.GetAxis("Mouse ScrollWheel") * sensitivity);
    }
}
