using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.LevelBase;

namespace Game.Levels
{
    public class LevelTest_2 : LevelData
    {
        public override ItemType GetNextFillItemType()
        {
            return GetRandomCubeItemType();
        }

        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            GridData[0, 0] = ItemType.GreenCube;
            GridData[0, 1] = ItemType.GreenBalloon;
            GridData[0, 2] = ItemType.GreenBalloon;
            GridData[0, 3] = ItemType.GreenCube;
            GridData[0, 4] = ItemType.Balloon;
            GridData[0, 5] = ItemType.RedCube;
            GridData[0, 6] = ItemType.RedBalloon;
            GridData[0, 7] = ItemType.RedBalloon;
            GridData[0, 8] = ItemType.RedCube;

            GridData[1, 0] = ItemType.Balloon;
            GridData[1, 1] = ItemType.Balloon;
            GridData[1, 2] = ItemType.Balloon;
            GridData[1, 3] = ItemType.GreenCube;
            GridData[1, 4] = ItemType.Balloon;
            GridData[1, 5] = ItemType.RedCube;
            GridData[1, 6] = ItemType.Balloon;
            GridData[1, 7] = ItemType.Balloon;
            GridData[1, 8] = ItemType.Balloon;

            GridData[2, 0] = ItemType.Balloon;
            GridData[2, 1] = ItemType.Balloon;
            GridData[2, 2] = ItemType.Balloon;
            GridData[2, 3] = ItemType.Balloon;
            GridData[2, 4] = ItemType.Bomb;
            GridData[2, 5] = ItemType.Balloon;
            GridData[2, 6] = ItemType.Balloon;
            GridData[2, 7] = ItemType.Balloon;
            GridData[2, 8] = ItemType.Balloon;

            GridData[3, 0] = ItemType.None;
            GridData[3, 1] = ItemType.None;
            GridData[3, 2] = ItemType.Crate;
            GridData[3, 3] = ItemType.Bomb;
            GridData[3, 4] = ItemType.Crate;
            GridData[3, 5] = ItemType.Bomb;
            GridData[3, 6] = GetRandomCubeItemType();
            GridData[3, 7] = GetRandomCubeItemType();
            GridData[3, 8] = GetRandomCubeItemType();

            GridData[4, 0] = ItemType.None;
            GridData[4, 1] = ItemType.Crate;
            GridData[4, 2] = ItemType.Bomb;
            GridData[4, 3] = ItemType.Crate;
            GridData[4, 4] = ItemType.Crate;
            GridData[4, 5] = ItemType.Crate;
            GridData[4, 6] = ItemType.Bomb;
            GridData[4, 7] = GetRandomCubeItemType();
            GridData[4, 8] = GetRandomCubeItemType();

            GridData[5, 0] = ItemType.None;
            GridData[5, 1] = ItemType.None;
            GridData[5, 2] = ItemType.Crate;
            GridData[5, 3] = ItemType.Bomb;
            GridData[5, 4] = ItemType.Crate;
            GridData[5, 5] = ItemType.Bomb;
            GridData[5, 6] = GetRandomCubeItemType();
            GridData[5, 7] = GetRandomCubeItemType();
            GridData[5, 8] = GetRandomCubeItemType();

            GridData[6, 0] = ItemType.Balloon;
            GridData[6, 1] = ItemType.Balloon;
            GridData[6, 2] = ItemType.Balloon;
            GridData[6, 3] = ItemType.Balloon;
            GridData[6, 4] = ItemType.Bomb;
            GridData[6, 5] = ItemType.Balloon;
            GridData[6, 6] = ItemType.Balloon;
            GridData[6, 7] = ItemType.Balloon;
            GridData[6, 8] = ItemType.Balloon;

            GridData[7, 0] = ItemType.Balloon;
            GridData[7, 1] = ItemType.Balloon;
            GridData[7, 2] = ItemType.Balloon;
            GridData[7, 3] = ItemType.YellowCube;
            GridData[7, 4] = ItemType.Balloon;
            GridData[7, 5] = ItemType.BlueCube;
            GridData[7, 6] = ItemType.Balloon;
            GridData[7, 7] = ItemType.Balloon;
            GridData[7, 8] = ItemType.Balloon;

            GridData[8, 0] = ItemType.YellowCube;
            GridData[8, 1] = ItemType.YellowBalloon;
            GridData[8, 2] = ItemType.YellowBalloon;
            GridData[8, 3] = ItemType.YellowCube;
            GridData[8, 4] = ItemType.Balloon;
            GridData[8, 5] = ItemType.BlueCube;
            GridData[8, 6] = ItemType.BlueBalloon;
            GridData[8, 7] = ItemType.BlueBalloon;
            GridData[8, 8] = ItemType.BlueCube;
        }
    }
}