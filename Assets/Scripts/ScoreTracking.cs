using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreTracking : MonoBehaviour {
	public static ScoreTracking globalData { get { return GameObject.FindGameObjectWithTag ("GlobalData").GetComponent<ScoreTracking>(); } }

	[SerializeField] UnityEvent onGameOver;
	[SerializeField] UnityEvent onGameReset;

	int _score = 0;
	public int score {
		get { return _score; }
		set {
			if (gameOver) return;
			_score = value;
			scoreText.text = "Score: " + _score.ToString ();
		}
	}
	int _deaths = 0;
	public int deaths {
		get { return _deaths; }
		set {
			if (gameOver) return;
			_deaths = value;
			if (deaths >= maxDeaths) gameOver = true;
			deathsText.text = "Deaths: " + _deaths.ToString();
		}
	}

	public int maxDeaths = 50;
	bool _gameOver;
	public bool gameOver { 
		get { return _gameOver; }
		private set { _gameOver = value; if (gameOver) onGameOver.Invoke ();
            Cursor.visible = true;
        }
	}

	public float timeForCombo = 5.0f;
	float timeComboExpires;

	// vars for cointing old ppl types
	int R = 0;
	int G = 0;
	int B = 0;

    public Text scoreText;
    public Text deathsText;

	public void Score(Person p) {
		if (p.GetComponent<PersonRed> ()) {
			++R;
			score += R;
		} else if (p.GetComponent<PersonGreen> ()) {
			++G;
			score += G;
		} else if (p.GetComponent<PersonBlue> ()) {
			++B;
			score += B;
		}
		timeComboExpires = Time.time + timeForCombo;
	}

    // Use this for initialization
    void Start () {
		Reset ();
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeComboExpires) {
			R = G = B = 0;
		}
    }

	public void Reset() {
		// actually, just do this
		if (gameOver) {
			SceneManager.LoadScene ("Main");
		}

		// better but faulty way
		gameOver = false;
		score = 0;
		deaths = 0;
		R = G = B = 0;
		onGameReset.Invoke ();
	}
}
