using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool Grabbed { get; set; }
    private Vector3 spawnPosition;
    private Rigidbody rigidbody;
    private NetworkObject networkObject;

    private void Start()
    {
        if (!NetworkManager.Singleton.IsHost)
            Reset();

        networkObject = GetComponent<NetworkObject>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        VariableTrackerController.TrackVariable("General", gameObject, "BallGrab", Grabbed ? "Ball Grabbed" : "Ball Loose");
    }

    public void Reset()
    {
        if (!NetworkManager.Singleton.IsHost)
            return;

        networkObject.RemoveOwnership();
        rigidbody.velocity = Vector3.zero;
        transform.position = spawnPosition;
    }
}
