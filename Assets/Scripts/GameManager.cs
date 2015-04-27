using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    int Score = 0;
    int Health = 3;
    public static System.Action<int> UpdateScore;
    public static System.Action<int> UpdateHealth;
    int CurrentHealth = 3;
	// Use this for initialization
	void Start () {
        UpdateScore += (score) => { };
        UpdateHealth += (health) => { };
        
        SpaceObject.ObjectDestroyed += (type,points)=>{

            if (type != typeof(SpaceShip))
            {
                Score += points;
                UpdateScore(Score);
            }
            else
            {
                CurrentHealth--;
                if (CurrentHealth <= 0)
                {
                    Score = 0;
                    CurrentHealth = Health;                    
                }
                UpdateHealth(CurrentHealth);
                UpdateScore(Score);
            }
        };

        CurrentHealth = Health;
        UpdateHealth(CurrentHealth);
        UpdateScore(Score);
	}
	
	// Update is called once per frame
	void Update () {
	
	}   
}
