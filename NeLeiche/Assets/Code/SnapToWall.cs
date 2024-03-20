using UnityEngine;

public class SnapToWall : MonoBehaviour
{
    public float snapRadius = 1.0f;
    public LayerMask hideOutLayer;

    private Transform _transform;
    private Rigidbody rb;

    private void Awake()
    {
        _transform = transform;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Check for snap points
            Collider[] colliders = Physics.OverlapSphere(_transform.position, snapRadius, hideOutLayer);
            if (colliders.Length > 0)
            {
                // Snap to the closest snap point
                Transform closestSnapPoint = colliders[0].transform;
                _transform.position = closestSnapPoint.position;
                _transform.rotation = closestSnapPoint.rotation;

                rb.isKinematic = true;
            }
        }

    }
}
