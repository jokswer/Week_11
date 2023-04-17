using UnityEngine;

namespace ActiveItems
{
    public class Ball : ActiveItem
    {
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _visualTransform;
        
        private float _minRadius = 0.4f;
        private float _maxRadius = 0.7f;
        private float _maxRadiusLevel = 10f;

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _renderer.material = _ballSettings.BallMaterials[level];
            SetRadius(level);
            _projection.Setup(_ballSettings.BallTransparentMaterials[level], _levelText.text, Radius);
        }
        
        private void SetRadius(int level)
        {
            _radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxRadiusLevel);
            _visualTransform.localScale = Vector3.one * (_radius * 2f);
            _collider.radius = _radius;
            _trigger.radius = _radius + 0.1f;
        }

        public override void DoEffect()
        {
            base.DoEffect();
            IncreaseLevel();
        }
    }
}