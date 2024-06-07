using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySet : MonoBehaviour
{
    // Enemy WayPoint set
    private Transform[] waypoint;
    private int WayCount;
    private int CurrentIndex;
    private MonsterMove monstermove;

    //WayPoint
    public void Setup(Transform[] waypoint)
    {
        monstermove = GetComponent<MonsterMove>();

        WayCount = waypoint.Length;
        this.waypoint = new Transform[WayCount];
        this.waypoint = waypoint;

        transform.position = waypoint[CurrentIndex].position;

        StartCoroutine("OnMove");
    }


    IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            //Transform.Rotate(Vector3.forward);

            if (Vector3.Distance(transform.position, waypoint[CurrentIndex].position) < 0.02f * monstermove.speed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(CurrentIndex < WayCount - 1)
        {
            transform.position = waypoint[CurrentIndex].position;
            CurrentIndex++;
            Vector3 direction = (waypoint[CurrentIndex].position - transform.position).normalized;
            monstermove.movedirection(direction);
            
        }
        else
        {
            transform.position = waypoint[CurrentIndex].position;
            CurrentIndex = 0;
            Vector3 direction = (waypoint[CurrentIndex].position - transform.position).normalized;
            monstermove.movedirection(direction);
        }

    }
}
