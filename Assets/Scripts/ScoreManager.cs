using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private ScoreElement[] _scoreElementPrefabs;
    [SerializeField] private ScoreElement[] _scoreElements;
    [SerializeField] private Transform _itemScoreParent;
    [SerializeField] private Camera _camera;

    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _scoreElements = new ScoreElement[_level.Tasks.Length];

        for (var index = 0; index < _level.Tasks.Length; index++)
        {
            var task = _level.Tasks[index];
            var type = task.Type;

            for (var j = 0; j < _scoreElementPrefabs.Length; j++)
            {
                if (type != _scoreElementPrefabs[j].Type) continue;

                var newScoreElement = Instantiate(_scoreElementPrefabs[j], _itemScoreParent);
                newScoreElement.Setup(task);
                _scoreElements[index] = newScoreElement;
            }
        }
    }

    public bool AddScore(ItemType type, Vector3 position, int level = 0)
    {
        for (var i = 0; i < _scoreElements.Length; i++)
        {
            if (_scoreElements[i].Type != type) continue;
            if (_scoreElements[i].CurrentScore == 0) continue;
            if (_scoreElements[i].Level != level) continue;

            StartCoroutine(AddScoreAnimation(_scoreElements[i], position));

            return true;
        }

        return false;
    }

    private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        var icon = Instantiate(scoreElement.Icon, position, Quaternion.identity);
        var a = position;
        var b = position + Vector3.back * 6.5f + Vector3.down * 5f;
        var screenPosition = new Vector3(scoreElement.IconPosition.x, scoreElement.IconPosition.y,
            -_camera.transform.position.z);
        var d = _camera.ScreenToWorldPoint(screenPosition);
        var c = d + Vector3.back * 6f;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
            yield return null;
        }

        Destroy(icon.gameObject);
        scoreElement.AddOne();
        CheckWin();
    }

    private void CheckWin()
    {
        for (var i = 0; i < _scoreElements.Length; i++)
        {
            if (_scoreElements[i].CurrentScore != 0)
            {
                return;
            }
        }

        Debug.Log("Win");
    }
}