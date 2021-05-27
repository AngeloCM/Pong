using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _ball;

    RectTransform _ballRect;

    Vector3 moveEnemy;

    void Start()
    {
        moveEnemy = gameObject.transform.position;
        _ballRect = _ball.GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdateEnemyPosition(_ball.transform.position);
    }

    void UpdateEnemyPosition(Vector3 ballPosition)
    {
        if (_ballRect.localPosition.x > 0f)
        {
            if (ballPosition.y > gameObject.transform.position.y)
            {
                moveEnemy.y += 1f;
                gameObject.transform.position = moveEnemy;
            }
            else if (ballPosition.y < gameObject.transform.position.y)
            {
                moveEnemy.y -= 1f;
                gameObject.transform.position = moveEnemy;
            }
        }
    } 
}
