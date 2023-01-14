using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Mechanics;
using UnityEngine;

namespace Game.Core.BoardBase
{
	public class Cell : MonoBehaviour
	{
		public TextMesh LabelText;
		private readonly MatchFinder _matchFinder = new MatchFinder();

		[HideInInspector] public int X;
		[HideInInspector] public int Y;

		[HideInInspector] public Cell FirstCellBelow;
		[HideInInspector] public bool IsFillingCell;
		
		public List<Cell> Neighbours { get; private set; }
		public List<Cell> AllArea { get; private set; }
		
		public List<Cell> GetRowList { get; private set; }
		public List<Cell> GetColumnList { get; private set; }
		

		private Item _item;

		public Board Board { get; private set; }

		public Item Item
		{
			get
			{
				return _item;
			}
			set
			{
				if (_item == value) return;
				
				var oldItem = _item;
				_item = value;
				
				if (oldItem != null && Equals(oldItem.Cell, this))
				{
					oldItem.Cell = null;
				}
				if (value != null)
				{
					value.Cell = this;
				}
			}
		}

		public void Prepare(int x, int y, Board board)
		{
			X = x;
			Y = y;
			transform.localPosition = new Vector3(x,y);
			IsFillingCell = Y == Board.Rows - 1;
			Board = board;
			
			UpdateLabel();
			UpdateNeighbours(Board);
			UpdateAllArea(Board);
			GetColumns(Board);
			GetRows(Board);
		}

		private void UpdateNeighbours(Board board)
		{
			Neighbours = new List<Cell>();
			var up = board.GetNeighbourWithDirection(this, Direction.Up);
			var down = board.GetNeighbourWithDirection(this, Direction.Down);
			var left = board.GetNeighbourWithDirection(this, Direction.Left);
			var right = board.GetNeighbourWithDirection(this, Direction.Right);
			
			if(up!=null) Neighbours.Add(up);
			if(down!=null) Neighbours.Add(down);
			if(left!=null) Neighbours.Add(left);
			if(right!=null) Neighbours.Add(right);

			if (down != null) FirstCellBelow = down;
		}

		public void GetRows(Board board)
		{
			GetRowList = new List<Cell>();

			for (int i = 0; i < Board.Rows; i++)
			{
				GetRowList.Add(board.GetCellWithCoordinate(this.X, i));
			}
		}
		
		public void GetColumns(Board board)
		{
			GetColumnList = new List<Cell>();

			for (int i = 0; i < Board.Cols; i++)
			{
				GetColumnList.Add(board.GetCellWithCoordinate(i, this.Y));
			}
		}

		private void UpdateAllArea(Board board)
		{
			AllArea = new List<Cell>();
			var up = board.GetNeighbourWithDirection(this, Direction.Up);
			var down = board.GetNeighbourWithDirection(this, Direction.Down);
			var left = board.GetNeighbourWithDirection(this, Direction.Left);
			var right = board.GetNeighbourWithDirection(this, Direction.Right);
			var upLeft = board.GetNeighbourWithDirection(this, Direction.UpLeft);
			var upRight = board.GetNeighbourWithDirection(this, Direction.UpRight);
			var downLeft = board.GetNeighbourWithDirection(this, Direction.DownLeft);
			var downRight = board.GetNeighbourWithDirection(this, Direction.DownRight);
			
			if(up!=null) AllArea.Add(up);
			if(down!=null) AllArea.Add(down);
			if(left!=null) AllArea.Add(left);
			if(right!=null) AllArea.Add(right);
			if(upLeft!=null) AllArea.Add(upLeft);
			if(upRight != null) AllArea.Add(upRight);
			if(downLeft != null) AllArea.Add(downLeft);
			if(downRight != null) AllArea.Add(downRight);
		}

		private void UpdateLabel()
		{
			var cellName = X + ":" + Y;
			LabelText.text = cellName;
			gameObject.name = "Cell "+cellName;
		}

		public bool HasItem()
		{
			return Item != null;
		}
		
		public override string ToString()
		{
			return gameObject.name;
		}

		public Cell GetFallTarget()
		{
			var targetCell = this;
			while (targetCell.FirstCellBelow != null && targetCell.FirstCellBelow.Item == null)
			{
				targetCell = targetCell.FirstCellBelow;
			}
			return targetCell;
		}
		


	}
}
