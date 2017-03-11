using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Top : MonoBehaviour {

	public GameObject prefBall;

	void Start(){
	}

	// ボール生成
	public void CreateBall(){
		GameObject ball = (GameObject)Instantiate (prefBall, prefBall.transform.localPosition, Quaternion.identity);
		ball.transform.SetParent (GameObject.Find ("Canvas").transform, false);
	}

	public void LoadGame(){
		SceneManager.LoadScene ("Game");
	}
}
