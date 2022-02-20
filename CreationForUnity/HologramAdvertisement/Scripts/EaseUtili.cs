using UnityEngine;

namespace Creation.HologramAdvertisement
{
	public enum EaseType
	{
		Linear,
		InSine,
		OutSine,
		InOutSine,
		InQuad,
		OutQuad,
		InOutQuad,
		InCubic,
		OutCubic,
		InOutCubic,
		InQuart,
		OutQuart,
		InOutQuart,
		InQuint,
		OutQuint,
		InOutQuint,
		InExpo,
		OutExpo,
		InOutExpo,
		InCirc,
		OutCirc,
		InOutCirc,
		InElastic,
		OutElastic,
		InOutElastic,
		InBack,
		OutBack,
		InOutBack,
		InBounce,
		OutBounce,
		InOutBounce,
	}

	public static class EaseUtil
	{
		public static float Ease(EaseType easeType, float currentTime, float totalTime, float from, float to)
		{
			switch (easeType)
			{
				case EaseType.Linear:
					return EaseLinear(currentTime, totalTime, from, to);
				case EaseType.InSine:
					return EaseInSine(currentTime, totalTime, from, to);
				case EaseType.OutSine:
					return EaseOutSine(currentTime, totalTime, from, to);
				case EaseType.InOutSine:
					return EaseInOutSine(currentTime, totalTime, from, to);
				case EaseType.InQuad:
					return EaseInQuad(currentTime, totalTime, from, to);
				case EaseType.OutQuad:
					return EaseOutQuad(currentTime, totalTime, from, to);
				case EaseType.InOutQuad:
					return EaseInOutQuad(currentTime, totalTime, from, to);
				case EaseType.InCubic:
					return EaseInCubic(currentTime, totalTime, from, to);
				case EaseType.OutCubic:
					return EaseOutCubic(currentTime, totalTime, from, to);
				case EaseType.InOutCubic:
					return EaseInOutCubic(currentTime, totalTime, from, to);
				case EaseType.InQuart:
					return EaseInQuart(currentTime, totalTime, from, to);
				case EaseType.OutQuart:
					return EaseOutQuart(currentTime, totalTime, from, to);
				case EaseType.InOutQuart:
					return EaseInOutQuart(currentTime, totalTime, from, to);
				case EaseType.InQuint:
					return EaseInQuint(currentTime, totalTime, from, to);
				case EaseType.OutQuint:
					return EaseOutQuint(currentTime, totalTime, from, to);
				case EaseType.InOutQuint:
					return EaseInOutQuint(currentTime, totalTime, from, to);
				case EaseType.InExpo:
					return EaseInExpo(currentTime, totalTime, from, to);
				case EaseType.OutExpo:
					return EaseOutExpo(currentTime, totalTime, from, to);
				case EaseType.InOutExpo:
					return EaseInOutExpo(currentTime, totalTime, from, to);
				case EaseType.InCirc:
					return EaseInCirc(currentTime, totalTime, from, to);
				case EaseType.OutCirc:
					return EaseOutCirc(currentTime, totalTime, from, to);
				case EaseType.InOutCirc:
					return EaseInOutCirc(currentTime, totalTime, from, to);
				case EaseType.InElastic:
					return EaseInElastic(currentTime, totalTime, from, to);
				case EaseType.OutElastic:
					return EaseOutElastic(currentTime, totalTime, from, to);
				case EaseType.InOutElastic:
					return EaseInOutElastic(currentTime, totalTime, from, to);
				case EaseType.InBack:
					return EaseInBack(currentTime, totalTime, from, to);
				case EaseType.OutBack:
					return EaseOutBack(currentTime, totalTime, from, to);
				case EaseType.InOutBack:
					return EaseInOutBack(currentTime, totalTime, from, to);
				case EaseType.InBounce:
					return EaseInBounce(currentTime, totalTime, from, to);
				case EaseType.OutBounce:
					return EaseOutBounce(currentTime, totalTime, from, to);
				case EaseType.InOutBounce:
					return EaseInOutBounce(currentTime, totalTime, from, to);
			}

			return 0;
		}

		public static float EaseLinear(float currentTime, float totalTime, float from, float to)
		{
			return (to - from) * currentTime / totalTime + from;
		}

		public static float EaseInQuad(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return (to - from) * currentTime * currentTime + from;
		}

		public static float EaseOutQuad(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return -(to - from) * currentTime * (currentTime - 2f) + from;
		}

		public static float EaseInOutQuad(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime / 2;
			if (currentTime < 1)
				return (to - from) / 2 * Mathf.Pow(currentTime, 2.0f) + from;
			--currentTime;
			return -(to - from) / 2 * (currentTime * (currentTime - 2) - 1) + from;
		}

		public static float EaseInCubic(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return (to - from) * Mathf.Pow(currentTime, 3.0f) + from;
		}

