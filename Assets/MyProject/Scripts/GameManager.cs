using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    private GameObject _pauseMenuUI;
    private InputReader _inputReader;

    public void StartGameManager()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //private void Awake()
    //{
    //    if (Instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    Instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    private void OnDisable()
    {
        _inputReader.OnPause -= PauseGame;
    }

    public void RegisterInput(InputReader inputReader)
    {
        _inputReader = inputReader;
        _inputReader.OnPause += PauseGame;
    }

    public void RegisterPauseMenu(PauseMenu menu)
    {
        _pauseMenuUI = menu.gameObject;
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        _pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log(IsPaused);
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        _pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneLoader.ReloadCurrentScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
