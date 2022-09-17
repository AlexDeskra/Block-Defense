using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public void StartMovement(List<Vector3> Waypoints, float Speed)
    {
        StartCoroutine(Movement(Waypoints, Speed));
    }
    private IEnumerator Movement(List<Vector3> Waypoints, float Speed)
    {
        int index = 0;
        while (index < Waypoints.Count)
        {
            Vector3 Direction = Waypoints[index] - transform.position;
            Direction.z = 0;
            Direction = Direction.normalized;
            while (Vector3.Distance(transform.position, Waypoints[index]) > 0.1f)
            {
                transform.Translate(Direction * Speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            transform.position = Waypoints[index];
            index++;
        }
        onEnemyDestinationReach();
        yield return null;
    }
    private void onEnemyDestinationReach()
    {
        GetComponent<EnemyObject>().onEnemyDestinationReach();
    }
}