		public static float EaseOutCubic(float currentTime, float totalTime, float from, float to)
		{
			currentTime = currentTime / totalTime - 1f;
			return (to - from) * (Mathf.Pow(currentTime, 3.0f) + 1f) + from;
		}

		public static float EaseInOutCubic(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime / 2;
			if (currentTime < 1f)
				return (to - from) / 2f * Mathf.Pow(currentTime, 3.0f) + from;
			currentTime -= 2f;
			return (to - from) / 2f * (Mathf.Pow(currentTime, 3.0f) + 2f) + from;
		}

		public static float EaseInQuart(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return (to - from) * Mathf.Pow(currentTime, 4.0f) + from;
		}

		public static float EaseOutQuart(float currentTime, float totalTime, float from, float to)
		{
			currentTime = currentTime / totalTime - 1f;
			return -(to - from) * (Mathf.Pow(currentTime, 4.0f) - 1f) + from;
		}

		public static float EaseInOutQuart(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime / 2;
			if (currentTime < 1f)
				return (to - from) / 2f * Mathf.Pow(currentTime, 4.0f) + from;
			currentTime -= 2f;
			return -(to - from) / 2f * (Mathf.Pow(currentTime, 4.0f) - 2f) + from;
		}

		public static float EaseInQuint(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return (to - from) * Mathf.Pow(currentTime, 5.0f) + from;
		}

		public static float EaseOutQuint(float currentTime, float totalTime, float from, float to)
		{
			currentTime = currentTime / totalTime - 1f;
			return (to - from) * (Mathf.Pow(currentTime, 5.0f) + 1f) + from;
		}

		public static float EaseInOutQuint(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime / 2;
			if (currentTime < 1f)
				return (to - from) / 2f * Mathf.Pow(currentTime, 5.0f) + from;
			currentTime -= 2f;
			return (to - from) / 2f * (Mathf.Pow(currentTime, 5.0f) + 2f) + from;
		}

		public static float EaseInSine(float currentTime, float totalTime, float from, float to)
		{
			return -(to - from) * Mathf.Cos(currentTime / totalTime * (Mathf.PI / 2.0f)) + (to - from) + from;
		}

		public static float EaseOutSine(float currentTime, float totalTime, float from, float to)
		{
			return (to - from) * Mathf.Sin(currentTime / totalTime * (Mathf.PI / 2.0f)) + from;
		}

		public static float EaseInOutSine(float currentTime, float totalTime, float from, float to)
		{
			return -(to - from) / 2.0f * (Mathf.Cos(Mathf.PI * currentTime / totalTime) - 1) + from;
		}

		public static float EaseInExpo(float currentTime, float totalTime, float from, float to)
		{
			if (currentTime <= 0f)
				return from;
			return (to - from) * Mathf.Pow(2f, 10f * (currentTime / totalTime - 1f)) + from;
		}

		public static float EaseOutExpo(float currentTime, float totalTime, float from, float to)
		{
			if (currentTime >= totalTime)
				return to;
			return (to - from) * (-Mathf.Pow(2f, -10f * currentTime / totalTime) + 1f) + from;
		}

		public static float EaseInOutExpo(float currentTime, float totalTime, float from, float to)
		{
			if (currentTime <= 0f)
				return from;
			if (currentTime >= totalTime)
				return to;
			currentTime /= totalTime / 2;
			if (currentTime < 1f)
				return (to - from) / 2 * Mathf.Pow(2f, 10f * (currentTime - 1f)) + from;
			--currentTime;
			return (to - from) / 2 * (-Mathf.Pow(2f, -10f * currentTime) + 2f) + from;
		}

		public static float EaseInCirc(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			return -(to - from) * (Mathf.Sqrt(1f - currentTime * currentTime) - 1f) + from;
		}

		public static float EaseOutCirc(float currentTime, float totalTime, float from, float to)
		{
			currentTime = currentTime / totalTime - 1f;
			return (to - from) * Mathf.Sqrt(1f - currentTime * currentTime) + from;
		}

		public static float EaseInOutCirc(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime / 2;
			if (currentTime < 1f)
				return -(to - from) / 2f * (Mathf.Sqrt(1f - Mathf.Pow(currentTime, 2.0f)) - 1f) + from;
			currentTime -= 2f;
			return (to - from) / 2f * (Mathf.Sqrt(1f - Mathf.Pow(currentTime, 2.0f)) + 1f) + from;
		}

		public static float EaseInBack(float currentTime, float totalTime, float from, float to, float s = 1.70158f)
		{
			currentTime /= totalTime;
			return (to - from) * Mathf.Pow(currentTime, 2.0f) * ((s + 1f) * currentTime - s) + from;
		}

