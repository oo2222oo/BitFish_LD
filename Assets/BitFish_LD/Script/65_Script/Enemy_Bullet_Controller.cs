using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Controller : MonoBehaviour
{
    public float damage;
    public float knockback;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player_Controller ec = other.GetComponent<Player_Controller>();
            if (!ec.isHurt && ec.canHurt)
            {
                Vector2 vec = other.transform.position - transform.position;
                vec.Normalize();
                ec.GetHit(damage, knockback, vec, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
