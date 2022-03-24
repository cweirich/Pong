using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private BallController ball;
    private PaddleController paddle;
    private static int scoreLeft = 0;
    private static int scoreRight = 0;
    private Mode selectedMode;
    private bool isPaused = false;

    private enum Side { Left, Right };

    public GameObject ballPrefab;
    public GameObject paddlePrefab;
    public Text scoreLeftText;
    public Text scoreRightText;
    public int victoryScore = 6;
    public AudioSource scoreLeftSfx;
    public AudioSource scoreRightSfx;
    public AudioSource matchPointSfx;

    public static int ScoreLeft { get => scoreLeft; set => scoreLeft = value; }
    public static int ScoreRight { get => scoreRight; set => scoreRight = value; }

    private void Awake()
    {
        scoreLeft = ScoreRight = 0;
        SpawnBall();
        CreateRightPaddle();
        scoreLeftText.text = scoreLeft.ToString();
        scoreRightText.text = scoreRight.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }

    public void LeftScored()
    {
        scoreLeft++;
        scoreLeftText.text = scoreLeft.ToString();
        PlayScoreSfx(Side.Left);
        Destroy(ball.gameObject);
        if (scoreLeft < victoryScore || scoreLeft - scoreRight < 2)
            SpawnBall();
        else
            GameOver();
    }

    public void RightScored()
    {
        scoreRight++;
        scoreRightText.text = scoreRight.ToString();
        PlayScoreSfx(Side.Right);
        Destroy(ball.gameObject);
        if (scoreRight < victoryScore || scoreRight - scoreLeft < 2)
            SpawnBall();
        else
            GameOver();
    }

    private void PlayScoreSfx(Side side)
    {
        if (!isMatchPoint(side))
            if (side == Side.Left)
                scoreLeftSfx.Play();
            else
                scoreRightSfx.Play();
        else
            matchPointSfx.Play();
    }

    private bool isMatchPoint(Side side)
    {
        if (side == Side.Left)
        {
            if (ScoreLeft > victoryScore - 2 && ScoreLeft - ScoreRight > 0)
                return true;
        }
        else
        {
            if (ScoreRight > victoryScore - 2 && ScoreRight - ScoreLeft > 0)
                return true;
        }

        return false;
    }

    private void CreateRightPaddle()
    {
        GameObject paddleInstance = Instantiate(paddlePrefab, transform);

        paddle = paddleInstance.GetComponent<PaddleController>();
        paddle.selectedMode = MenuController.selectedMode;

    }

    private void SpawnBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, transform);

        ball = ballInstance.GetComponent<BallController>();

        ball.transform.position = Vector2.zero;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
