using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashFollow : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] RectTransform slashInputRect;
    Vector3 startPoint, endPoint;


    public Collider2D opponentShieldRef;

    bool follow;

    public bool hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint = new Vector3(startPoint.x, startPoint.y, Camera.main.nearClipPlane);

            //if (slashInputRect.rect.Contains(Input.mousePosition)) //touch within acceptable area
            {
                line.SetPosition(0, startPoint);
                ToggleFollow(true);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint = new Vector3(endPoint.x, endPoint.y, Camera.main.nearClipPlane);
            ToggleFollow(false);
            try
            {
                GameManager.instance.localPlayer.slashStart.Value = startPoint;
                GameManager.instance.localPlayer.slashEnd.Value = endPoint;
            }
            catch 
            {

            }
            endPoint = Vector3.zero;
            startPoint = Vector3.zero;

            //CheckHit();
        }
        if (follow)
        {
            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint = new Vector3(endPoint.x, endPoint.y, Camera.main.nearClipPlane);
            line.SetPosition(1, endPoint);
        }
    }
    public void ToggleFollow(bool enable)
    {
        follow = enable;
        line.enabled = enable;
    }

    public bool CheckHit()
    {
        //raycast from start to end, check collide with opponentreference collider //TODO start & end y -= offsetdifference
        if(Physics2D.Linecast(startPoint, endPoint).collider == opponentShieldRef)
        {
            //missed
            Debug.Log("Shield");
            GameManager.instance.localPlayer.hit.Value = false;
            return false;
        }
        else
        {
            //hit
            GameManager.instance.localPlayer.hit.Value = true;
            return true;
            //dmg to opponent
        }
    }

    //TODO function to reverse enemyslash to animate
    //animation for attack
}
