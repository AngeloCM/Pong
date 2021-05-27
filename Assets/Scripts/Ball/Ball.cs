using Assets.Scripts.Managers;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private Rigidbody2D _rgBall;
    [SerializeField] private float _ballSpeed;

    Vector3 initial;

    int random;

    void Start()
    {
        random = Random.Range(1, 4);
        BallMovement();
    }

    void Update()
    {
        initial = _rgBall.velocity;
    }

    void BallMovement()
    {
        if (random == 1)
        {
            initial = new Vector3(1, 1, 0);
        }
        else if (random == 2)
        {
            initial = new Vector3(-1, 1, 0);
        }
        else if (random == 3)
        {
            initial = new Vector3(1, -1, 0);
        }
        else if (random == 4)
        {
            initial = new Vector3(-1, -1, 0);
        }

        _rgBall.velocity = (initial * _ballSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = initial.magnitude;
        var direction = Vector3.Reflect(initial.normalized, collision.contacts[0].normal);

        _rgBall.velocity = direction * Mathf.Max(speed, 0f);

        if (collision.gameObject.tag == "Left")
        {
            GameManager.Instance.GameOver();
            UIManager.Instance.SetGameOverText("You Lost");
        }

        if (collision.gameObject.tag == "Right")
        {
            GameManager.Instance.GameOver();
            UIManager.Instance.SetGameOverText("You Won");
        }

        if(collision.gameObject.tag == "Player")
        {
            _rgBall.AddForce(_rgBall.velocity * 2f);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            _rgBall.AddForce(_rgBall.velocity * 2f);
        }
    }
}
