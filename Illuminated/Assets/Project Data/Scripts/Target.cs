using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject particleSystem;

    [SerializeField]
    float xVel = 0;
    float upVel = 0;
    bool hasBounced = false;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xVel, rb.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if(hasBounced == false)
            {
                upVel = collision.relativeVelocity.y;
                hasBounced = true;
            }
            rb.velocity = new Vector3(rb.velocity.x, upVel);
        }
        else
        {
            GameObject ps = Instantiate(particleSystem, transform.position, Quaternion.identity);
            ps.GetComponent<Rigidbody>().velocity = Vector3.right * xVel;
            ps.GetComponent<ParticleSystem>().Play();
            Destroy(ps, 2.0f);
            Destroy(gameObject);
        }
    }
}
