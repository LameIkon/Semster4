using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private GameMode _currentMode;

    [SerializeField] private TutorialManager _TutorialManager;
    [SerializeField] private CompletionTrackerBoard _CompletionTrackerBoard;
    [SerializeField] private HighscoreTable _HighscoreTable; 
    //[SerializeField] private LogManager logManager;

    private void Start()
    {
        SetGameMode(GameMode.TutorialMode); // Initialize by setting it to tutorial
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
        _TutorialManager.gameObject.SetActive(true);
        Debug.Log("Entered Tutorial Mode");
    }

    private void ExitTutorialMode()
    {
        _TutorialManager.gameObject.SetActive(false);
        Debug.Log("Exited Tutorial Mode");
    }

    private void EnterNormalGameMode()
    {
        _CompletionTrackerBoard.gameObject.SetActive(true);
        _HighscoreTable.gameObject.SetActive(true);
        //logManager.gameObject.SetActive(true);
        Debug.Log("Entered Normal Game Mode");
    }

    private void ExitNormalGameMode()
    {
        _CompletionTrackerBoard.gameObject.SetActive(false);
        _HighscoreTable.gameObject.SetActive(false);
        Debug.Log("Exited Normal Game Mode");
    }
}
    public enum GameMode
    {
        None,
        TutorialMode,
        NormalGameMode
    }
