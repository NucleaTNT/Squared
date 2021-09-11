using UnityEngine;

public class ClimbGameManager : BaseGameManager
{
    protected override object[] GetPlayerInitData()
    {
        return new object[]{ 45 };
    }
}
