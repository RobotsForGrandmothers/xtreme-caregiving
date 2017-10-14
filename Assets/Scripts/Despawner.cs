using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {
    public int count = 0;
    public int score = 0;

    // vars for coinging old ppl types
    public int R = 0;
    public int G = 0;
    public int B = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    // trigger when an old person appears
    void OnTriggerEnter2D(Collider2D oldman) {
        if (oldman.GetComponent<Person>() != null)
        {
            // socring your old ppl
            if (count != 5)
            {

                // count the types of old person
                count += 1;
                Debug.Log("added 1 to count total now " + count);
                if (oldman.GetComponent<PersonRed>())
                {
                    R += 1;
                    Debug.Log("1 more red person, total is: " + R);
                }
                if (oldman.GetComponent<PersonGreen>())
                {
                    G += 1;
                    Debug.Log("1 more green person, total is: " + G);
                }
                if (oldman.GetComponent<PersonBlue>())
                {
                    B += 1;
                    Debug.Log("1 more blue person, total is: " + B);
                }
            }
                // scoring them
            if (count == 5)
                {
                    Debug.Log("5 old people passed");
                    count = 0;
                    score += ((R * 1) + (G * 10) + (B * 100));
                    R = 0;
                    G = 0;
                    B = 0;
                    Debug.Log("score is " + score);
                }
          
            // getting rid of them
            Destroy(oldman.gameObject);
        }
    }
}