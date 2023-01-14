using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;

namespace Game.Items
{
    public class CrateItem : Item
    {
        public void PrepareCrateItem(ItemBase itemBase)
        {
            var crateLayer2Sprite = ServiceProvider.GetImageLibrary.CrateLayer2Sprite;
            itemBase.IsFallable = false;
            itemBase.Health = 2;
            itemBase.InterectWithExplode = true;
            itemBase.Clickable = false;
            Prepare(itemBase, crateLayer2Sprite);
        }
        
        public override void TryExecute()
        {
            Health--;
            if (Health < 1)
            {
                base.TryExecute();
            }
            else
            {
                var crateLayer1Sprite = ServiceProvider.GetImageLibrary.CrateLayer1Sprite;
                UpdateSprite(crateLayer1Sprite);
            }
        }
    }
    

}
