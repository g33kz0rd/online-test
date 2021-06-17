using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class PositionAndRotationSyncer : NetworkBehaviour
{
    private NetworkObject networkObject;
    [SerializeField]
    private bool fixedUpdate = false;

    public override void NetworkStart()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    void FixedUpdate()
    {
        if (!fixedUpdate)
            return;

        Sync();
    }

    void Update()
    {
        if (fixedUpdate)
            return;

        Sync();
    }

    private void Sync()
    {
        if (!networkObject.IsOwner)
            return;

        InformServerRpc(transform.position, transform.rotation);
    }



    [ServerRpc]
    private void InformServerRpc(Vector3 position, Quaternion rotation)
    {
        InformClientRpc(position, rotation);
    }

    [ClientRpc]
    private void InformClientRpc(Vector3 position, Quaternion rotation)
    {
        if (networkObject.IsOwner)
            return;

        transform.position = position;
        transform.rotation = rotation;
    }
}
