using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool playerNear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.GetContact(0).normal.y <= -0.5f)
            {
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().Death();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerNear = true;
        }
    }
}
