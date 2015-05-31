using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    int Score = 0;
    int Health = 3;
    int CurrentHealth = 3;
    public static System.Action<int> UpdateScore;
    public static System.Action<int> UpdateHealth;
    public static System.Action<GraphicType> ChangeGraphic;
    public static GraphicType GraphicType = global::GraphicType.Mesh;

	// Use this for initialization
	void Start () {
        UpdateScore += (score) => { };
        UpdateHealth += (health) => { };
        ChangeGraphic += (types) => { };
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
        ChangeGraphic(GraphicType);
	}
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GraphicType tmp = GraphicType == global::GraphicType.Mesh ? global::GraphicType.Sprite : global::GraphicType.Mesh;
            ChangeGraphic(tmp);
            GraphicType = tmp;
        }
	}   
}
