using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{

    public float damage;
    public float knockback;
    private float direction = 0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diff = rb.velocity.normalized;
        direction = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rb.MoveRotation(Quaternion.Euler(0f, 0f, direction));
    }
    public void Move(float dir, float force)
    {
        Vector2 vec = new Vector2(Mathf.Cos(dir * Mathf.Deg2Rad), Mathf.Sin(dir * Mathf.Deg2Rad));
        rb.AddForce(vec * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.tag;
        if (tag == "Enemy")
        {
            Destroy(gameObject);
            Enemy_Main_Manager ec = other.GetComponent<Enemy_Main_Manager>();
            ec.GetHit(damage, knockback, rb.velocity.normalized, transform.position);
        }
    }
}
