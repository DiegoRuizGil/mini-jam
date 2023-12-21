using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bear : Animal
{
    public GameObject Player;

    [SerializeField]
    private float runSpeed;

    Rigidbody2D _rb;

    //Animator anim;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        PickPosition();

    }

    void FixedUpdate()
    {

        //if (Vector2.Distance(_rb.position, Player.transform.position) < distanceDetected)
        //{

        //    if(seePlayer == false)
        //    {
        //        StopMovingRandom();
        //        StartCoroutine(AttackPlayerBear());
        //    }

        //    SeePlayer(true);

        //}
        

        if (Vector2.Distance(_rb.position, Player.transform.position) < distanceDetected && seePlayer != true)
        {
            SeePlayer(true);
            StopMovingRandom();
            StartCoroutine(AttackPlayerBear());
        }

        if (Vector2.Distance(_rb.position, Player.transform.position) > distanceDetected)
        {
            SeePlayer(false);
        }
    }

    private IEnumerator AttackPlayerBear()
    {
        float i = 0.0f;
        
        //Vector2 moveDir = vector2 - _rb.position;
        while (i < 3.0f || seePlayer == true)
        {
            Vector2 vector2 = Player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, vector2, runSpeed * Time.deltaTime);
            i += 1 * Time.deltaTime;
            yield return null;
        }

        SeePlayer(false);
        PickPosition();
    }
}
