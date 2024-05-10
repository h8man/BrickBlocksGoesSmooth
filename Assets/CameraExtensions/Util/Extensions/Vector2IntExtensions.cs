using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    public static class Vector2IntExtensions
    {
        public static Vector2 ToVector2(this Vector3 v)
        {
            return (Vector2)v;
        }
        public static Vector3 ToVector3(this Vector2 v)
        {
            return (Vector3)v;
        }
        public static Vector2 ToUnits(this Vector2Int v, float ppu)
        {
            return new Vector2(v.x/ppu, v.y/ppu);
        }
        public static Vector2 ToStep(this Vector2 v, float ppu)
        {
            return v.ToPixels(ppu).ToUnits(ppu);
        }
        public static Vector2Int ToPixels(this Vector2 v, float ppu)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x*ppu), Mathf.RoundToInt(v.y*ppu));
        }
        public static Vector2Int RoundToInt(this Vector2 v)
        {
            return Vector2Int.RoundToInt(v);
        }
        public static Vector3Int RoundToInt(this Vector3 v)
        {
            return Vector3Int.RoundToInt(v);
        }
    }
}
