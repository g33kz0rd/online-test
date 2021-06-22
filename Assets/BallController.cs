using MLAPI;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool Grabbed { get; private set; }
    public int OwnerTeam { get; private set; }
    [SerializeField]
    private Transform spawnPosition;
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
        transform.position = spawnPosition.position;
    }

    public void Grab(int team)
    {
        Grabbed = true;
        OwnerTeam = team;
    }

    public void Release()
    {
        Grabbed = false;
    }
}
