using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private LevelObjectView _gunView;
        private PlayerController _playerController;
        private GunController _gunController;
        private ListExecuteObjects _executeObjects;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _gunController = new GunController(_gunView, _playerView);
            _executeObjects = new ListExecuteObjects(_playerController);
            _executeObjects.Add(_gunController);
        }

        void Update()
        {
            for (int i = 0; i < _executeObjects.Lenght; i++)
            {
                _executeObjects[i].Update();
            }
        }
    }
}