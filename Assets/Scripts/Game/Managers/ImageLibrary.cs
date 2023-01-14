using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Managers
{
    public class ImageLibrary : MonoBehaviour, IProvidable
    {
        public Sprite GreenCubeSprite;
        public Sprite GreenCubeRocketHintSprite;
        public Sprite GreenCubeBombHintSprite;
        public Sprite YellowCubeSprite;
        public Sprite YellowCubeRocketHintSprite;
        public Sprite YellowCubeBombHintSprite;
        public Sprite BlueCubeSprite;
        public Sprite BlueCubeRocketHintSprite;
        public Sprite BlueCubeBombHintSprite;
        public Sprite RedCubeSprite;
        public Sprite RedCubeRocketHintSprite;
        public Sprite RedCubeBombHintSprite;

        public Sprite BalloonSprite;
        
        public Sprite GreenBalloonSprite;
        public Sprite YellowBalloonSprite;
        public Sprite BlueBalloonSprite;
        public Sprite RedBalloonSprite;
        
        public Sprite CrateLayer1Sprite;
        public Sprite CrateLayer2Sprite;
        
        public Sprite BombSprite;

        public Sprite RocketVertical;
        public Sprite RocketHorizontal;
        public Sprite RocketUp;
        public Sprite RocketRight;
        public Sprite RocketDown;
        public Sprite RocketLeft;
        

        private void Awake()
        {
            ServiceProvider.Register(this);
        }
    }
}