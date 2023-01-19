using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerClient : NetworkBehaviour
{
    public NetworkVariable<Vector2> relativeShieldPosition = new NetworkVariable<Vector2>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public Vector2 localRelativeShieldPos;
    private void Start()
    {
        OnPlayerJoin();
        relativeShieldPosition.OnValueChanged += (oldVal, newVal) => 
        { 
            if(newVal != Vector2.zero)
                localRelativeShieldPos = newVal; 
        };
    }

    public void OnPlayerJoin()
    {
        GameManager.instance.OnPlayerJoin(GetComponent<PlayerData>());
        transform.parent = GameManager.instance.serverCanvas.transform;
    }

}
