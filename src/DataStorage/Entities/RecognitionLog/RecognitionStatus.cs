
namespace DataStorage.Entities
{
	public enum RecognitionStatus
	{
		/// <summary>Решение содержит распознанный номер.</summary>
		Recognized = 0,

		/// <summary>Решение не содержит распознанный номер.</summary>
		NotRecognized = 1,

		/// <summary>Решение создано оператором.</summary>
		Manual = 2,
	}
}
