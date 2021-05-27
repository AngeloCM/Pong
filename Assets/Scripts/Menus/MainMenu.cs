using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Animation _mainMenuAnimation;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        [SerializeField] private AnimationClip _fadeInAnimation;
        [SerializeField] private AudioSource _mainMenuSong;

        public Events.EventFadeComplete OnMainMenuFadeComplete;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

        }

        public void OnFadeOutComplete()
        {
            OnMainMenuFadeComplete.Invoke(true);
        }

        public void OnFadeInComplete()
        {
            OnMainMenuFadeComplete.Invoke(false);
            UIManager.Instance.SetDummyCameraActive(true);
        }

        void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
            {
                FadeOut();
            }

            if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
            {
                FadeIn();
            }
        }

        public void FadeIn()
        {
            _mainMenuAnimation.Stop();
            _mainMenuAnimation.clip = _fadeInAnimation;
            _mainMenuAnimation.Play();
            PlayMainMenuSong();
        }

        public void FadeOut()
        {
            UIManager.Instance.SetDummyCameraActive(false);

            _mainMenuAnimation.Stop();
            _mainMenuAnimation.clip = _fadeOutAnimation;
            _mainMenuAnimation.Play();
            _mainMenuSong.Stop();
        }

        public void PlayMainMenuSong()
        {
            _mainMenuSong.Play();
        }
    }
}
