using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerData : NetworkBehaviour
{
    public PlayerClient client;

    public NetworkVariable<Vector2> slashStart = new NetworkVariable<Vector2>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Vector2> slashEnd = new NetworkVariable<Vector2>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> hp =  new NetworkVariable<int>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> maxHp = new NetworkVariable<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> dmg = new NetworkVariable<int>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public int playerIndex;

    public NetworkVariable<bool> finishedTurn =  new NetworkVariable<bool>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> hit = new NetworkVariable<bool>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Start()
    {
        if (IsOwner)
        {
            maxHp.Value = 5;
            dmg.Value = 1;
            hp.Value = maxHp.Value;
            GameManager.instance.localHpSlider.maxValue = maxHp.Value;     
            
            hp.OnValueChanged += (_old, _new) =>
            {
               GameManager.instance.localHpSlider.value = _new;
                Debug.Log(_new);
            };    
        }
    }
    public void SetHp(int hp)
    {
        this.hp.Value = hp;
        if (this.hp.Value <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        //other win //will be incorrect if matchdraw
        FirebaseSyncing.instance.SetWinningPlayerName(GameManager.instance.opName);
        GameManager.instance.GameLose();
    }
}
