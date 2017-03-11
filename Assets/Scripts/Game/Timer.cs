using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timeNum = 0;

	private Text timeText = null;

	// Use this for initialization
	void Start () {
		timeText = GameObject.FindGameObjectWithTag ("Time").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("GameManager").GetComponent<GameManager> ().GetGameStarted ()) {
			//	タイムを文字列として表示
			timeText.text = timeNum.ToString ("N0");

			if (timeNum <= 0.0f) {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().Result ();
			}

			timeNum -= Time.deltaTime;
		}
	}
}
