using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GunController : IExecute
    {
        private Transform _gunTransform;
        private Transform _playerTransform;

        //private float _angleSpeed = 10f;

        public GunController(LevelObjectView gunView, LevelObjectView playerView) 
        { 
            _gunTransform= gunView.transform;
            _playerTransform= playerView.transform;
        }

        public void RotateTowards()
        {
            var angle = Vector3.Angle(Vector3.down, _playerTransform.position - _gunTransform.position);
            if(_playerTransform.position.x < _gunTransform.position.x) angle = -angle;
            _gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void Update()
        {
            RotateTowards();
        }
    }
}