using System.Collections.Generic;
using System.Linq;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;

namespace Game.Items
{
    public class BombItem : Item
    {
        private readonly MatchType _matchType = MatchType.Special;
        
        public void PrepareBombItem(ItemBase itemBase)
        {
            var createLayerBombSprite = ServiceProvider.GetImageLibrary.BombSprite;
            
            itemBase.IsFallable = true;
            itemBase.Clickable = true;
            
            itemBase.ComboAvailableTypes.Add(ItemType.Bomb);
            itemBase.ComboAvailableTypes.Add(ItemType.HorizontalRocket);
            itemBase.ComboAvailableTypes.Add(ItemType.VerticalRocket);
            
            Prepare(itemBase, createLayerBombSprite);
        }
        
        public override MatchType GetMatchType()
        {
            return _matchType;
        }
        
        public override void TryExecute()
        {
            var explodeBombCellAllArea = Cell.AllArea;
            base.TryExecute();

            for (var i = 0; i < explodeBombCellAllArea.Count; i++)
            {
                var currentCell = explodeBombCellAllArea[i];
                if (!currentCell.HasItem()) continue;
                var currentCellItem = currentCell.Item;
                currentCellItem.TryExecute();
            }
        }
        
    }
    

}