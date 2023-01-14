using System.Collections.Generic;
using System.Linq;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;

namespace Game.Mechanics
{
    public class ComboManager
    {
        private readonly MatchFinder _matchFinder = new MatchFinder();
        private List<Cell> _matches;
        public ComboType GetFindComboType(Cell _cell)
        {
            var counterBomb = 0;
            var counterRocket = 0;
            _matches = _matchFinder.FindMatches(_cell, MatchType.Special);
            if (_matches.Count > 1)
            {
                for (int i = 0; i < _matches.Count; i++)
                {
                    Cell currentCell = _matches[i];
                    Item currentItem = currentCell.Item;

                    if (currentItem.ItemType == ItemType.Bomb)
                        counterBomb++;
                    else if (currentItem.ItemType == ItemType.HorizontalRocket || currentItem.ItemType == ItemType.VerticalRocket)
                        counterRocket++;
                }

                if (counterBomb > 1) return ComboType.BombBomb;
                if (counterRocket >= 1 && counterBomb == 1) return ComboType.BombRocket;
                return ComboType.RocketRocket;
            }
            return ComboType.None;
        }
        public void TryExecute(Cell cell)
        {
            Item cellItem = cell.Item;
            switch (GetFindComboType(cell))
            {
                case ComboType.None:
                    cellItem.TryExecute();
                    break;
                case ComboType.BombBomb:
                    BombBombExecute(cell);
                    break;
                case ComboType.BombRocket:
                    BombRocketComboExecute(cell);
                    break;
                case ComboType.RocketRocket:
                    RocketRocketComboExecute(cell);
                    break;
            }
        }

        private void MainExecuter(List<Cell> affectedCells)
        {
            for (int i = 0; i < _matches.Count; i++)
            {
                Cell currentCell = _matches[i];
                Item currentItem = currentCell.Item;
                currentItem.RemoveItem();
            }
            
            for (int i = 0; i < affectedCells.Count; i++)
            {
                Cell currentCell = affectedCells[i];
                if (!currentCell.HasItem()) 
                    continue;
                
                Item currentItem = currentCell.Item;
                currentItem.TryExecute();
            }
        }
        
        private void RocketRocketComboExecute(Cell cell)
        {
            List<Cell> affectedCells = new List<Cell>();

            List<Cell> currentColumnCells = cell.GetColumnList;
            List<Cell> currentRowCells = cell.GetRowList;
            
            affectedCells.AddRange(currentColumnCells);
            affectedCells.AddRange(currentRowCells);
            
            MainExecuter(affectedCells);
        }

        private void BombRocketComboExecute(Cell cell)
        {
            List<Cell> affectedCells = new List<Cell>();

            List<Cell> allAreaCells = cell.AllArea;

            for (int i = 0; i < allAreaCells.Count; i++)
            {
                var currentCell = allAreaCells[i];

                List<Cell> currentColumnCells = currentCell.GetColumnList;
                List<Cell> currentRowCells = currentCell.GetRowList;
                
                affectedCells.AddRange(currentColumnCells);
                affectedCells.AddRange(currentRowCells);
            }
            
            MainExecuter(affectedCells);
        }
        
        private void BombBombExecute(Cell cell)
        {
            List<Cell> affectedCells = new List<Cell>();

            List<Cell> allAreaCells = cell.AllArea;
            affectedCells.AddRange(allAreaCells);
            
            for (int i = 0; i < 2; i++)
            {
                int affectedCellsCount = affectedCells.Count;
                for (int j = 0; j < affectedCellsCount; j++)
                {
                    Cell currentCell = affectedCells[j];
                    affectedCells.AddRange(currentCell.AllArea);
                }
                affectedCells = affectedCells.Distinct().ToList();
            }
            
            MainExecuter(affectedCells);
        }
    }
}