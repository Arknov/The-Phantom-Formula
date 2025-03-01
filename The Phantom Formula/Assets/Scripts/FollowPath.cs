using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovementPath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f; // How close you need to get to reach a point
    public float RotationSpeed = 200f; // Speed of rotation in degrees per second
    public float PauseDuration = 1f; // Time to pause before rotating

    private IEnumerator<Transform> pointInPath;
    private bool isRotating = false;
    private bool isPaused = false;

    void Start()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path is null", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
            Debug.Log("Path has no points", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
        StartCoroutine(PauseBeforeRotation());
    }

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null || isPaused)
        {
            return;
        }

        // Calculate direction to next point
        Vector3 direction = (pointInPath.Current.position - transform.position).normalized;

        // Determine target rotation
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Rotate towards target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        // Check if rotation is complete
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isRotating = false;
        }
        else
        {
            isRotating = true;
        }

        // Move towards point only if rotation is complete
        if (!isRotating)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }

        // If the point is reached, move to the next point
        if (!isRotating && (transform.position - pointInPath.Current.position).sqrMagnitude < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
            StartCoroutine(PauseBeforeRotation()); // Pause before next rotation
        }
    }

    private IEnumerator PauseBeforeRotation()
    {
        isPaused = true;
        yield return new WaitForSeconds(PauseDuration);
        isPaused = false;
        isRotating = true;
    }
}
