using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableClickable : ClickableObject
{

    [SerializeField]private PlayerData localPlayer;
    public bool holding;
    Vector2 centerOffset;
    private void Start()
    {
        Invoke(nameof(OnStart), 0.01f);
    }
    private void OnStart()
    {
        localPlayer = GameManager.instance.localPlayer;
    }

    public void IsHolding(bool hold)
    {
        holding = hold;

        //when release update reference in servercanvas, then update local representation
        GameManager.instance.serverCanvas.UpdateShieldReferencePosition(GameManager.instance.localPlayer.playerIndex, Camera.main.WorldToScreenPoint(transform.position));
    }
    public void SetCenterOffsetFromClick()
    {
        centerOffset = transform.position-(Input.mousePosition);
    }

    private void Update()
    {
        if (holding)
        {
            transform.position = (Input.mousePosition) + (Vector3)centerOffset;
        }
    }
}
