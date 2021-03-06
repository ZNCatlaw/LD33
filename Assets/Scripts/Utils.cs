﻿using UnityEngine;
using System.Collections;

namespace Utils
{
    public static class Math
    {
        public static float RoundTo(float f, int precision)
        {
            int multiplier = 10 * precision;
            return Mathf.Round(f * multiplier) / multiplier;
        }

        public static float RoundToPixel(float f, int pixelsPerUnit)
        {
            return Mathf.Round(f * pixelsPerUnit) / pixelsPerUnit;
        }
    }

    public static class Cast
    {
        public static Vector3 V2toV3(Vector2 f)
        {
            return new Vector3(f.x, f.y);
        }
    }

    public static class Sound
    {
        public enum Fade { In, Out };

        public static IEnumerator FadeAudio(AudioSource audio, float duration, Fade fadeType, Interpolate.EaseType easeType = Interpolate.EaseType.EaseOutExpo)
        {
            float start = fadeType == Fade.In ? 0.0f : 1.0f;
            float distance = fadeType == Fade.In ? 1.0f : -1.0f;
            float t = 0.0f;
            var easeFunction = Interpolate.Ease(easeType);

            while (t <= duration)
            {
                audio.volume = easeFunction(start, distance, t, duration);
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}