using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class BallKickController : NetworkBehaviour
{
    private NetworkObject networkObject;
    private GameObject ball;
    private Rigidbody ballRigidBody;

    [SerializeField]
    private float kickForce = 10;

    [SerializeField]
    private BallGrabController grabber;
    [SerializeField]
    private GameObject cameraContainer;

    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();

        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!networkObject.IsLocalPlayer)
            return;

        if (!Input.GetKey(KeyCode.F))
            return;

        KickBallServerRpc();
    }

    [ServerRpc]
    void KickBallServerRpc()
    {
        if (!grabber.HasBall)
            return;

        KickControllerBallClientRpc();
    }

    [ClientRpc]
    void KickControllerBallClientRpc()
    {
        grabber.Drop();

        if (!networkObject.IsLocalPlayer)
            return;

        var force = cameraContainer.transform.forward * kickForce;
        ballRigidBody.AddForce(force);
    }

    private void OnGUI()
    {
        if (networkObject.IsLocalPlayer)
            VariableTrackerController.TrackVariable($"Local Player", gameObject, "HasBall", $"Has ball {grabber.HasBall}");
        else
            VariableTrackerController.TrackVariable($"Player {networkObject.OwnerClientId}", gameObject, "HasBall", $"Has ball {grabber.HasBall}");
    }
}
