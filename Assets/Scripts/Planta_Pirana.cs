using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta_Piraña : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            target.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

}
