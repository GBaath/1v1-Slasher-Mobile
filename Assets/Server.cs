using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Unity.Netcode;
using UnityEngine.UI;

public class Server : NetworkBehaviour
{

    [ServerRpc(RequireOwnership = false)]
    public void CheckEndTurnServerRpc()
    {
        if (GameManager.instance.localPlayer.finishedTurn.Value && GameManager.instance.otherPlayer.finishedTurn.Value)
        {
            Debug.Log("checkend");
            ResolveTurnClientRpc();
        }
    }
    [ClientRpc]
    public void ResolveTurnClientRpc()
    {
        Debug.Log("Resolve");
        GameManager.instance.turn++;
        //update reference positions
        GameManager.instance.opponentShield.GetComponent<OpponentTranslator>().UpdatePosition(GameManager.instance.otherPlayer.transform.position);
        //check hits
        //fan vad snuskigt TODO remove
        if (FindObjectOfType<SlashFollow>().CheckHit(GameManager.instance.otherPlayer.slashStart.Value,GameManager.instance.otherPlayer.slashEnd.Value))
        {
            //update hp
            GameManager.instance.localPlayer.SetHp(GameManager.instance.localPlayer.hp.Value - GameManager.instance.otherPlayer.dmg.Value);
        }
        //show opponent slash
        //TODO ALL THSI SHIT NEEDS REFERENCESCALING TOO
        GameManager.instance.opponentSword.Draw();

        //sync variables

        GameManager.instance.localPlayer.finishedTurn.Value = false;
        //otherPlayer.finishedTurn.Value = false;
    }
}
