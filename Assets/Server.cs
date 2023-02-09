using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;

public class Server : NetworkBehaviour
{
    [SerializeField] UnityTransport transport;
    //public NetworkVariable<int> nrOfPlayers = new NetworkVariable<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public int nrOfPlayers;

    public void SetAdress(string ip)
    {
        transport.ConnectionData.Address = ip;
      
    }
    public void SetPort(string port)
    {
        transport.ConnectionData.Port = System.Convert.ToUInt16(port);
    }

    [ServerRpc(RequireOwnership = false)]
    public void CheckEndTurnServerRpc()
    {
        if (GameManager.instance.localPlayer.finishedTurn.Value && GameManager.instance.otherPlayer.finishedTurn.Value)
        {
            ResolveTurnClientRpc();
        }
    }
    [ClientRpc]
    public void ResolveTurnClientRpc()
    {
        GameManager.instance.turn++;
        //update reference positions
        GameManager.instance.opponentShield.GetComponent<OpponentTranslator>().UpdatePosition(GameManager.instance.otherPlayer.transform.position);
        //check hits
        //fan vad snuskigt TODO remove
        OpponentSword opSword = FindObjectOfType<OpponentSword>();
        if (FindObjectOfType<SlashFollow>().CheckHit(opSword.start, opSword.end)) 
        {
            Debug.Log("hit from opponent");
            //update hp
            GameManager.instance.localPlayer.SetHp(GameManager.instance.localPlayer.hp.Value - GameManager.instance.otherPlayer.dmg.Value);

            //GameManager.instance.opponentHpSlider.value = GameManager.instance.otherPlayer.hp.Value;
            //SetOpponentHealthSliderClientRpc(GameManager.instance.otherPlayer.playerIndex);
        }
        //show opponent slash
        GameManager.instance.opponentSword.Draw();

        //sync variables

        GameManager.instance.localPlayer.finishedTurn.Value = false;
        //otherPlayer.finishedTurn.Value = false;
    }
    [ClientRpc]
    public void SetOpponentHealthSliderClientRpc(int playerindex) //set opreference slider value if call matches index
    {
        if(GameManager.instance.localPlayer.playerIndex == playerindex)
        {
            GameManager.instance.opponentHpSlider.value = GameManager.instance.otherPlayer.hp.Value;
            if(GameManager.instance.otherPlayer.hp.Value<=0)
            {
                GameManager.instance.GameWin();
            }
        }
    }

}
