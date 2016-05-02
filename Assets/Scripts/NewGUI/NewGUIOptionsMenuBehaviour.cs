using UnityEngine;
using System.Collections;

public class NewGUIOptionsMenuBehaviour : MonoBehaviour
{
    public Transform[] ObjectsToGameMenu= new Transform[0];
    public Transform[] ObjectsToMainMenu = new Transform[0];
    // Use this for initialization
    void Start()
    {
        GameManager.ChangeGameStatus += (status) => {
            if (GameManager.instance.GameStatus == GameState.Play)
            {
                gameObject.SetActive(false);
            }
        };
    }

    public void CheckState() 
    {
        if(GameManager.instance.GameStatus == GameState.MainMenu) 
        {
            System.Array.ForEach(ObjectsToMainMenu, o => o.gameObject.SetActive(true));
        }
        if (GameManager.instance.GameStatus == GameState.Pause)
        {
            System.Array.ForEach(ObjectsToGameMenu, o => o.gameObject.SetActive(true));
        }
    }
}
