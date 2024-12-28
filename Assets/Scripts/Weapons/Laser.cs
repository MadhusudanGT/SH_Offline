using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [Header("Laser Configuration")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 8f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private Color laserColor = Color.red;

    [Header("Events")]
    [SerializeField] private UnityEvent OnHitTarget;

    [Header("Debug Settings")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private float hitIndicatorSize = 0.23f;

    private Transform cachedTransform;
    private Vector3 lastStartPosition;
    private Vector3 lastEndPosition;

    private RaycastHit rayHit;

    private void Awake()
    {
        if (!lineRenderer)
        {
            Debug.LogError("LineRenderer is not assigned.");
            enabled = false;
            return;
        }

        cachedTransform = transform;

        lineRenderer.positionCount = 2;
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    private void FixedUpdate()
    {
        UpdateLaserIfNeeded();
    }

    private void UpdateLaserIfNeeded()
    {
        Vector3 start = cachedTransform.position;
        Vector3 direction = cachedTransform.forward;
        Vector3 end;

        if (Physics.Raycast(start, direction, out rayHit, laserDistance, ~ignoreMask))
        {
            end = rayHit.point;
        }
        else
        {
            end = start + direction * laserDistance;
        }

        if (start != lastStartPosition || end != lastEndPosition)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            lastStartPosition = start;
            lastEndPosition = end;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Vector3 start = cachedTransform.position;
        Vector3 direction = cachedTransform.forward;
        Gizmos.color = laserColor;
        Gizmos.DrawRay(start, direction * laserDistance);

        if (rayHit.collider)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(rayHit.point, hitIndicatorSize);
        }
    }

    #region Public Methods

    public void SetLaserDistance(float distance)
    {
        laserDistance = Mathf.Max(0, distance);
    }

    public void SetLaserColor(Color color)
    {
        laserColor = color;
        if (lineRenderer)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }

    #endregion
}
