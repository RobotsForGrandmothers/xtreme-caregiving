using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    // trigger when an old person appears
    void OnTriggerEnter2D(Collider2D oldman) {
        if (oldman.GetComponent<Person>() != null && !oldman.GetComponent<Person>().dead) {
            // tell scorer to score person
			ScoreTracking.globalData.GetComponent<ScoreTracking>().Score (oldman.GetComponent<Person>());

            // getting rid of them
            Destroy(oldman.gameObject);
        }
    }
}