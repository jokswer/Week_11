using UnityEngine;

namespace PassiveItems
{
    public class Stone : PassiveItem
    {
        [SerializeField] private GameObject _dieEffect;
        [Range(0, 2)] [SerializeField] private int _level = 2;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private Stone _stonePrefab;

        public override void OnAffect()
        {
            base.OnAffect();

            if (_level > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    CreateChildStone(_level - 1);
                }
            }

            Die();
        }

        private void Die()
        {
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void CreateChildStone(int level)
        {
            var stone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
            stone.SetLevel(level);
        }

        private void SetLevel(int level)
        {
            _level = level;
            var scale = 1f;
            if (level == 1)
            {
                scale = .7f;
            }
            else if (level == 0)
            {
                scale = .45f;
            }

            _visualTransform.localScale = Vector3.one * scale;
        }
    }
}