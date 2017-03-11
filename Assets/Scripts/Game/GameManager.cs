using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private float[] score = new float[2];
	private bool bStarted = false;
	private bool bStop = false;
	private bool bClear = false;
	private GameObject stopBG;
	private GameObject resultBG;
	private int maxScore = 11;
	private int difficulty = 0;

	public Text[] scoreText = new Text[2];
	public Text buttonText;
	public Text resultPlayerNameText;
	public GameObject[] button = new GameObject[3];
	public GameObject prefBall;
	public GameObject stopObj;
	public GameObject resultObj;

	// ボールの出現範囲
	private float maxBallPosY = 142;
	private float minBallPosY = 66;

	// Use this for initialization
	void Start () {

		if (Time.timeScale < 1.0f)	Time.timeScale = 1.0f;

		stopBG = stopObj.transform.Find ("StopBG").gameObject;
		resultBG = resultObj.transform.Find ("ResultBG").gameObject;

		stopBG.SetActive (false);
		resultBG.SetActive (false);

		ResetScore ();

		for (int i = 0; i < 3; i++) {
			button [i].GetComponent<Image> ().color = Color.white;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (bStarted) {
			for (int i = 0; i < 2; i++) {
				// ストップ時のUIカラー変更
				if (bStop)
					button [i].GetComponent<Image> ().color = Color.gray;
				else
					button [i].GetComponent<Image> ().color = Color.white;

				// スコアを表示
				scoreText [i].text = score [i].ToString ();

				// スコアによるリザルト判定
				if (score [i] >= maxScore)
					Result ();
				
			}
			
			if (bClear) {
				button [2].GetComponent<Image> ().color = Color.gray;
			} 
		}
	}
		
	// ゴールイベントコルーチン
	IEnumerator GoalEvent(){
		yield return new WaitForSeconds (1.0f);
		GameObject gameManager = GameObject.Find ("GameManager");
		gameManager.GetComponent<GameManager> ().CreateBall ();
	}

	// スコア初期化
	void ResetScore(){
		score[0] = 0;
		score [1] = 0;
	}

	// スコア加算
	public void AddScore( int add, int playerType ){
		score [playerType] += add;
	}

	// ボール生成
	public void CreateBall(){
		GameObject ball = (GameObject)Instantiate (prefBall, prefBall.transform.localPosition, Quaternion.identity);
		ball.transform.SetParent (GameObject.Find ("Canvas").transform, false);
	}

	//ボール出現範囲の取得
	public float GetMaxBallPosY(){
		return maxBallPosY;
	}
	public float GetMinBallPosY(){
		return minBallPosY;
	}

	// Top画面読み込み
	public void LoadTop(){
		if (!bStop) {
			Time.timeScale = 1.0f;
			SceneManager.LoadScene ("Top");
		}
	}

	// Game再読み込み
	public void ReLoadGame(){
		if (!bStop) {
			Time.timeScale = 1.0f;
			SceneManager.LoadScene ("Game");
		}
	}

	// Start
	public void GameStart(){
		CreateBall ();
		bStarted = true;
	}
	public bool GetGameStarted(){
		return bStarted;
	}

	// Stop有効・無効化処理
	public void SetStop(){
		if (!bClear) {
			if (!bStop) {
				stopBG.SetActive (true);
				Time.timeScale = 0.0f;
				buttonText.text = "Resume";
				bStop = true;
			} else {
				stopBG.SetActive (false);
				Time.timeScale = 1.0f;
				buttonText.text = "Stop";
				bStop = false;
			}
		}
	}

	// リザルト表示
	public void Result(){
		if (score [0] > score [1]) {
			resultPlayerNameText.text = "PLAYER";
		} else {
			resultPlayerNameText.text = "CPU";
		}
		Time.timeScale = 0.0f;
		resultBG.SetActive (true);
		bClear = true;
	}

	public bool GetbStop(){
		return bStop;
	}
	public bool GetbClear(){
		return bClear;
	}

	public void SetDifficulty( int set ){
		difficulty = set;
	}
	public int GetDifficulty(){
		return difficulty;
	}
	public int GetScore( int playerTypeNum ){
		return (int)score [playerTypeNum];
	}

	public void ExChangeScoreTexts()
	{
		for (int i = 0; i < 2; i++) {
			Vector3 localPos = scoreText [i].transform.parent.transform.localPosition;
			localPos = new Vector3 (-localPos.x, localPos.y, localPos.z);
			scoreText [i].transform.parent.transform.localPosition = localPos;
		}
	}
}
