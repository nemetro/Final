using UnityEngine;
using System.Collections;

public class WayPointDrawGizmo : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "wayPoint.png", true);
    }
}
