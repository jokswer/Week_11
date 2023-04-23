using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [Header("Ball")] 
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private RawImage _image;
    [SerializeField] private BallSettings _ballSettings;

    public override void Setup(Task task)
    {
        base.Setup(task);

        var number = (int)Mathf.Pow(2, task.Level + 1);
        _levelText.text = number.ToString();
        _image.color = _ballSettings.BallMaterials[task.Level].color;

        Level = task.Level;
    }
}