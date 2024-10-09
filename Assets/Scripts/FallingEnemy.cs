using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : Enemy
{
    [SerializeField]
    private float timeToReturnUp;
    private float initialYPos;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        initialYPos = transform.position.y;
        _speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear == true)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (transform.position.y >= initialYPos)
            {
                playerNear = false;
                _speed = speed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        _speed = 0;
        Invoke("ReturnUp", timeToReturnUp);
    }

    private void ReturnUp()
    {
        _speed = -5;
    }
}
