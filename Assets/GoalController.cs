using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class GoalController : NetworkBehaviour
{
    public int Score { get; set; }
    [SerializeField]
    private int team;
    private BallController ballController;

    private void FixedUpdate()
    {
        if (ballController != null)
            return;

        var ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball == null)
            return;

        ballController = ball.GetComponent<BallController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ball")
            return;


        if(ballController.OwnerTeam == team)
        GoalServerRpc();
    }

    [ServerRpc]
    private void GoalServerRpc()
    {
        GoalClientRpc();
    }

    [ClientRpc]
    private void GoalClientRpc()
    {
        Score++;
        ballController.Reset();
    }

    private void OnGUI()
    {
        VariableTrackerController.TrackVariable("General", gameObject, "Score", $"Score is: {Score}");
    }
}
