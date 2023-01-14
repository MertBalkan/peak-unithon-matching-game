using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Mechanics;
using UnityEngine;

namespace Game.Core.ItemBase
{
    public abstract class Item : MonoBehaviour
    {
        private const int BaseSortingOrder = 10;

        public SpriteRenderer SpriteRenderer;
        public FallAnimation FallAnimation;

        public ItemType ItemType;
        public bool IsFallable;
        public int Health;
        public bool Clickable;
        public bool InterectWithExplode;
        public List<ItemType> ComboAvailableTypes = new List<ItemType>();
        
        private int _childSpriteOrder;


        public ParticleSystem Particle;
        private Cell _cell;

        public Cell Cell
        {
            get { return _cell; }
            set
            {
                if (_cell == value) return;

                var oldCell = _cell;
                _cell = value;

                if (oldCell != null && oldCell.Item == this)
                {
                    oldCell.Item = null;
                }

                if (value != null)
                {
                    value.Item = this;
                    gameObject.name = _cell.gameObject.name + " " + GetType().Name;
                }

            }
        }

        public void Prepare(ItemBase itemBase, Sprite sprite)
        {
            SpriteRenderer = AddSprite(sprite);

            ItemType = itemBase.ItemType;
            FallAnimation = itemBase.FallAnimation;
            IsFallable = itemBase.IsFallable;
            Health = itemBase.Health;
            Clickable = itemBase.Clickable;
            InterectWithExplode = itemBase.InterectWithExplode;
            ComboAvailableTypes = itemBase.ComboAvailableTypes;
            
            FallAnimation.Item = this;
        }

        public SpriteRenderer AddSprite(Sprite sprite)
        {
            var spriteRenderer = new GameObject("Sprite_" + _childSpriteOrder).AddComponent<SpriteRenderer>();
            spriteRenderer.transform.SetParent(transform);
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerID = SortingLayer.NameToID("Item");
            spriteRenderer.sortingOrder = BaseSortingOrder + _childSpriteOrder++;

            return spriteRenderer;
        }

        public void RemoveSprite(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == SpriteRenderer)
            {
                SpriteRenderer = null;
            }

            Destroy(spriteRenderer.gameObject);
        }

        public virtual MatchType GetMatchType()
        {
            return MatchType.None;
        }
        
        public bool IsFalling()
        {
            return FallAnimation.IsFalling;
        }

        public void Fall()
        {
            if (!this.IsFallable) return;
            FallAnimation.FallTo(Cell.GetFallTarget());				
        }
        
        public virtual void TryExecute()
        {
            RemoveItem();
        }
        

        public void UpdateSprite(Sprite sprite)
        {
            var SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = sprite;
        }

        public virtual void UpgradeSprite(ItemType itemType)
        {
            return;
        }

        public virtual void DowngradeToBaseSprite()
        {
            return;
        }

        public void RemoveItem()
        {
            Cell.Item = null;
            Cell = null;

            Destroy(gameObject);
        }

        public override string ToString()
        {
            return gameObject.name;
        }
        
    }
}