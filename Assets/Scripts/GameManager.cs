using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public UnityEvent OnWin;

    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    private static GameManager _instance;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void Win()
    {
        var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Progress.Instance.SetLevel(nextLevelIndex);
        Progress.Instance.AddCoins(50);

        OnWin.Invoke();
        _winWindow.SetActive(true);
    }

    public void Lose() => _loseWindow.SetActive(true);

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}