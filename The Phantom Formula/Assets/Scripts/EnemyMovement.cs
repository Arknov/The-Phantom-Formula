using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public MovementPath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f; // How close you need to get to reach a point
    public float RotationSpeed = 200f; // Speed of rotation in degrees per second
    public float PauseDuration = 1f; // Time to pause before rotating

    [SerializeField] private float visionSpeed = 1.5f; // Speed at which the object moves towards the player
    [SerializeField] private float visionRange = 5f; // Maximum distance for the raycast to detect the player
    [SerializeField] private float visionAngle = 180;
    private GameObject player; // Reference to the player object
    private bool hasLineOfSight = false; // Flag to check if the object has line of sight to the player

    private IEnumerator<Transform> pointInPath;
    private bool isRotating = false;
    private bool isPaused = false;

    private LineRenderer lineRenderer; // LineRenderer to draw the FOV

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

        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            // Log an error if the player object is not found
            Debug.LogError("Player object not found. Please ensure the player object has the tag 'Player'.");
        }

        // Initialize the LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 50; // Number of points to draw the circle
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if (hasLineOfSight && player != null)
        {
            // Calculate direction to next point
            Vector3 direction = (player.transform.position - transform.position).normalized;

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

            // Move towards the player if the object has line of sight and the player is not null
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, visionSpeed * Time.deltaTime);
        }
        else
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

        // Update the FOV visualization
        UpdateFOV();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {

            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToPlayer) <= visionAngle / 2)
            {

                // Cast a ray from the object to the player within the vision range
                RaycastHit2D ray = Physics2D.Raycast(transform.position, directionToPlayer, visionRange, ~LayerMask.GetMask("Enemy"));

                if (ray.collider != null)
                {
                    // Check if the ray hit the player object
                    hasLineOfSight = ray.collider.CompareTag("Player");
                    if (hasLineOfSight)
                    {
                        // Draw a green ray if the object has line of sight to the player
                        Debug.DrawRay(transform.position, directionToPlayer, Color.green);
                    }
                    else
                    {
                        // Draw a red ray if the object does not have line of sight to the player
                        Debug.DrawRay(transform.position, directionToPlayer, Color.red);
                    }
                }
                else
                {
                    // No line of sight if the ray does not hit anything within the vision range
                    hasLineOfSight = false;
                }
            } else
            {
                hasLineOfSight = false;
            }
        }
    }

    private IEnumerator PauseBeforeRotation()
    {
        isPaused = true;
        yield return new WaitForSeconds(PauseDuration);
        isPaused = false;
        isRotating = true;
    }

    private void UpdateFOV()
    {
        float halfAngle = visionAngle / 2f; // Half of the field of view
        float angleStep = visionAngle / (lineRenderer.positionCount - 1); // Angle increment per point
        Vector3 forwardDirection = transform.right; // Enemy's forward direction

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = (-halfAngle + (i * angleStep)) * Mathf.Deg2Rad; // Rotate within FOV
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // Convert angle to direction
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * direction; // Rotate based on enemy's facing direction

            Vector3 point = transform.position + (rotatedDirection * visionRange);
            lineRenderer.SetPosition(i, point);
        }

        // Ensure the first and last points connect back to the enemy for a cone shape
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

        // Change color based on hasLineOfSight
        Color newColor = hasLineOfSight ? Color.red : Color.white; // Red when player is in sight, white otherwise
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
    }
}
