using UnityEngine;
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

        public static IEnumerator FadeAudio(AudioSource audio, float timer, Fade fadeType)
        {
            float start = fadeType == Fade.In ? 0.0F : 1.0F;
            float end = fadeType == Fade.In ? 1.0F : 0.0F;
            float i = 0.0F;
            float step = 1.0F / timer;

            while (i <= 1.0F)
            {
                i += step * Time.deltaTime;
                audio.volume = Mathf.Lerp(start, end, i);
                yield return new WaitForSeconds(step * Time.deltaTime);
            }
        }
    }
}