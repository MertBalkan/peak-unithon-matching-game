using System.Collections.Generic;
using Game.Core.Enums;
using Game.Mechanics;
using UnityEngine;

namespace Game.Core.ItemBase
{
	public class ItemBase : MonoBehaviour
	{
		public FallAnimation FallAnimation;
		public bool IsFallable = true;
		public ItemType ItemType;
		public int Health = 1;
		public bool Clickable = true;
		public bool InterectWithExplode = false;
		public List<ItemType> ComboAvailableTypes = new List<ItemType>();
	}
}
