using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    struct Vec2
    {
        public float X;
        public float Y;

        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y); } }

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        //constants

        /// <summary>
        /// zero vector
        /// </summary>
        public static Vec2 ZERO { get { return new Vec2(0, 0); } }
        /// <summary>
        /// Vector that points to the back
        /// </summary>
        public static Vec2 BACK { get { return new Vec2(0, -1); } }
        /// <summary>
        /// Vector that points to the front
        /// </summary>
        public static Vec2 FRONT { get { return new Vec2(0, 1); } }
        /// <summary>
        /// Vector that points right
        /// </summary>
        public static Vec2 RIGHT { get { return new Vec2(1, 0); } }
        /// <summary>
        /// Vector that points left
        /// </summary>
        public static Vec2 LEFT { get { return new Vec2(-1, 0); } }

        public Vec2(Vec2 v) : this(v.X, v.Y) { }

        public static implicit operator SFML.Window.Vector2f(Vec2 v) { return new SFML.Window.Vector2f(v.X, v.Y); }
        public static implicit operator Vec2(SFML.Window.Vector2f v) { return new Vec2(v.X, v.Y); }
        public static Vec2 operator +(Vec2 v1, Vec2 v2) { return new Vec2(v1.X + v2.X, v1.Y + v2.Y); }
        public static Vec2 operator -(Vec2 v1, Vec2 v2) { return new Vec2(v1.X - v2.X, v1.Y - v1.Y); }
        public static Vec2 operator *(float t, Vec2 v) { return new Vec2(t * v.X, t * v.Y); }
        public static Vec2 operator *(Vec2 v, float t) { return new Vec2(t * v.X, t * v.Y); }
        public static Vec2 operator /(Vec2 v, float t) { return new Vec2(v.X / t, v.Y / t); }
        public static bool operator ==(Vec2 v1, Vec2 v2) { return v1.X == v2.X && v1.Y == v2.Y; }

        public void Normalize()
        {
            X /= Length;
            Y /= Length;
        }

        public Vec2 GetNormalized()
        {
            return this / Length;
        }

        public float dot(Vec2 v) { return X * v.X + Y * v.Y; }
    }
}
