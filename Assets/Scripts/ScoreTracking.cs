using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracking : MonoBehaviour {
	public static ScoreTracking globalData { get { return GameObject.FindGameObjectWithTag ("GlobalData").GetComponent<ScoreTracking>(); } }

	int _score = 0;
	public int score {
		get { return _score; }
		set {
			_score = value;
			scoreText.text = "Score: " + _score.ToString ();
		}
	}
	int _deaths = 0;
	public int deaths {
		get { return _deaths; }
		set {
			_deaths = value;
			deathsText.text = "Deaths: " + _deaths.ToString();
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
		score = 0;
		deaths = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeComboExpires) {
			R = G = B = 0;
		}
    }
}
