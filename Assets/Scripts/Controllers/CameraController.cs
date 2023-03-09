using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController : IExecute
    {
        private Transform _cameraT;
        private Transform _playerT;

        public CameraController(Transform cameraT, Transform playerT)
        {
            _cameraT = cameraT;
            _playerT = playerT;
        }

        public void Update()
        {
            _cameraT.position = new Vector3(_playerT.position.x, _playerT.position.y, -100f);
        }
    }
}