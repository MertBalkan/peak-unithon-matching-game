using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Mechanics
{	
	public class HintManager : MonoBehaviour 
	{
		[SerializeField] private Board Board;
        private readonly MatchFinder _matchFinder = new MatchFinder();


        private void HighlightHints()
        {
            var visitedCells = new List<Cell>();
            
            for (var y = 0; y < Board.Rows; y++)
            {
                for (var x = 0; x < Board.Cols; x++)
                {
                    var cell = Board.Cells[x, y];
                    
                    if (visitedCells.Contains(cell) || !cell.HasItem()) continue;
                    
                    var matchedCells = GetCellsMatches(cell);
                    var matchedCubeItemCount = _matchFinder.CountMatchedCubeItem(matchedCells);
                    
                    visitedCells.AddRange(matchedCells);
                    
                    for (var i = 0; i < matchedCells.Count; i++)
                    {
                        var currentCell = matchedCells[i];
                        var currentItem = currentCell.Item;
                    
                        if (currentItem.GetMatchType() == MatchType.Special && matchedCells.Count > 1)
                        {
                            if (currentItem.Particle == null)
                                currentItem.Particle = ServiceProvider.GetParticleManager.PlayComboParticleOnItem(currentItem);
                            continue;
                        }

                        SpriteHintUpdate(currentItem, matchedCubeItemCount);
                        ServiceProvider.GetParticleManager.CurrentItemParticleDestroyer(currentItem);
                    }
                }
            }
        }

        private void SpriteHintUpdate(Item item, int clickableCubeItemCount)
        {
            switch (clickableCubeItemCount)
            {
                case < 5:
                    item.DowngradeToBaseSprite();
                    break;
                case < 7:
                    item.UpgradeSprite(ItemType.VerticalRocket);
                    break;
                default:
                    item.UpgradeSprite(ItemType.Bomb);
                    break;
            }
        }
        
        private List<Cell> GetCellsMatches(Cell cell)
        {
            return _matchFinder.FindMatches(cell, cell.Item.GetMatchType());
        }

        private void Update () 
		{
            HighlightHints();
		}
	}
}
