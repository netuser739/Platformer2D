using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public enum AnimState
    {
        Idle = 0,       //speed = 4-5
        Run = 1,        //speed = 10
        Jump = 2,
        Death = 3,      //speed = 4
        Punch = 4,      //speed = 10
        OpenBag = 5,
        SitFire = 6,    //speed = 10
        SitDown = 7,    //speed = 5
        StandUp = 8,    //speed = 5
    }

    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimState Track;
            public float _speed;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequence = new List<SpriteSequence>();
    }
}