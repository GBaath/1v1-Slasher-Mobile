using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSword : MonoBehaviour
{
    [SerializeField]TrailRenderer swordTrail;


    public void Draw(Vector3 start, Vector3  end, float time)
    {
        StartCoroutine(LerpPosition(start, end, time));
    }
    private IEnumerator LerpPosition(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
