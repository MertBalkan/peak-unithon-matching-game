using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Items
{
    public class BalloonItem : Item
    {

        public void PrepareBalloonItem(ItemBase itemBase)
        {
            var balloonSprite = ServiceProvider.GetImageLibrary.BalloonSprite;
            itemBase.Clickable = false;
            itemBase.InterectWithExplode = true;
            Prepare(itemBase, balloonSprite);
        }
    }
}