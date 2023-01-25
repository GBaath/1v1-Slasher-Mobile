using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerClient : NetworkBehaviour
{
    public NetworkVariable<Vector2> relativeShieldPosition = new NetworkVariable<Vector2>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    private void Start()
    {
        OnPlayerJoin();

    }

    public void OnPlayerJoin()
    {
        GameManager.instance.OnPlayerJoin(GetComponent<PlayerData>());

        //stupid canvas rendering overrides
        transform.SetParent(GameManager.instance.serverCanvas.transform);
        transform.position = Vector3.zero;
        transform.position += Vector3.forward*Camera.main.nearClipPlane;
    }

}
