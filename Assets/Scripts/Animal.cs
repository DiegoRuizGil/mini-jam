using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float randomX;
    [SerializeField]
    private float randomY;
    [SerializeField]
    private float minWaitTime;
    [SerializeField]
    private int maxWaitTime;
    private Vector3 currentRandomPos;

    public float distanceDetected;

    [SerializeField]
    private int maxHealt;
    [SerializeField]
    private int lifes;


    public bool seePlayer;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PickPosition()
    {
        currentRandomPos = new Vector3(transform.position.x + Random.Range(-randomX, randomX), transform.position.y + Random.Range(-randomY, randomY), 0);
        StartCoroutine(MoveToRandomPos());
    }

    private IEnumerator MoveToRandomPos()
    {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Vector3 currentPos = transform.position;

        while (i < 1.0f)
        {
            if (seePlayer)
                i = 1f;
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(currentPos, currentRandomPos, i);
            yield return null;
        }

        if (!seePlayer)
            StartCoroutine(WaitForSomeTime());
        

        IEnumerator WaitForSomeTime()
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            PickPosition();
        }
    }

    public void SeePlayer(bool value)
    {
        seePlayer = value;
    }

    public void StopMovingRandom()
    {
        StopCoroutine(MoveToRandomPos());
    }
}
