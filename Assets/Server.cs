using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Unity.Netcode;

public class Server : NetworkBehaviour
{

    [ServerRpc(RequireOwnership = false)]
    public void CheckEndTurnServerRpc()
    {
        if (GameManager.instance.localPlayer.finishedTurn.Value && GameManager.instance.otherPlayer.finishedTurn.Value)
        { ///THIS SHIT DONT RUN ON BOTH CUS SOMETHING
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
        if (GameManager.instance.otherPlayer.hit.Value)
        {
            //update hp
            GameManager.instance.localPlayer.SetHp(GameManager.instance.localPlayer.hp.Value - GameManager.instance.otherPlayer.dmg.Value);
        }
        //show opponent slash
        GameManager.instance.opponentSword.Draw(GameManager.instance.otherPlayer.slashStart.Value, GameManager.instance.otherPlayer.slashEnd.Value, 1);

        //sync variables

        GameManager.instance.localPlayer.finishedTurn.Value = false;
        //otherPlayer.finishedTurn.Value = false;
    }
}
