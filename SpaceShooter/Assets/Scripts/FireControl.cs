using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;
    private new AudioSource audio;
    public AudioClip fireSfx;
    public MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 0.8f);
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2) * 0.5f);
        //muzzleFlash.material.SetTextureOffset("_Maintext", offset);
        muzzleFlash.material.mainTextureOffset = offset;

        Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        Vector3 scale = Vector3.one * Random.Range(1.0f, 2.5f);
        muzzleFlash.transform.localScale = scale; 

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;
    }
    
}
