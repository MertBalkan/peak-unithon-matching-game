using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using Game.Mechanics;
using UnityEngine;

namespace Game.Items
{
    public class CubeItem : Item
    {
        private MatchType _matchType;
        private readonly MatchFinder _matchFinder = new MatchFinder();

        public void PrepareCubeItem(ItemBase itemBase, MatchType matchType)
        {
            _matchType = matchType;
            itemBase.Clickable = true;
            Prepare(itemBase, GetSpritesForMatchType());
        }

        public override void UpgradeSprite(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Bomb:
                    UpdateColorfulBombSprite();
                    break;
                case ItemType.HorizontalRocket:
                case ItemType.VerticalRocket:
                    UpdateColorfulRocketSprite();
                    break;
                default:
                    break;
            }
        }
        public override void DowngradeToBaseSprite()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            Sprite newSprite;
            switch (_matchType)
            {

                case MatchType.Green:
                    newSprite = imageLibrary.GreenCubeSprite;
                    break;
                case MatchType.Yellow:
                    newSprite = imageLibrary.YellowCubeSprite;
                    break;
                case MatchType.Blue:
                    newSprite = imageLibrary.BlueCubeSprite;
                    break;
                case MatchType.Red:
                    newSprite = imageLibrary.RedCubeSprite;
                    break;
                default:
                    return;
            }

            UpdateSprite(newSprite);
        }

        private void UpdateColorfulBombSprite()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            Sprite newSprite;
            switch (_matchType)
            {

                case MatchType.Green:
                    newSprite = imageLibrary.GreenCubeBombHintSprite;
                    break;
                case MatchType.Yellow:
                    newSprite = imageLibrary.YellowCubeBombHintSprite;
                    break;
                case MatchType.Blue:
                    newSprite = imageLibrary.BlueCubeBombHintSprite;
                    break;
                case MatchType.Red:
                    newSprite = imageLibrary.RedCubeBombHintSprite;
                    break;
                default:
                    return;
            }

            UpdateSprite(newSprite);
        }
        
        private void UpdateColorfulRocketSprite()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            Sprite newSprite;
            switch (_matchType)
            {

                case MatchType.Green:
                    newSprite = imageLibrary.GreenCubeRocketHintSprite;
                    break;
                case MatchType.Yellow:
                    newSprite = imageLibrary.YellowCubeRocketHintSprite;
                    break;
                case MatchType.Blue:
                    newSprite = imageLibrary.BlueCubeRocketHintSprite;
                    break;
                case MatchType.Red:
                    newSprite = imageLibrary.RedCubeRocketHintSprite;
                    break;
                default:
                    return;
            }

            UpdateSprite(newSprite);
        }
        
        private Sprite GetSpritesForMatchType()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            
            switch (_matchType)
            {
                case MatchType.Green:
                    return imageLibrary.GreenCubeSprite;
                case MatchType.Yellow:
                    return imageLibrary.YellowCubeSprite;
                case MatchType.Blue:
                    return imageLibrary.BlueCubeSprite;
                case MatchType.Red:
                    return imageLibrary.RedCubeSprite;
            }

            return null;
        }

        public override MatchType GetMatchType()
        {
            return _matchType;
        }

        public override void TryExecute()
        {
            ServiceProvider.GetParticleManager.PlayCubeParticle(this);
            
            base.TryExecute();
        }
    }
}
