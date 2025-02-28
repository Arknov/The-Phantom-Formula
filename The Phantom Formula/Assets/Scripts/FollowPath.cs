using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPath : MonoBehaviour
{
    public MovementPath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f; //how close you need to get to reach a point

    private IEnumerator<Transform> pointInPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //make sure path is assigned
        if (MyPath == null)
        {
            Debug.LogError("Movement Path is null", gameObject);
            return;
        }
        
        //coroutine from MovementPath.cs
        pointInPath = MyPath.GetNextPathPoint();
        //goes to P0
        pointInPath.MoveNext();
        //if path has no points throw error
        if (pointInPath.Current == null)
        {
            Debug.Log("Path has no points", gameObject);
            return;
        }

        //snaps position to P0 before start
        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        //double check path has a point & currently at a point
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        //look in direction of movement
        Vector3 direction = pointInPath.Current.position - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.001f) //prevents rotation when the point is very close
        {
            float angle = Mathf.Atan2(pointInPath.Current.position.y, pointInPath.Current.position.x) * Mathf.Rad2Deg;
            
            //CHANGE LATER USING VECTOR3.ROTATETOWARDS()
            if (pointInPath.Current.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, -angle + 90f);
            } else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }

        //move towards point
        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);

        //if the point is reached, move on to next point
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }
}
