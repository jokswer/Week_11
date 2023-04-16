using UnityEngine;

namespace ActiveItems
{
    public class Ball : ActiveItem
    {
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private Renderer _renderer;

        public override void SetLevel(int level)
        {
            base.SetLevel(level);

            _renderer.material = _ballSettings.BallMaterials[level];
            _projection.Setup(_ballSettings.BallTransparentMaterials[level], _levelText.text, Radius);
        }
    }
}