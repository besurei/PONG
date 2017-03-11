using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour {

	public float speed = 0.0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

			NPCMove();
	}

	void NPCMove(){

		GameObject ball = GameObject.Find ("TitleBall"); 

		Vector3 ballLocalPos = ball.GetComponent<TitleBall> ().GetLocalPosition ();

		float goalPosY = 0.0f;

		goalPosY = ballLocalPos.y;

		if (transform.localPosition.y < goalPosY) {
			transform.localPosition += Vector3.up * speed * Time.deltaTime;
		} else if (transform.localPosition.y > goalPosY) {
			transform.localPosition += Vector3.down * speed * Time.deltaTime;
		}
	}
}
