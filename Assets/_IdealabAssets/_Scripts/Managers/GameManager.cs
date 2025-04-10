using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private GameMode _currentMode;

    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private CompletionTrackerBoard completionTrackerBoard;
    [SerializeField] private LogManager logManager;

    private void Start()
    {
        SetGameMode(GameMode.TutorialMode);
    }

    public void SetGameMode(GameMode newMode)
    {
        if (_currentMode == newMode) return;

        // Exit current state
        switch (_currentMode)
        {
            case GameMode.TutorialMode:
                ExitTutorialMode();
                break;
            case GameMode.NormalGameMode:
                ExitNormalGameMode();
                break;
        }

        _currentMode = newMode;

        // Enter new state
        switch (_currentMode)
        {
            case GameMode.TutorialMode:
                EnterTutorialMode();
                break;
            case GameMode.NormalGameMode:
                EnterNormalGameMode();
                break;
        }
    }

    private void EnterTutorialMode()
    {
        tutorialManager.gameObject.SetActive(true);
        Debug.Log("Entered Tutorial Mode");
    }

    private void ExitTutorialMode()
    {
        tutorialManager.gameObject.SetActive(false);
        Debug.Log("Exited Tutorial Mode");
    }

    private void EnterNormalGameMode()
    {
        completionTrackerBoard.gameObject.SetActive(true);
        logManager.gameObject.SetActive(true);
        Debug.Log("Entered Normal Game Mode");
    }

    private void ExitNormalGameMode()
    {
        completionTrackerBoard.gameObject.SetActive(false);
        Debug.Log("Exited Normal Game Mode");
    }
}
    public enum GameMode
    {
        None,
        TutorialMode,
        NormalGameMode
    }
