using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
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
        if(playerIndex == 0)
            GetComponent<Camera>().cullingMask = player1Mask;
        else
            GetComponent<Camera>().cullingMask = player2Mask;

            
    }
}
