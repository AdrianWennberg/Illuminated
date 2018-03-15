using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField]
    float speed = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameController.Instance.playing)
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0));
	}
}
