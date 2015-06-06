using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static int score = 0;
    static int health = 3;
    static int currentHealth = 3;

    public static int Score
    {
        get { return GameManager.score; }
        set { GameManager.score = value; UpdateScore(value); }
    }

    public static int Health
    {
        get { return GameManager.health; }
        set { GameManager.health = value; }
    }

    public static int CurrentHealth
    {
        get { return GameManager.currentHealth; }
        set { GameManager.currentHealth = value; UpdateHealth(value); }
    }

    public static System.Action<int> UpdateScore;
    public static System.Action<int> UpdateHealth;
    public static System.Action<GraphicType> ChangeGraphicEvent;
    public static GraphicType GraphicType = global::GraphicType.Mesh;
    static bool isGame = false;
    static bool isPause = false;
   
    public static bool IsGame
    {
        get { return GameManager.isGame; }
        set {
            GameManager.isGame = value;            
        }
    }

    public static bool IsPause
    {
        get { return GameManager.isPause; }
        set {
            GameManager.isPause = value;
        }
    }

    public static System.Action<bool> GamePlay;
    public static System.Action<bool> PauseAction;

    static GameManager()
    {
        GamePlay += (state) => { GameManager.IsGame = state; };
        PauseAction += (state) => { GameManager.IsPause = state; Time.timeScale = state ? 0 : 1; };
    }

	// Use this for initialization
	void Start () {
        UpdateScore += (score) => { };
        UpdateHealth += (health) => { };
        ChangeGraphicEvent += (types) => { };
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
        ChangeGraphicEvent(GraphicType);
        PauseAction(false);
        GamePlay(false);
	}

    public static void ChangeGraphic(GraphicType toType)
    {
        ChangeGraphicEvent(toType);
        GraphicType = toType;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GraphicType tmp = GraphicType == global::GraphicType.Mesh ? global::GraphicType.Sprite : global::GraphicType.Mesh;
            ChangeGraphic(tmp);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsGame) return;
            PauseAction( IsPause ? false : true);
        }
	}   
}
