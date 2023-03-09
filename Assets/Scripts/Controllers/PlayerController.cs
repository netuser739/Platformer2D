using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : IExecute
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimatorController;
        private ContactPooler _contactPooler;
        private LevelObjectView _playerView;
        private Transform _playerTransform;
        private Rigidbody2D _rb;

        private float _xInput;
        private bool _isJump;

        private float _speed = 150f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpTreshold = 1f;
        private float _yVelosity = 0;
        private float _xVelosity = 0;

        private bool _isPunching;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimatorController = new SpriteAnimatorController(_config);
            _contactPooler = new ContactPooler(player._collider);
            _playerAnimatorController.StartAnimation(player._spriteRenderer, AnimState.Idle, true);
            _playerView = player;
            _playerTransform = player._transform;
            _rb = player._rb;
        }

        private void MoveTowards()
        {
            _xVelosity = Time.fixedDeltaTime * _speed * (_xInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelosity, _yVelosity);
            _playerTransform.localScale = _xInput < 0 ? _leftScale : _rightScale;
        }

        private bool Attack()
        {
            return _contactPooler.IsGrounded && _isPunching;
        }

        public void Update()
        {
            _playerAnimatorController.Update();
            _contactPooler.Update();
            _xInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xInput) > _movingTreshold;
            _yVelosity = _rb.velocity.y;
            _isPunching = Input.GetMouseButton(0);

            _playerAnimatorController.StartAnimation(_playerView._spriteRenderer,
                    _isPunching ? AnimState.Punch : _isMoving ? AnimState.Run : AnimState.Idle, true);  //сложная конструкция, можно попроще?

            if (_isMoving) 
            {
                MoveTowards();
            }
            else
            {
                _xVelosity = 0;
                _rb.velocity = new Vector2(_xVelosity, _rb.velocity.y);
            }

            if (_contactPooler.IsGrounded)
            {

                if (_isJump && _yVelosity <= _jumpTreshold)  // зачем нужна _yVelosity <= 0?
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
               
            }
            else
            {
                if(_contactPooler.LeftContact || _contactPooler.RightContact)
                {
                    _xVelosity = 0;
                    _rb.velocity = new Vector2(_xVelosity, _rb.velocity.y);
                }
                if (Mathf.Abs(_yVelosity) > _jumpTreshold)
                {
                    _playerAnimatorController.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, false);
                }
            }
        }
    }
}