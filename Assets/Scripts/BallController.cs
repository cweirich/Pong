using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float minXSpeed = 0.6f;
    public float maxXSpeed = 1.8f;
    public float minYSpeed = 0.6f;
    public float maxYSpeed = 1.8f;

    public float difficultyMultiplier = 1.2f;

    private Rigidbody2D rigidBody;
    private GameController game;

    public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody = value; }

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed) * (Random.value < 0.5f ? -1 : 1),
                                         Random.Range(minYSpeed, maxYSpeed) * (Random.value < 0.5f ? -1 : 1));
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Limit")
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -rigidBody.velocity.y);
        else if (otherCollider.tag == "Paddle")
        {
            GetComponent<AudioSource>().Play();
            rigidBody.velocity = new Vector2(-rigidBody.velocity.x * difficultyMultiplier, rigidBody.velocity.y * (Random.value < 0.5f ? -1 : 1));
        }
        else if (otherCollider.tag == "LeftLimit")
            game.RightScored();
        else if (otherCollider.tag == "RightLimit")
            game.LeftScored();
    }
}
