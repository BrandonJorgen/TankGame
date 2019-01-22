using UnityEngine;

[CreateAssetMenu]

public class BoolChange : ScriptableObject
{
    public BoolData Data;

    public void MakeTrue()
    {
        Data.Bool = true;
    }

    public void MakeFalse()
    {
        Data.Bool = false;
    }
}