using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private WinLoseMenu _winloseMenu;
        [SerializeField] private Camera _dummyCamera;

        public Events.EventFadeComplete OnMainMenuFadeComplete;

        private void Start()
        {
            _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
            _mainMenu.PlayMainMenuSong();
        }

        void HandleMainMenuFadeComplete(bool fadeOut)
        {
            OnMainMenuFadeComplete.Invoke(fadeOut);
        }

        void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
            _winloseMenu.gameObject.SetActive(currentState == GameManager.GameState.ENDED);
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.StartGame();
            }
        }

        public void SetDummyCameraActive(bool active)
        {
            _dummyCamera.gameObject.SetActive(active);
        }

        public void SetGameOverText(string gameOver)
        {
            _winloseMenu.setText(gameOver);
        }
    }
}
