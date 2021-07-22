using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("BULLET"))
        {
            Vector3 pos = collision.GetContact(0).point;
            Vector3 normal = collision.GetContact(0).normal;
            Quaternion rot = Quaternion.LookRotation(-normal);

            GameObject spark = Instantiate(sparkEffect, pos, rot);
            Destroy(spark, 0.2f);
            
            
            Destroy(collision.gameObject);
        }
            

    }
}
