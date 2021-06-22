using MLAPI;
using MLAPI.NetworkVariable;

public class PlayerInfo : NetworkBehaviour
{
    public NetworkVariableInt Team = new NetworkVariableInt(new NetworkVariableSettings()
    {
        ReadPermission = NetworkVariablePermission.Everyone,
        WritePermission = NetworkVariablePermission.OwnerOnly
    });
}
