using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _coinsText.text = Progress.Instance.Coins.ToString();
        _levelText.text = $"Level {Progress.Instance.Level}";
        _startButton.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }
}