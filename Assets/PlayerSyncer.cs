using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class PlayerSyncer : NetworkBehaviour
{
    private NetworkVariableVector3 position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    private NetworkObject networkObject;

    public override void NetworkStart()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    void Update()
    {
        if (networkObject.IsLocalPlayer)
            position.Value = transform.position;
        else
            transform.position = position.Value;
    }
}
