using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);
	private int hitedPlayerType = 0;	// Player01 = 0; Player02 = 1;
	private float maxSpeed = 400.0f;

	public float speed = 0.0f;

	void Start(){

		hitedPlayerType = Random.Range (0, 2);

		float[] moveVecY = new float[6];
		float val = -3.0f;
		for (int i = 0; i < 6; i++) {
			moveVecY [i] = (float)val + i;

			if (moveVecY[i] == 0.0f) {
				moveVecY[i] = 1.0f;
			}
		}

		switch (hitedPlayerType) {
		case 0:
			moveVector = new Vector3 (1, moveVecY [Random.Range (0, 6)], 0);
			hitedPlayerType = GameObject.Find ("PlayerLeft").GetComponent<Player> ().GetPlayerType ();
			break;

		case 1:
			moveVector = new Vector3 (-1, moveVecY[Random.Range(0,6)], 0);
			hitedPlayerType = GameObject.Find ("PlayerRight").GetComponent<Player> ().GetPlayerType ();
			break;
		}
	}

	void Update(){
		transform.localPosition += ( moveVector * speed ) * Time.deltaTime;
	}

	void SetMoveVector( Vector3 vec ){
		moveVector = vec;
	}

	Vector3 GetMoveVector(){
		return moveVector;
	}

	void OnTriggerEnter( Collider col ){
		if (col.gameObject.CompareTag ("Wall")) {
			SetMoveVector (new Vector3 (moveVector.x, -moveVector.y, moveVector.z));
		} else if (col.gameObject.CompareTag ("Player")) {
			hitedPlayerType = col.gameObject.GetComponent<Player> ().GetPlayerType ();
			SetMoveVector( new Vector3( -moveVector.x, moveVector.y, moveVector.z));

			if(speed < maxSpeed) speed += 50;
		}
	}

	public int GetHitedPlayerType(){
		return hitedPlayerType;
	}

	public Vector3 GetLocalPosition(){
		return transform.localPosition;
	}
}
