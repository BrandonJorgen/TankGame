using UnityEngine;

[CreateAssetMenu]
public class ValueChange : ScriptableObject
{
    public FloatData Data;

    public void AddValueToObj(FloatData data)
    {
        Data.Value += data.Value;
    }

    public void MinusValueToObj(FloatData data)
    {
        Data.Value -= data.Value;
    }

    public void MultiplyValueToObj(FloatData data)
    {
        Data.Value *= data.Value;
    }

    public void DivideValueToObj(FloatData data)
    {
        Data.Value /= data.Value;
    }
}
