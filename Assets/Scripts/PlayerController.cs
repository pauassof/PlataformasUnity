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
    private LevelManager lm;
    public bool invencible;
    [SerializeField]
    private float powerupTime;
    [SerializeField]
    private GameObject[] powerUpsPreFabs;
    [SerializeField]
    private GameObject tochoDuroPreFab;
    [SerializeField]
    private Transform spawn;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plataform")
        {
            transform.parent = collision.transform;
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
                int azar = Random.Range(0, powerUpsPreFabs.Length);
                GameObject clone = Instantiate(powerUpsPreFabs[azar], collision.transform.position, Quaternion.identity );
                StartCoroutine(PowerUpAnim(clone.transform));
                Instantiate(tochoDuroPreFab, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }

            else if (collision.GetContact(0).normal == Vector3.up)
            {
                isGrounded = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                GameManager.instance.totalCoins += 1;
                Destroy(other.gameObject);
                lm.UpdateCoins();
                break;
            case "Limit":
                Death();
                break;
            case "Finish":
                lm.FinishLevel();
                break;
            case "Vida":
                GameManager.instance.lives += 1;
                lm.UpdateLives();
                Destroy(other.gameObject);
                break;
            case "Estrella":
                invencible = true;
                Invoke("FinishInvencible", powerupTime);
                Destroy(other.gameObject);
                break;
            case "Jump":
                jumpForce *= 1.5f;
                Invoke("FinishJump", powerupTime);
                Destroy(other.gameObject);
                break;
            case "Spawn":
                lm.spawnPoint.position = spawn.position;
                lm.spawnPoint.rotation = spawn.rotation;
                break;
            default: 
                break;

        }
        
    }
    private void FinishInvencible()
    {
        invencible = false;
    }

    private void FinishJump()
    {
        jumpForce /= 1.5f;
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

    IEnumerator PowerUpAnim(Transform transPowerUp)
    {
        Vector3 initialPos = transPowerUp.position;
        Vector3 finalPos = transPowerUp.position + Vector3.up;
        float t = 0;

        while (t<1) 
        {
            t += Time.deltaTime*1.5f;
            transPowerUp.position = Vector3.Lerp(initialPos, finalPos, t);
            yield return null;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Plataform")
        {
            transform.parent = null;
            isGrounded = false;
        }
    }

}
