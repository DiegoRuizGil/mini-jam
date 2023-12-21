using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Deer : Animal
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

        if (Vector2.Distance(_rb.position, Player.transform.position) < distanceDetected && seePlayer != true)
        {
            SeePlayer(true);
            StopMovingRandom();
            StartCoroutine(AttackPlayerDeer());

        }
    }

    private IEnumerator AttackPlayerDeer()
    {
        float i = 0.0f;
        Vector2 vector2 = Player.transform.position;
        Vector2 moveDir = vector2 - _rb.position;
        while (i < 5.0f) 
        {
            
            transform.Translate(moveDir.normalized * runSpeed * Time.deltaTime);
            i += 1 * Time.deltaTime;
            yield return null;
        }
        
        SeePlayer(false);
        PickPosition();
    }

}
