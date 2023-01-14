using System.Collections.Generic;
using System.Linq;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Items
{
    public class RocketItem : Item
    {
        private bool _rocketDirectionVertical = false;
        private readonly MatchType _matchType = MatchType.Special;
        
        public void PrepareRocketItem(ItemBase itemBase, ItemType itemType)
        {
            if (itemType == ItemType.VerticalRocket)
                _rocketDirectionVertical = true;
            var createLayerBombSprite = (_rocketDirectionVertical) ? 
                ServiceProvider.GetImageLibrary.RocketVertical : ServiceProvider.GetImageLibrary.RocketHorizontal;
            
            itemBase.IsFallable = true;
            itemBase.Clickable = true;
            
            itemBase.ComboAvailableTypes.Add(ItemType.Bomb);
            itemBase.ComboAvailableTypes.Add(ItemType.HorizontalRocket);
            itemBase.ComboAvailableTypes.Add(ItemType.VerticalRocket);
            
            Prepare(itemBase, createLayerBombSprite);
        }

        public bool IsRocketDirectionVertical()
        {
            return _rocketDirectionVertical;
        }
        
        public override MatchType GetMatchType()
        {
            return _matchType;
        }

        public override void TryExecute()
        {
            if (_rocketDirectionVertical)
            {
                var cellList = Cell.GetRowList;
                base.TryExecute();
                for (int i = 0; i < cellList.Count; i++)
                {
                    if (cellList[i].HasItem())
                        cellList[i].Item.TryExecute();
                }
            }
            else
            {
                var cellList = Cell.GetColumnList;
                base.TryExecute();
                for (int i = 0; i < cellList.Count; i++)
                {
                    if (cellList[i].HasItem())
                        cellList[i].Item.TryExecute();
                }
            }
        }
    }
    

}