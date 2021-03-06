using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { PREGAME, RUNNING, PAUSED, ENDED };

    public GameObject[] SystemPrefab;
    public Events.EventGameState OnGameStateChanged;

    string _currentLevelName = string.Empty;

    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperations;
    GameState _currentGameState = GameState.PREGAME;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);        
    }

    private void Update()
    {
        if(_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnloadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(_currentLevelName);
        }
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            case GameState.ENDED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefab.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefab[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if(ao ==  null)
        {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
            return;
        }

        ao.completed += OnloadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }

        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void GameOver()
    {
        UpdateState(GameState.ENDED);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
