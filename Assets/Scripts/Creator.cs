using System.Collections;
using ActiveItems;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private ActiveItem _ballPrefab;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TMP_Text _numberOfBallsText;

    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;
    private int _ballsLeft;
    private Coroutine _waitForLose;

    private void Start()
    {
        CollapseManager.Instance.OnCollapse.AddListener(ResetLoseTimer);
        GameManager.Instance.OnWin.AddListener(StopWaitForLose);

        _ballsLeft = Level.Instance.NumberOfBalls;
        UpdateBallsNumber();

        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    public void UpdateBallsNumber()
    {
        _numberOfBallsText.text = _ballsLeft.ToString();
    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            var ray = new Ray(_spawner.position, Vector3.down);

            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out var hit, 100, _layerMask,
                    QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, hit.distance, 1);
                _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    private void CreateItemInTube()
    {
        if (_ballsLeft == 0)
        {
            Debug.Log("Balls Ended");
            return;
        }

        var level = Random.Range(0, Level.Instance.MaxCreatedLevel);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetLevel(level);
        _itemInTube.SetToTube();
        _ballsLeft--;
        UpdateBallsNumber();
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;

        for (float t = 0; t < 1f; t += Time.deltaTime / 0.4f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }

        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();
        _itemInTube = null;

        CreateItemInTube();
    }

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.Projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);

        if (_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    private void ResetLoseTimer()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    private IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("lose");
        GameManager.Instance.Lose();
    }

    private void StopWaitForLose()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
        }
    }
}