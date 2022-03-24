using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 1;
    public float cpuSpeed = 0.015f;
    public Mode selectedMode;
    public int player;

    private Rigidbody2D rigidBody;
    private BallController ball;
    private float cpuMovement = 0;
    private const float maxCpuMovement = 3;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (selectedMode == Mode.OnePlayer)
            CPUMovement();
        else
            PlayerMovement();
    }

    private void CPUMovement()
    {
        if (ball == null)
            ball = FindObjectOfType<BallController>();

        if (ball == null)
            return;

        float yPaddle = rigidBody.position.y;
        float yBall = ball.RigidBody.position.y;

        if (ball.RigidBody.position.x < 0)
            return;

        if (yPaddle < yBall)
        {
            float tentativeMovement = cpuMovement + cpuSpeed * Time.deltaTime;
            cpuMovement = (int)tentativeMovement >= maxCpuMovement ? 3 : tentativeMovement;
        }
        else if (yPaddle > yBall)
        {
            float tentativeMovement = cpuMovement - cpuSpeed * Time.deltaTime;
            cpuMovement = (int)tentativeMovement <= maxCpuMovement ? -3 : tentativeMovement;
        }

        rigidBody.velocity = new Vector2(0, cpuMovement);
    }

    private void PlayerMovement()
    {
        float verticalMovement = Input.GetAxis("Vertical" + player);

        rigidBody.velocity = new Vector2(0, verticalMovement * speed);
    }
}
