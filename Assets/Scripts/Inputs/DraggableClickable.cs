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
        clickable = true;
    }

    public void IsHolding(bool hold)
    {
        holding = hold;

        //when release update reference in servercanvas, then update local representation
        GameManager.instance.serverCanvas.UpdateShieldReferencePosition(GameManager.instance.localPlayer.playerIndex, (transform.position));
    }
    public void SetCenterOffsetFromClick()
    {
        centerOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        if (holding)
        {
            transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)centerOffset);
            transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.nearClipPlane);
        }
    }
}
