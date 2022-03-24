using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text PlayerTwoText;
    public Text PlayerOneScore;
    public Text PlayerTwoScore;
    public AudioSource victoryMusic;
    public AudioSource defeatMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (MenuController.selectedMode == Mode.OnePlayer)
            PlayerTwoText.text = "CPU";
        else
            PlayerTwoText.text = "Player 2";

        PlayerOneScore.text = GameController.ScoreLeft.ToString();
        PlayerTwoScore.text = GameController.ScoreRight.ToString();

        PlayMusic();
    }

    public void PongAgain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StopPonging()
    {
        Application.Quit();
    }

    private void PlayMusic()
    {
        if (MenuController.selectedMode == Mode.TwoPlayer || GameController.ScoreLeft > GameController.ScoreRight)
            victoryMusic.Play();
        else
            defeatMusic.Play();
    }
}
