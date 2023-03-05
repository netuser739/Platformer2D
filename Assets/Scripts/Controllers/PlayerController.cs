using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : IExecute
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimatorController;
        private LevelObjectView _playerView;
        private Transform _playerTransform;

        private float _xInput;
        private bool _isJump;

        private float _speed = 3f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpTreshold = 1f;
        private float _gravity = -9.8f;
        private float _grounLevel = -3.5f;
        private float _yVelosity;

        private bool _isPunching;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimatorController = new SpriteAnimatorController(_config);
            _playerAnimatorController.StartAnimation(player._spriteRenderer, AnimState.Idle, true);
            _playerView = player;
            _playerTransform = player._transform;
        }

        private void MoveTowards()
        {
            _playerTransform.position += Vector3.right * (Time.deltaTime * _speed * (_xInput < 0 ? -1 : 1));
            _playerTransform.localScale = _xInput < 0 ? _leftScale : _rightScale;
        }

        private bool Attack()
        {
            return IsGrounded() && _isPunching;
        }

        public bool IsGrounded()
        {
            return _playerTransform.position.y <= _grounLevel && _yVelosity <= 0;
        }

        public void Update()
        {
            _playerAnimatorController.Update();
            _xInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xInput) > _movingTreshold;
            _isPunching = Input.GetMouseButton(0);

            if(_isMoving) 
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                _playerAnimatorController.StartAnimation(_playerView._spriteRenderer, 
                    _isPunching?AnimState.Punch:_isMoving?AnimState.Run:AnimState.Idle, true);  //сложная конструкция, можно попроще?

                if (_isJump && _yVelosity <= 0)  // зачем нужна _yVelosity <= 0?
                {
                    _yVelosity = _jumpForce;
                }
                else //if(_yVelosity < 0)   <-- и это?
                {
                    _yVelosity = 0;
                    _playerTransform.position = new Vector3(_playerTransform.position.x, 
                        _grounLevel, _playerTransform.position.z);
                }                
            }
            else
            {
                if(Mathf.Abs(_yVelosity) > _jumpTreshold)
                {
                    _playerAnimatorController.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, false);
                }
                _yVelosity += _gravity * Time.deltaTime;
                _playerTransform.position += Vector3.up * Time.deltaTime * _yVelosity;
            }

        }
    }
}