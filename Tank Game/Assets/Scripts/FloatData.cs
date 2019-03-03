using UnityEngine;

[CreateAssetMenu]

public class FloatData : ScriptableObject
{
	public float Value;

	public void AddToValue(float addValue)
	{
		Value += addValue;
	}

	public void SubtractToValue(float minusValue)
	{
		Value -= minusValue;
	}

	public void MultiplyToValue(float multiplyValue)
	{
		Value *= multiplyValue;
	}

	public void DivideToValue(float divideValue)
	{
		Value /= divideValue;
	}
}
