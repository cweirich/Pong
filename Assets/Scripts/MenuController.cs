using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{    
    public static Mode selectedMode;

    public void OnePlayerStart()
    {
        selectedMode = Mode.OnePlayer;
        SceneManager.LoadScene("Table");
    }

    public void TwoPlayersStart()
    {
        selectedMode = Mode.TwoPlayer;
        SceneManager.LoadScene("Table");
    }

    public void StopPonging()
    {
        Application.Quit();
    }
}
