using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelControl : MonoBehaviour
{
    private int hitCount = 0;
    public Texture[] textures;
    public GameObject explosionEffect;
    
    [SerializeField]
    private new MeshRenderer renderer;

    void Start()
    {
        renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0,textures.Length);

        renderer.material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            
            if (++hitCount==3)
            {
                ExpBarrel();
            }
            
        }
    }

    void ExpBarrel()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 2000.0f);
        Destroy(this.gameObject, 3.0f);

        Quaternion rot = Quaternion.Euler(0, Random.Range(0,361), 0); // Vector3.up * Random.Range(0,361)
        GameObject exp = Instantiate(explosionEffect, transform.position, rot);
        Destroy(exp, 5.0f);

    }
}
