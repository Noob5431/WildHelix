using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineAnimate))]
public class SplineJump : MonoBehaviour
{
    [SerializeField]
    SplineContainer[] splines;
    [SerializeField]
    float[] spline_speeds;
    [SerializeField]
    float distanceToTrigger;

    SplineAnimate animation;
    Transform player_transform;

    int currentSpline = 0;

    private void Start()
    {
        animation = gameObject.GetComponent<SplineAnimate>();
        player_transform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if((player_transform.position - transform.position).magnitude < distanceToTrigger 
            && Physics.Linecast(transform.position,player_transform.position) && currentSpline < splines.Length
            && !animation.IsPlaying)
        {
            animation.Duration = spline_speeds[currentSpline];
            animation.Container = splines[currentSpline];
            animation.Restart(true);
            currentSpline++;
        }
        /*animation.Container = splines[debug_choice];
        animation.Play();*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new UnityEngine.Vector4(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(this.transform.position, distanceToTrigger);
    }
}
