using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Items
{
    public class ColorfulBalloonItem : Item
    {
        private MatchType _matchType;

        public void PrepareColorfulBalloonItem(ItemBase itemBase, MatchType matchType)
        {
            _matchType = matchType;
            Sprite ballonRenderer = GetSpritesForMatchType();
            itemBase.InterectWithExplode = false;
            itemBase.Clickable = false; 
            
            Prepare(itemBase, ballonRenderer);
        }

        private Sprite GetSpritesForMatchType()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;

            switch (_matchType)
            {
                case MatchType.Green:
                    return imageLibrary.GreenBalloonSprite;
                case MatchType.Yellow:
                    return imageLibrary.YellowBalloonSprite;
                case MatchType.Blue:
                    return imageLibrary.BlueBalloonSprite;
                case MatchType.Red:
                    return imageLibrary.RedBalloonSprite;
            }

            return null;
        }

        public override MatchType GetMatchType()
        {
            return _matchType;
        }
    }
}