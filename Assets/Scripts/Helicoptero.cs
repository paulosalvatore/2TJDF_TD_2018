using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptero : MonoBehaviour
{
    public Waypoint waypointAtual;
    private FollowTarget followTarget;

    private void Start()
    {
        followTarget = GetComponent<FollowTarget>();
        followTarget.target = waypointAtual.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            waypointAtual = waypointAtual.waypointPosterior;
            followTarget.target = waypointAtual.transform;
        }
    }
}