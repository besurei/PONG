using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 0.0f;

	private int playerType = 1;

	private GameObject gameManager;

	// Use this for initialization
	void Start () {

		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

		if (playerType == 0
			&& !gameManager.GetComponent<GameManager>().GetbStop()
			&& !gameManager.GetComponent<GameManager>().GetbClear()) {
			if (Input.GetKey (KeyCode.W)) {
				transform.localPosition += Vector3.up * speed * Time.deltaTime;
		
			} else if (Input.GetKey (KeyCode.S)) {
				transform.localPosition += Vector3.down * speed * Time.deltaTime;
			}

			if (Input.GetMouseButton(0)) {
				Vector3 screenPos = Input.mousePosition;
				Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
				Vector3 globalPos = Camera.main.WorldToViewportPoint (worldPos);
				transform.localPosition = transform.InverseTransformDirection ( new Vector3(transform.localPosition.x, (globalPos.y * 470.0f) -280.0f, 0.0f) );
			}
		}
		else if (playerType == 1) {
			NPCMove();

		}
	}

	public int GetPlayerType(){
		return playerType;
	}
	public void SetPlayerType(int playerTypeNum){
		playerType = playerTypeNum;
		GameObject gameManager = GameObject.Find("GameManager");
		if (gameObject.name.Equals ("PlayerRight")) {
			gameManager.GetComponent<GameManager> ().ExChangeScoreTexts ();
		}

		Destroy (GameObject.Find ("GameSetting"));
		gameManager.GetComponent<GameManager> ().GameStart ();
	}

	void NPCMove(){

		GameObject ball;
		if (ball = GameObject.FindGameObjectWithTag ("Ball")) {

			Vector3 ballLocalPos = ball.GetComponent<Ball> ().GetLocalPosition ();

			int difficulty;

			int[] score = new int[2];
			for (int i = 0; i < 2; i++) {
				score [i] = gameManager.GetComponent<GameManager> ().GetScore (i);
			}

			// 難易度切り替え
			if (score [0] >= score [1] + 5) {
				difficulty = 2;
			} else if (score [0] <= score [1] - 5) {
				difficulty = 0;
			} else {
				difficulty = 1;
			}

			float speed = 0.0f;

			float goalPosY = 0.0f;
			// 難易度によって移動速度を変更
			switch (difficulty) {
			case 0:
				goalPosY = ballLocalPos.y += Random.Range(-30,30);
				speed = Random.Range (220.0f, 270.0f);
				break;

			case 1:
				goalPosY = ballLocalPos.y += Random.Range(-20,20);
				speed = Random.Range (270.0f, 302.0f);
				break;

			case 2:
				goalPosY = ballLocalPos.y += Random.Range(-20,20);
				speed = Random.Range (320.0f, 370.0f);
				break;
			}

			if (transform.localPosition.y < goalPosY) {
				transform.localPosition += Vector3.up * speed * Time.deltaTime;
			} else if (transform.localPosition.y > goalPosY) {
				transform.localPosition += Vector3.down * speed * Time.deltaTime;
			}
		}
	}
}
