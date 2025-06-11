using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 1f;
    public GameObject explosionEffect;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            /*
            if (!explosionEffect)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            */
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }





}
