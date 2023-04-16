using System.Collections;
using TMPro;
using UnityEngine;

namespace ActiveItems
{
    public class ActiveItem : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private float _radius;

        [SerializeField] protected TMP_Text _levelText;
        [SerializeField] protected Projection _projection;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private SphereCollider _trigger;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;

        private float _minRadius = 0.4f;
        private float _maxRadius = 0.7f;
        private float _maxRadiusLevel = 10f;
        private bool _isDead = false;
    
        public float Radius => _radius;
        public int Level => _level;
        public bool IsDead => _isDead;
        public Projection Projection => _projection;

        private void Start()
        {
            _projection.Hide();
        }

        [ContextMenu("IncreaseLevel")]
        public void IncreaseLevel()
        {
            _level++;
            SetLevel(_level);
            _animator.SetTrigger("IncreaseLevel");
            StartCoroutine(ToggleTrigger());
        }

        public virtual void SetLevel(int level)
        {
            _level = level;
            var number = (int)Mathf.Pow(2, _level + 1);
            SetLevelText(number);
            SetRadius(number);
        }

        public void SetToTube()
        {
            _trigger.enabled = false;
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            _rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        public void Drop()
        {
            _trigger.enabled = true;
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            transform.parent = null;
            _rigidbody.velocity = Vector3.down * 1.2f;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void Disable()
        {
            _trigger.enabled = false;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            _isDead = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isDead) return;

            var rb = other.attachedRigidbody;

            if (rb == null || !rb.TryGetComponent<ActiveItem>(out var otherItem)) return;

            if (!otherItem.IsDead && _level == otherItem.Level)
            {
                CollapseManager.Instance.Collapse(this, otherItem);
            }
        }

        private void SetLevelText(int value)
        {
            _levelText.text = value.ToString();
        }

        private void SetRadius(int level)
        {
            _radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxRadiusLevel);
            _visualTransform.localScale = Vector3.one * (_radius * 2f);
            _collider.radius = _radius;
            _trigger.radius = _radius + 0.1f;
        }

        private IEnumerator ToggleTrigger()
        {
            _trigger.enabled = false;
            yield return new WaitForSeconds(0.08f);
            _trigger.enabled = true;
        }
    }
}