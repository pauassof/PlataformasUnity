using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "LimitEnemy") 
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

}