		public static float EaseOutBack(float currentTime, float totalTime, float from, float to, float s = 1.70158f)
		{
			currentTime = currentTime / totalTime - 1f;
			return (to - from) * (Mathf.Pow(currentTime, 2.0f) * ((s + 1f) * currentTime + s) + 1f) + from;
		}

		public static float EaseInOutBack(float currentTime, float totalTime, float from, float to, float s = 1.70158f)
		{
			currentTime /= totalTime / 2f;
			s *= 1.525f;
			if (currentTime < 1f)
				return (to - from) / 2f * (Mathf.Pow(currentTime, 2.0f) * ((s + 1f) * currentTime - s)) + from;
			currentTime -= 2f;
			return (to - from) / 2f * (Mathf.Pow(currentTime, 2.0f) * ((s + 1f) * currentTime + s) + 2f) + from;
		}

		public static float EaseOutBounce(float currentTime, float totalTime, float from, float to)
		{
			currentTime /= totalTime;
			if (currentTime < 1.0f / 2.75f)
				return (to - from) * (7.5625f * Mathf.Pow(currentTime, 2.0f)) + from;
			else if (currentTime < 2.0f / 2.75f)
				return (to - from) * (7.5625f * (currentTime -= 1.5f / 2.75f) * currentTime + 0.75f) + from;
			else if (currentTime < 2.5f / 2.75f)
				return (to - from) * (7.5625f * (currentTime -= 2.25f / 2.75f) * currentTime + 0.9375f) + from;
			return (to - from) * (7.5625f * (currentTime -= 2.625f / 2.75f) * currentTime + 0.984375f) + from;
		}

		public static float EaseInBounce(float currentTime, float totalTime, float from, float to)
		{
			return to - from - EaseOutBounce(totalTime - currentTime, totalTime, 0, to - from) + from;
		}

		public static float EaseInOutBounce(float currentTime, float totalTime, float from, float to)
		{
			if (currentTime < totalTime / 2f)
				return EaseInBounce(currentTime * 2f, totalTime, 0f, to - from) * 0.5f + from;
			return EaseOutBounce(currentTime * 2f - totalTime, totalTime, 0f, to - from) * 0.5f + from +
			       (to - from) * 0.5f;
		}

		public static float EaseInElastic(float currentTime, float totalTime, float from, float to, float a = 0f,
			float p = 0f)
		{
			if (currentTime == 0f)
				return from;
			currentTime /= totalTime;
			if (currentTime == 1f)
				return from + (to - from);
			if (p == 0f)
				p = totalTime * 0.3f;
			float s;
			if (a == 0f || a < Mathf.Abs(to - from))
			{
				a = to - from;
				s = p / 4.0f;
			}
			else
			{
				s = p / Mathf.PI * 2f * Mathf.Asin((to - from) / a);
			}

			currentTime -= 1;
			return -(a * Mathf.Pow(2f, 10f * currentTime) *
			         Mathf.Sin((currentTime * totalTime - s) * Mathf.PI * 2f / p)) +
			       from;
		}

		public static float EaseOutElastic(float currentTime, float totalTime, float from, float to, float a = 0,
			float p = 0)
		{
			if (currentTime == 0f)
				return from;
			currentTime /= totalTime;
			if (currentTime == 1f)
				return from + (to - from);
			if (p == 0f)
				p = totalTime * 0.3f;
			float s;
			if (a == 0f || a < Mathf.Abs(to - from))
			{
				a = to - from;
				s = p / 4.0f;
			}
			else
			{
				s = p / Mathf.PI * 2f * Mathf.Asin((to - from) / a);
			}

			return a * Mathf.Pow(2f, -10f * currentTime) *
			       Mathf.Sin((currentTime * totalTime - s) * Mathf.PI * 2f / p) +
			       (to - from) + from;
		}

		public static float EaseInOutElastic(float currentTime, float totalTime, float from, float to, float a = 0,
			float p = 0)
		{
			if (currentTime == 0f)
				return from;
			currentTime /= totalTime / 2;
			if (currentTime == 2f)
				return from + (to - from);
			if (p == 0f)
				p = totalTime * (0.3f * 1.5f);
			float s;
			if (a == 0f || a < Mathf.Abs(to - from))
			{
				a = to - from;
				s = p / 4.0f;
			}
			else
			{
				s = p / Mathf.PI * 2f * Mathf.Asin((to - from) / a);
			}

			if (currentTime < 1f)
			{
				currentTime -= 1f;
				return -0.5f * (a * Mathf.Pow(2f, 10f * currentTime) *
				                Mathf.Sin((currentTime * totalTime - s) * Mathf.PI * 2f / p)) + from;
			}

			currentTime -= 1f;
			return a * Mathf.Pow(2f, -10f * currentTime) *
			       Mathf.Sin((currentTime * totalTime - s) * Mathf.PI * 2f / p) *
			       0.5f + (to - from) +
			       from;
		}
	}
}