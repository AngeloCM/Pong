using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject WinLose;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        RestartButton.onClick.AddListener(HandleRestartClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void setText(string gameOver)
    {
        WinLose.GetComponent<Text>().text = gameOver;
    }
}
