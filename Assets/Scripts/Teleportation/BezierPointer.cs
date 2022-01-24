using UnityEngine;

/// <summary>
/// BezierPointer renders a bezier curve. It stops when a collider is hit,
/// and stores data on the hit point and gameobject.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class BezierPointer : MonoBehaviour {
    private LineRenderer lineRenderer;
    
    private readonly Vector3[] points = new Vector3[3];
    
    private Vector3 prevPosition;
    
    // MAX_SEGMENT_COUNT determines how long the curve can be.
    private const int MAX_SEGMENT_COUNT = 10;
    
    // SUB_SEGMENT_COUNT determines the resolution of the curve
    private const int SUB_SEGMENT_COUNT = 50;
    
    private bool endPointDetected;
    private Vector3 endPoint;
    private GameObject endPointTarget;
    
    [SerializeField]
    private float curveFactor = 2.5f;
    
    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update() {
        if (!lineRenderer.enabled) {
            return;
        }
        UpdatePoints();
        DrawCurve();
    }
    
    public void SetActive(bool active) {
        endPointDetected = false;
        endPointTarget = null;
        lineRenderer.enabled = active;
    }

    public void SetColor(Color color) {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    
    /// <summary>
    /// IsHittingTarget returns true if this pointer has hit something,
    /// returning the gameobject `target` that was hit.
    /// </summary>
    public bool IsHittingTarget(out GameObject target) {
        target = null;
        
        if (!endPointDetected) {
            return false;
        }
        
        target = endPointTarget;
        return true;
    }

    /// <summary>
    /// UpdateControlPoints sets 3 points of the Bezier curve; one is the origin, one is slightly
    /// in front of the controller, and one is forward and downward from the controller.
    /// </summary>
    private void UpdatePoints() {
        var t = gameObject.transform;
        var forward = t.forward * 5;
        points[0] = t.position;
        points[1] = points[0] + (forward * 2f / 5f);
        points[2] = points[1] + (forward * 3f / 5f) + Vector3.down * curveFactor;
    }
    
    /// <summary>
    /// DrawCurve draws the entire Bezier curve until we either hit a
    /// collider or reach the maximum number of segments. 
    /// </summary>
    private void DrawCurve() {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, points[0]);
        
        prevPosition = points[0];
        var index = 0;

        while (!DrawSegment(index) && index < MAX_SEGMENT_COUNT) {
            index++;
        }
    }

    /// <summary>
    /// DrawSegment all of the subsegments of a Bezier curve segment indicated by `index`.
    /// Returns true if we hit a collider (at which point we stop drawing the curve), else false.
    /// </summary>
    private bool DrawSegment(int index) {
        for (var subSegmentIndex = 0; subSegmentIndex <= SUB_SEGMENT_COUNT; subSegmentIndex++) {
            if (DrawSubSegment(index, subSegmentIndex)) {
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// DrawSubSegment draws a subsegment of a subsegment of a Bezier curve.
    /// If we hit a collider, stop drawing the curve and return true.
    /// </summary>
    private bool DrawSubSegment(int segmentIndex, int subSegmentIndex) {
        // indexOffset is the total number of subsegments drawn in other Bezier curve segments so far.
        // We need this to know what indices of the line renderer to use for this subsegment.
        var indexOffset = segmentIndex * SUB_SEGMENT_COUNT;
        
        // A bezier curve typically takes `t` values from 0 to 1, but to extend the curve,
        // we add 1 to `t` for every segment drawn.
        var t = segmentIndex + subSegmentIndex / (float) SUB_SEGMENT_COUNT;
        lineRenderer.positionCount =  indexOffset + subSegmentIndex + 1;

        var nextPosition = CalculateBezierPoint(t, points[0], points[1], points[2]);

        // If subsegment intersects with the collider, stop drawing curve and return.
        if (CheckForCollision(prevPosition, nextPosition)) {
            lineRenderer.SetPosition(indexOffset + subSegmentIndex, endPoint);
            endPointDetected = true;
            return true;
        } 
        
        lineRenderer.SetPosition(indexOffset + subSegmentIndex, nextPosition);
        endPointDetected = false;
        prevPosition = nextPosition;
        return false;
    }

    /// <summary>
    /// CheckForCollision draws a ray between two points and checks for any intersecting colliders.
    /// </summary>
    private bool CheckForCollision(Vector3 start, Vector3 end) {
        var r = new Ray(start, end - start);

        if (!Physics.Raycast(r, out var hit, Vector3.Distance(start, end))) {
            return false;
        }
        
        endPointTarget = hit.collider.gameObject;
        endPoint = hit.point;
        return true;
    }

    /// <summary>
    /// CalculateBezier curve returns a point on a Bezier curve given 3 points (`p0`, `p1`, & `p2`)
    /// and a time value `t`.
    /// </summary>
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        return Mathf.Pow((1f - t), 2) * p0 +
               2f * (1f - t) * t * p1 +
               Mathf.Pow(t, 2) * p2;
    }
}