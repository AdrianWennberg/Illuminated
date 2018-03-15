using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject shatterEffect;

    [SerializeField]
    float xVel = 0;
    float upVel = 0;
    bool hasBounced = false;
    public Rigidbody rb;
    public Light pointLight;

    private void FixedUpdate()
    {
        if (GameController.Instance.playing)
        {
            if (transform.position.x > 140)
            {
                GameController.Instance.Win();
                transform.parent = GameController.Instance.paddle.transform;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                StartCoroutine(WinMove());
            }
            else
                rb.velocity = new Vector3(xVel, rb.velocity.y);
        }
    }

    public IEnumerator WinMove()
    {
        float maxScale = transform.localScale.x * 5;
        Debug.Log(maxScale);

        while (Mathf.Abs(transform.localPosition.x) > 0.1 && Mathf.Abs(transform.localPosition.y - GameController.Instance.targetStart.y) > 0.1) {
            transform.localPosition -= new Vector3(transform.localPosition.x, (transform.localPosition.y - GameController.Instance.targetStart.y)) * Time.deltaTime;
            
            pointLight.intensity += 2 * Time.deltaTime;
            if (transform.localScale.x < maxScale)
            {
                Debug.Log(transform.localScale.x + " < " + maxScale);
                transform.localScale += Vector3.one * Time.deltaTime;
            }
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameController.Instance.playing == false)
            return;
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
            GameObject ps = Instantiate(shatterEffect, transform.position, Quaternion.identity);
            ps.GetComponent<Rigidbody>().velocity = Vector3.right * xVel;
            ps.GetComponent<ParticleSystem>().Play();
            Destroy(ps, 2.0f);
            gameObject.SetActive(false);
            GameController.Instance.GameOver();
            rb.velocity = Vector3.zero;
        }
    }

    internal void Restart()
    {
        gameObject.SetActive(true);
    }
}
