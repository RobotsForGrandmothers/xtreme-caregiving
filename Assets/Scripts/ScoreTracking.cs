using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracking : MonoBehaviour {
    public Text scoretText;
    public Text deathsText;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        scoretText.text = "Score: " + Despawner.score.ToString();
        deathsText.text = "Deaths: " + Despawner.deaths.ToString();
    }
}
