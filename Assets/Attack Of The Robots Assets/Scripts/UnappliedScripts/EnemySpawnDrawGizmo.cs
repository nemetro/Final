using UnityEngine;
using System.Collections;

public class PlayerSpawnDrawGizmo : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "playerSpawn.png", true);
    }
}
