using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float h;
    private float v;
    private float r;
    public float speed = 10.0f;
    private Animation anim;
    public float hp = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Vector3 dir = ((Vector3.forward * v) + (Vector3.right * h)).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * 200.0f * r); ;

        PlayerAnim();
    }

    void PlayerAnim()
    {
        if (v >= 0.1f)
            anim.CrossFade("RunF", 0.3f);
        else if (v <= -0.1f)
            anim.CrossFade("RunB", 0.3f);
        else if (h >= 0.1f)
            anim.CrossFade("RunR", 0.3f);
        else if (h <= -0.1f)
            anim.CrossFade("RunL", 0.3f);
        else
            anim.CrossFade("Idle", 0.3f);

    }

    void OnTriggerEnter(Collider other)
    {
        if (hp > 0.0f && other.CompareTag("PUNCH"))
        {
            hp -= 10.0f;
            if (hp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        
        for(int i = 0; i < monsters.Length; i++)
        {
            monsters[i].SendMessage("YouWin",SendMessageOptions.DontRequireReceiver);
        }
    }

}
