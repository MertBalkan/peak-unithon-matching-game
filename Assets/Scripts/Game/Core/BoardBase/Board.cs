using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Core.Enums;
using Game.Items;
using Game.Managers;
using Game.Mechanics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Core.BoardBase
{
	public class Board : MonoBehaviour
	{
		public const int Rows = 9;
		public const int Cols = 9;
		
		public const int MinimumMatchCount = 2;
		
		public Transform CellsParent;
		public Transform ItemsParent;
		public Transform ParticlesParent;

		[SerializeField] private Cell CellPrefab;

		public readonly Cell[,] Cells = new Cell[Cols, Rows];

		private readonly MatchFinder _matchFinder = new MatchFinder();
		private ComboManager _comboManager = new ComboManager();
		
		public void Prepare()
		{
			CreateCells();
			PrepareCells();
		}
		
		private void CreateCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					var cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
					Cells[x, y] = cell;
				}
			}
		}

		private void PrepareCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					Cells[x, y].Prepare(x, y, this);
				}
			}
		}

		public Cell GetCellWithCoordinate(int x, int y)
		{
			return Cells[x, y];
		}
		
		public void CellTapped(Cell cell)
		{
			if (cell == null) return;

			if (!cell.HasItem()) return;
			
			SpecialTapSwitcher(cell);
		}

		private void SpecialTapSwitcher(Cell cell)
		{
			var cellItem = cell.Item;

			switch (cellItem.GetMatchType())
			{
				case MatchType.Special:
					_comboManager.TryExecute(cell);
					break;
				default:
					ExplodeMatchingCells(cell);
					break;
			}
		}
		
		private void ExplodeMatchingCells(Cell cell)
		{
			var previousCells = new List<Cell>();
			
			var cells = _matchFinder.FindMatches(cell, cell.Item.GetMatchType());
			var matchedCubeItemCount = _matchFinder.CountMatchedCubeItem(cells);

			if (matchedCubeItemCount < MinimumMatchCount) return;
			
			for (var i = 0; i < cells.Count; i++)
			{
				var explodedCell = cells[i];
				
				ExplodeMatchingCellsInNeighbours(explodedCell, previousCells);
				
				var item = explodedCell.Item;
				
				item.TryExecute();
			}
			SpawnBonuses(cell, matchedCubeItemCount);
		}

        public void SpawnBonuses(Cell cell, int clickableCellCount)
        {
	        switch (clickableCellCount)
	        {
		        case >= 7:
			        cell.Item = ServiceProvider.GetItemFactory.CreateItem(
				        ItemType.Bomb, this.ItemsParent);
			        break;
		        case >= 5:
			        cell.Item = ServiceProvider.GetItemFactory.CreateItem(
				        (Random.Range(0, 2) == 1) ? ItemType.HorizontalRocket : ItemType.VerticalRocket, this.ItemsParent);
			        break;
		        default:
			        return;
	        }

	        cell.Item.transform.position = cell.transform.position;
        }
		
        

        private void ExplodeMatchingCellsInNeighbours(Cell cell, List<Cell> previousCells)
		{
			var explodedCellNeighbours = cell.Neighbours;
			
			for (var j = 0; j < explodedCellNeighbours.Count; j++)
			{
				var neighbourCell = explodedCellNeighbours[j];
				var neighbourCellItem = neighbourCell.Item;
				
				if (neighbourCellItem != null && !previousCells.Contains(neighbourCell))
				{
					previousCells.Add(neighbourCell);
					
					if (neighbourCellItem.InterectWithExplode)
						neighbourCellItem.TryExecute();
				}
			}
		}

        public Cell GetNeighbourWithDirection(Cell cell, Direction direction)
		{
			var x = cell.X;
			var y = cell.Y;
			switch (direction)
			{
				case Direction.None:
					break;
				case Direction.Up:
					y += 1;
					break;
				case Direction.UpRight:
					y += 1;
					x += 1;
					break;
				case Direction.Right:
					x += 1;
					break;
				case Direction.DownRight:
					y -= 1;
					x += 1;
					break;
				case Direction.Down:
					y -= 1;
					break;
				case Direction.DownLeft:
					y -= 1;
					x -= 1;
					break;
				case Direction.Left:
					x -= 1;
					break;
				case Direction.UpLeft:
					y += 1;
					x -= 1;
					break;
				default:
					throw new ArgumentOutOfRangeException("direction", direction, null);
			}

			if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

			return Cells[x, y];
		}
	}
}
