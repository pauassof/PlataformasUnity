using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta_Piraña : Enemy
{
    [SerializeField]
    private float timeToReturnDown;
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
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void ReturnDown()
    {
        
    }
}
