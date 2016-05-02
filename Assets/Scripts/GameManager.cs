using UnityEngine;
using System.Collections;

public enum GameState { 
    MainMenu,
    Play,
    Pause
}

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

    public GameState GameStatus
    {
        get
        {
            return gameStatus;
        }

        set
        {
            SetGameState(value);
        }
    }
    
    public static System.Action<int> UpdateScore;
    public static System.Action<int> UpdateHealth;
    public static System.Action<GraphicType> ChangeGraphicEvent;
    public static GraphicType GraphicType = global::GraphicType.Mesh;
    GameState gameStatus = GameState.MainMenu;
    public static GameManager instance = null;
    public static System.Action<GameState> ChangeGameStatus;

    public void SetGameState(int stateCode) {
        try
        {
            SetGameState((GameState)stateCode);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void SetGameState(GameState value) {
        gameStatus = value;
        ChangeGameStatus(value);
        switch (value)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;

            case GameState.Play:
                Time.timeScale = 1;
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                break;

            default:
                break;
        }
    }

    // Use this for initialization
    void Awake () {
        instance = this;
        ChangeGameStatus+=(status)=>{ };
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
	}

    public void ChangeGraphic(GraphicType toType)
    {
        ChangeGraphicEvent(toType);
        GraphicType = toType;
    }
    public void ChangeGraphic(int type)
    {
        ChangeGraphicEvent((GraphicType)type);
        GraphicType = (GraphicType)type;
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
            switch (GameStatus)
            {
                case GameState.MainMenu:
                break;

                case GameState.Play:
                    GameStatus = GameState.Pause;
                break;

                case GameState.Pause:
                    GameStatus = GameState.Play;
                break;

                default:
                    break;
            }
        }
	}   

    public void QuitGame() {
        Application.Quit();
    }
}
