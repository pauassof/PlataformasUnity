using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody rb;
    private bool isGrounded;
    [SerializeField]
    private GameObject tochosRotos;
    [SerializeField]
    private GameObject luckysRotos;
    private LevelManager lm;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right*speed*Time.deltaTime, Space.World);
            transform.eulerAngles = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed*Time.deltaTime, Space.World);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded==true)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Destruible")
        {
            if (collision.GetContact(0).normal == Vector3.down)
            {
                GameObject clone = Instantiate(tochosRotos, collision.transform.position, collision.transform.rotation);
                Destroy(collision.gameObject);
                Destroy(clone, 5f);
            }
            else if (collision.GetContact(0).normal == Vector3.up)
            {
                isGrounded = true;
            }
        }
        if (collision.gameObject.tag == "DestruibleL")
        {
            if (collision.GetContact(0).normal == Vector3.down)
            {
                GameObject clone = Instantiate(luckysRotos, collision.transform.position, collision.transform.rotation);
                Destroy(collision.gameObject);
                Destroy(clone, 5f);
            }
            
            else if (collision.GetContact(0).normal == Vector3.up)
            {
                isGrounded = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.totalCoins += 1;
            Destroy(other.gameObject);
            lm.UpdateCoins();
        }
        if (other.gameObject.tag == "Limit")
        {
            Death();
        }
    }

    public void Death()
    {
        GameManager.instance.lives -= 1;
        lm.UpdateLives();
        if (GameManager.instance.lives == 0)
        {
            lm.gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            transform.position = lm.spawnPoint.position;
            transform.rotation = lm.spawnPoint.rotation;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
