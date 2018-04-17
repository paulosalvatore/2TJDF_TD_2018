using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Waypoints")]
    [Tooltip("Lista com todos os Waypoints")]
    public Waypoint[] waypoints;
    private int indexAtual = -1;
    private Waypoint waypointAnterior;
    private Waypoint waypointPosterior;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        AtualizarWaypoints();

        AtualizarWaypointAtual();

        LinkarWaypoints();

        if (waypointPosterior != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                transform.position,
                waypointPosterior.transform.position
            );
        }
    }

    private void LinkarWaypoints()
    {
        int indexAnterior = indexAtual - 1;
        int indexPosterior = indexAtual + 1;

        DefinirWaypoint(ref waypointAnterior, indexAnterior);
        DefinirWaypoint(ref waypointPosterior, indexPosterior);
    }

    private void DefinirWaypoint(ref Waypoint waypoint, int index)
    {
        if (index < 0)
            index = waypoints.Length - 1;
        else if (index == waypoints.Length)
            index = 0;

        waypoint = waypoints[index];
    }

    #region ATUALIZAÇÃO_WAYPOINTS

    private void AtualizarWaypointAtual()
    {
        indexAtual = PegarIdWaypoint(gameObject.name);
    }

    private void AtualizarWaypoints()
    {
        waypoints = FindObjectsOfType<Waypoint>();
        waypoints = waypoints.OrderBy(objeto => PegarIdWaypoint(objeto.name)).ToArray();

        List<string> waypointsNomes = new List<string>();
        foreach (Waypoint waypoint in waypoints)
        {
            waypointsNomes.Add(waypoint.name);
        }

        var waypointsNomesAgrupados = waypointsNomes.GroupBy(nome => nome);

        foreach (var nome in waypointsNomesAgrupados)
        {
            if (nome.Key == gameObject.name && nome.Count() > 1)
            {
                Debug.LogError(nome.Key + " está duplicado.");
            }
        }
    }

    #endregion ATUALIZAÇÃO_WAYPOINTS

    private int PegarIdWaypoint(string nome)
    {
        nome = nome.Replace("Waypoint (", "");
        nome = nome.Replace(")", "");

        int id = -1;

        try
        {
            id = int.Parse(nome) - 1;
        }
        catch (Exception)
        {
            Debug.LogError("Algum erro ocorreu. Certifique-se de que o Waypoint possui um número válido.");
        }

        return id;
    }
}