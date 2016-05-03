using UnityEngine;
using System.Collections;

public class OptionsMenuGUI : MonoBehaviour
{
    public Transform[] ObjectsToUsePauseMenu = new Transform[0];
    public Transform[] ObjectsToUseMainMenu = new Transform[0];
    // Use this for initialization
    void Start () {
        GameManager.ChangeGameStatus += (state) => { 
            if(state == GameState.Play) {
                gameObject.SetActive(false);
            }
        };
        gameObject.SetActive(false);
	}

    public void CheckState() {
        switch (GameManager.instance.GameStatus)
        {
            case GameState.MainMenu:
                System.Array.ForEach(ObjectsToUseMainMenu, o => o.gameObject.SetActive(true));
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                System.Array.ForEach(ObjectsToUsePauseMenu, o => o.gameObject.SetActive(true));
                break;
            default:
                break;
        }
    }
}
