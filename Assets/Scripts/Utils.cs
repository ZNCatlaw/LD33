using UnityEngine;

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
}