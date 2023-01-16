using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentTranslator : MonoBehaviour
{
    RectTransform localShieldRepresentation;


    //get data from network and update local transform
    public void UpdatePosition(Vector2 opponentCanvasPosition)
    {
        //todo screen to world point
        transform.position = new Vector2(localShieldRepresentation.position.x * -1, localShieldRepresentation.position.y);
    }
}
