using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashFollow : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    Vector2 startPoint, endPoint;

    public Collider2D opponentShieldRef;

    bool follow;
    void Update()
    {
        if (transform.root.GetComponent<PlayerData>().allowedToAct)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(0, startPoint);
                ToggleFollow(true);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ToggleFollow(false);
                CheckHit();
            }
            if (follow)
            {
                //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(1, endPoint);
            }
        }
    }
    public void ToggleFollow(bool enable)
    {
        follow = enable;
        line.enabled = enable;
    }

    public void CheckHit()
    {
        //raycast from start to end, check collide with opponentreference collider
        if(Physics2D.Linecast(startPoint, endPoint).collider == opponentShieldRef)
        {
            //missed
            Debug.Log("Shield");
        }
        else
        {
            //hit
            //dmg to opponent
        }
    }
}
