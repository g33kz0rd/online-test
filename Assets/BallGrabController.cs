using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class BallGrabController : MonoBehaviour
{
    private GameObject ball;
    private NetworkObject ballNetworkObject;
    private Rigidbody ballRigidBody;
    [SerializeField]
    private NetworkObject playerNetworkObject;
    [SerializeField]
    private Transform playerHand;

    public bool HasBall
    {
        get
        {
            return ball.transform.parent == playerHand;
        }
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballNetworkObject = ball.GetComponent<NetworkObject>();
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.E))
            return;

        GrabBallServerRpc();
    }

    [ServerRpc]
    void GrabBallServerRpc()
    {
        //if (Vector3.Distance(ball.transform.position, transform.position) > .5)
        //    return;
        GrabBallClientRpc();
    }

    [ClientRpc]
    void GrabBallClientRpc()
    {
        ball.transform.parent = playerHand;
        ball.transform.localPosition = Vector3.zero;
        ballRigidBody.useGravity = false;
        ballRigidBody.isKinematic = true;
        ballNetworkObject.ChangeOwnership(playerNetworkObject.OwnerClientId);
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 100, 20), $"Distance is {Vector3.Distance(ball.transform.position, transform.position)}");
    }
}
