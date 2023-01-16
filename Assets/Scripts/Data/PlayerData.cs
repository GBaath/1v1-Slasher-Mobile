using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerClient client;

    public LayerMask player1Mask, player2Mask;

    public Vector2 shieldPos;
    public Vector2 slashStart, slashEnd;

    public int hp;
    public int playerIndex;

    private bool _allowedToAct = true;
    public bool allowedToAct
    {
        get { return _allowedToAct; }
        set
        {
            _allowedToAct = value;

        }
    }
    private void Start()
    {
        //if(playerIndex == 0)
        //{
        //    GetComponent<Camera>().cullingMask = player1Mask;
        //    //GetComponentInChildren<NetworkedDraggableClickable>().gameObject.layer = 6; // set local layers so dosent get seen by other
        //    //GetComponentInChildren<OpponentTranslator>().gameObject.layer = 6;
        //    GameManager.instance.opponentShield = GameManager.instance.p2shield; //set local shield position reference to the other player shield
        //}
        //else
        //{
        //    GetComponent<Camera>().cullingMask = player2Mask;
        //    //GetComponentInChildren<NetworkedDraggableClickable>().gameObject.layer = 7;
        //    //GetComponentInChildren<OpponentTranslator>().gameObject.layer = 7;
        //    GameManager.instance.opponentShield = GameManager.instance.p1shield;
        //}

            
    }
}
