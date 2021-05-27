using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button ResumeButton;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button QuitButton;

        private void Start()
        {
            ResumeButton.onClick.AddListener(HandleResumeClicked);
            RestartButton.onClick.AddListener(HandleRestartClicked);
            QuitButton.onClick.AddListener(HandleQuitClicked);
        }

        void HandleResumeClicked()
        {
            GameManager.Instance.TogglePause();
        }

        void HandleRestartClicked()
        {
            GameManager.Instance.RestartGame();
        }

        void HandleQuitClicked()
        {
            GameManager.Instance.QuitGame();
        }
    }
}
