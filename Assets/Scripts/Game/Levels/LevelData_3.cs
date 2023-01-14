using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.LevelBase;

namespace Game.Levels
{
    public class LevelData_3 : LevelData
    {
        private static readonly ItemType[] ColoredBalloonArray = new[]
        {
            ItemType.GreenBalloon,
            ItemType.YellowBalloon,
            ItemType.BlueBalloon,
            ItemType.RedBalloon
        };

        public override ItemType GetNextFillItemType()
        {
            return GetRandomCubeItemType();
        }

        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            for (var y = 0; y < Board.Rows; y++)
            {
                GridData[0, y] = GetRandomColoredBalloonItemType();
                GridData[Board.Cols - 1, y] = GetRandomColoredBalloonItemType();
            }

            for (var y = 0; y < Board.Rows; y++)
            {
                for (var x = 1; x < Board.Cols - 1; x++)
                {
                    if (GridData[x, y] != ItemType.None) continue;
                    GridData[x, y] = GetRandomCubeItemType();
                }
            }
        }

        private ItemType GetRandomColoredBalloonItemType()
        {
            return GetRandomItemTypeFromArray(ColoredBalloonArray);
        }
    }
}