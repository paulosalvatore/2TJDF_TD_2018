using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public string targetTag;

    public float velocidadeMovimento = 1f;
    public float velocidadeRotacao = 1f;
    public bool lookAt = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (target == null && targetTag != "")
        {
            ProcurarTarget();
        }

        if (target == null)
        {
            transform.Translate(
                Vector3.forward * velocidadeMovimento * Time.deltaTime
            );

            return;
        }

        if (lookAt)
        {
            // Rotacionar

            // Movimentar
        }
        else
        {
            // Movimentar

            transform.Translate(
                (target.transform.position - transform.position).normalized *
                velocidadeMovimento *
                Time.deltaTime
            );
        }
    }

    private void ProcurarTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            Transform possivelTarget = null;

            foreach (GameObject checarTarget in targets)
            {
                float checarDistancia = Vector3.Distance(checarTarget.transform.position, transform.position);

                if (possivelTarget == null ||
                    checarDistancia < Vector3.Distance(possivelTarget.transform.position, transform.position))
                {
                    possivelTarget = checarTarget.transform;
                }
            }

            if (possivelTarget != null)
            {
                target = possivelTarget;
            }
        }
    }
}