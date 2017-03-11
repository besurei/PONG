using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	void OnTriggerEnter( Collider col ){
		if (col.CompareTag ("Ball")) {
			GameObject ball = col.gameObject;
			Destroy (ball.gameObject);
			GameObject gameManager = GameObject.Find ("GameManager").gameObject;
			gameManager.GetComponent<GameManager> ().AddScore (1, ball.GetComponent<Ball> ().GetHitedPlayerType() );
			gameManager.GetComponent<GameManager> ().StartCoroutine ("GoalEvent");
		}
	}
}
