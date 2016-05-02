using UnityEngine;
using System.Collections;

public class NewGUISetterNotActiveOnPlay : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GameManager.ChangeGameStatus += (state) => {
            gameObject.SetActive(GameManager.instance.GameStatus == GameState.Pause);
        };
        gameObject.SetActive(false);
    }

}
