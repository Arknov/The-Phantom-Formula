using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform gunTransform; // Reference to the gun sprite's transform

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        if (gunTransform == null)
        {
            Debug.LogError("Gun Transform is not assigned.");
            return;
        }

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're working in 2D

        // Calculate the direction from the gun to the mouse position
        Vector3 direction = mousePosition - gunTransform.position;

        // Calculate the angle between the gun's forward direction and the direction to the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the gun
        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}