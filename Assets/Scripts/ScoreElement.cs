using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
  public int Level;
  
  [SerializeField] private ItemType _type;
  [SerializeField] private int _currentScore;
  [SerializeField] private TMP_Text _text;
  [SerializeField] private Transform _iconTransform;
  [SerializeField] private AnimationCurve _scaleCurve;
  [SerializeField] private GameObject _flyingIconPrefab;

  public ItemType Type => _type;
  public int CurrentScore => _currentScore;
  public GameObject Icon => _flyingIconPrefab;
  public Vector3 IconPosition => _iconTransform.position;

  public void AddOne()
  {
    _currentScore--;
    
    if (_currentScore < 0)
    {
      _currentScore = 0;
    }

    _text.text = _currentScore.ToString();

    StartCoroutine(AddAnimation());
  }

  public virtual void Setup(Task task)
  {
    _currentScore = task.Number;
    _text.text = task.Number.ToString();
  }

  private IEnumerator AddAnimation()
  {
    for (float t = 0; t < 1f; t += Time.deltaTime * 1.8f)
    {
      var scale = _scaleCurve.Evaluate(t);
      _iconTransform.localScale = Vector3.one * scale;
      yield return null;
    }

    _iconTransform.localScale = Vector3.one;
  }
}