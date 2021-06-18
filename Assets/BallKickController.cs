using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class BallKickController : MonoBehaviour
{
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
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.F))
            return;

        //KickBallServerRpc();
        ball.transform.parent = null;
        ballRigidBody.useGravity = true;
        ballRigidBody.isKinematic = false;
        var force = cameraContainer.transform.forward * kickForce;
        ballRigidBody.AddForce(force);
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
        ball.transform.parent = null;
        ballRigidBody.useGravity = true;
        ballRigidBody.isKinematic = false;
        var force = cameraContainer.transform.forward * kickForce;
        ballRigidBody.AddForce(force);
    }
}
