using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedDraggableClickable : NetworkedClickableObject
{
    public bool holding;
    Vector2 centerOffset;
    public void IsHolding(bool hold)
    {
        holding = hold;

        //when release update reference in servercanvas, then update local representation
        GameManager.instance.serverCanvas.UpdateShieldReferencePosition(transform.root.GetComponent<PlayerData>().playerIndex, transform.position);
    }
    public void SetCenterOffsetFromClick()
    {
        centerOffset = transform.position-Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        Debug.Log(IsOwner);
        if (holding && transform.root.GetComponent<PlayerData>().allowedToAct&&IsOwner)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)centerOffset;
        }
    }
}
