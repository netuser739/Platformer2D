using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimatorController;

        private void Awake()
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimatorController = new SpriteAnimatorController(_config);
            _playerAnimatorController.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, 5);
        }

        void Update()
        {
            _playerAnimatorController.Update();
        }
    }
}