using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSword : MonoBehaviour
{
    [SerializeField]TrailRenderer swordTrail;

    private void Start()
    {
        Invoke("OnStart",1);
    }
    public void OnStart()
    {
        //Draw(new Vector3(-100, 100, 0), new Vector3(100, -100), 5);
    }
    public void Draw(Vector3 start, Vector3  end, float time)
    {
        Debug.Log("Draw");
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
