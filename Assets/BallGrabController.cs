using MLAPI;
using MLAPI.Messaging;
using System;
using UnityEngine;

public class BallGrabController : NetworkBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    private GameObject ball;
    private BallController ballController;
    private NetworkObject ballNetworkObject;
    private Rigidbody ballRigidBody;
    private NetworkObject networkObject;
    [SerializeField]
    private Transform playerHand;
    private bool grabbed = false;

    public bool HasBall
    {
        get
        {
            return grabbed;
        }
    }

    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();

        if (NetworkManager.Singleton.IsHost && networkObject.IsLocalPlayer)
        {
            ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            ballNetworkObject = ball.GetComponent<NetworkObject>();
            ballNetworkObject.Spawn();
        }
        else
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
            ballNetworkObject = ball.GetComponent<NetworkObject>();
        }

        ballController = ball.GetComponent<BallController>();
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    internal void Drop()
    {
        ball.transform.parent = null;
        ballRigidBody.useGravity = true;
        ballRigidBody.isKinematic = false;
        grabbed = false;
        ballController.Grabbed = false;
    }
    internal void Grab()
    {
        ball.transform.parent = playerHand;
        ball.transform.localPosition = Vector3.zero;
        ballRigidBody.useGravity = false;
        ballRigidBody.isKinematic = true;
        grabbed = true;
        ballController.Grabbed = true;
    }

    void Update()
    {
        if (!networkObject.IsLocalPlayer)
            return;

        if (ballController.Grabbed)
            return;


        if (!Input.GetKey(KeyCode.E))
            return;

        GrabBallServerRpc();
    }

    [ServerRpc]
    void GrabBallServerRpc()
    {
        //if (Vector3.Distance(ball.transform.position, transform.position) > .5)
        //    return;
        ballNetworkObject.ChangeOwnership(networkObject.OwnerClientId);
        GrabBallClientRpc();
    }

    [ClientRpc]
    void GrabBallClientRpc()
    {
        Grab();
    }

    private void OnGUI()
    {
        if (networkObject.IsLocalPlayer && NetworkManager.Singleton.IsHost)
            GUI.TextField(new Rect(120, 0, 100, 20), "Is host");

        if (networkObject.IsLocalPlayer)
            GUI.TextField(new Rect(0, 0, 100, 20), $"Distance is {Vector3.Distance(ball.transform.position, transform.position)}");
    }
}
