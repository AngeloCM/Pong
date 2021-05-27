using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RectTransform _playerPosition;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private float _playerSpeed;

    RectTransform _background;
    Vector3 movePlayer;

    Event e;

    void Start()
    {
        e = Event.current;
        movePlayer = _playerPosition.position;
        _background = GameObject.FindGameObjectWithTag("Background").GetComponent<RectTransform>();
    }

    void PlayerMoviment(KeyCode keyPressed)
    {
        switch (keyPressed)
        {
            case KeyCode.UpArrow:
                MoveUp();
                break;
            case KeyCode.DownArrow:
                MoveDown();
                break;
            default:
                Debug.Log("Invalid Key Pressed :" + keyPressed);
                break;
        }
    }

    void MoveUp()
    {
        if (checkedLimitUp())
        {
            movePlayer.y += 1f * _playerSpeed;
            _playerPosition.position = movePlayer;
        }
    }

    void MoveDown()
    {
        if (checkedLimitDown())
        {
            movePlayer.y -= 1f * _playerSpeed;
            _playerPosition.position = movePlayer;
        }
    }

    bool checkedLimitUp()
    {

        if (_playerPosition.anchoredPosition.y + 50 < _background.rect.height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkedLimitDown()
    {
        if (_playerPosition.anchoredPosition.y > 50)
        {
            return true;
        }        
        else
        {
            return false;
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            PlayerMoviment(e.keyCode);
        }
    }
}
