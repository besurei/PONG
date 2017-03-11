using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBall : MonoBehaviour {

	private Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);
	private float speed = 200.0f;

	void Start(){

		moveVector = new Vector3 (1.0f, 1.0f, 0.0f);
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
			SetMoveVector( new Vector3( -moveVector.x, moveVector.y, moveVector.z));
		}
	}
		
	public Vector3 GetLocalPosition(){
		return transform.localPosition;
	}
}
