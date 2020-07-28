using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Gongulus.Utility
{
    public static class Ext
    {
        public static float GetDistanceTo(this Vector3 pos1, Vector3 pos2)
        {
            Vector3 heading;

            heading.x = pos1.x - pos2.x;
            heading.y = pos1.y - pos2.y;
            heading.z = pos1.z - pos2.z;

            var distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;

            return Mathf.Sqrt(distanceSquared);
        }

        public static float GetDistanceTo(this Transform t1, Transform t2)
        {
            return t1.position.GetDistanceTo(t2.position);
        }


        public static float GetDistanceTo(this Transform t, Vector3 v)
        {
            return t.position.GetDistanceTo(v);
        }

        public static float GetDistanceTo(this Vector3 v, Transform t)
        {
            return t.position.GetDistanceTo(v);
        }

        public static float GetDistanceTo(this GameObject go1, GameObject go2)
        {
            return go1.transform.position.GetDistanceTo(go2.transform.position);
        }

        public static float GetDistanceTo(this GameObject go1, Transform t)
        {
            return go1.transform.position.GetDistanceTo(t.position);
        }

        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("can't get random item: list is null or count == 0");
                return default(T);
            }

            // UnityEngine.Random.InitState(UnityEngine.Random.Range(-10000, 10000));
            return list[StaticRandom.Instance.Next(0, list.Count)];
        }

        public static T GetRandomItem<T>(this List<T> list, out int index)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("can't get random item: list is null or count == 0");
                index = 0;
                return default(T);
            }

            index = StaticRandom.Instance.Next(0, list.Count);
            return list[index];
        }

        public static int GetRandomIndex<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("can't get random item: list is null or count == 0");
                return -1;
            }

            return StaticRandom.Instance.Next(0, list.Count);
        }

        public static int GetNextIndex<T>(this List<T> list, int curIndex)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("Can't get next index: list is null or count is 0");
                return -1;
            }

            if (list.Count == 1)
            {
                return 0;
            }

            var nextIndex = curIndex + 1;
            return nextIndex > list.Count - 1 ? 0 : nextIndex;
        }



        public static int GetPreviousIndex<T>(this List<T> list, int curIndex)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("Can't get previous index: list is null or count is 0");
                return -1;
            }

            var nextIndex = curIndex - 1;
            return nextIndex < 0 ? list.Count - 1 : nextIndex;
        }

        public static T GetRandomItem<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                Debug.LogWarning("can't get random item: list is null or count == 0");
                return default(T);
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;

        public static T GetRandomEnum<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new System.Random().Next(v.Length));
        }

        public static T GetRandomEnum<T>(int excludeNum)
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new System.Random().Next(v.Length - excludeNum));
        }




        public static List<GameObject> FindGameObjectsInChildWithTag(this GameObject parent, string tag)
        {
            var t = parent.transform;
            var list = new List<GameObject>(8);

            for (int i = 0; i < t.childCount; i++)
            {
                var child = t.GetChild(i);

                if (child.CompareTag(tag))
                {
                    list.Add(child.gameObject);
                }

            }

            return list;
        }


        ///<summary>
        /// Return percernt of value
        ///</summary>
        public static float GetPercent(this float value, float desiredPercent) =>
            value / 100f * desiredPercent;

        public static float GetPercent2(this float value, float desiredPercent) =>
            value * 100f / desiredPercent;

        ///<summary>
        /// Return percernt of value
        ///</summary>
        public static float GetPercent(this int value, int desiredPercent) =>
            (float)value / 100f * (float)desiredPercent;

        public static int Replace<T>(this IList<T> source, T oldValue, T newValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var index = source.IndexOf(oldValue);
            if (index != -1)
                source[index] = newValue;
            return index;
        }


        public static Vector3 GetBezierPoint(Vector3 startP, Vector3 midP, Vector3 endP, float t)
        {
            return Vector3.Lerp(
                Vector3.Lerp(startP, midP, t),
                Vector3.Lerp(midP, endP, t),
                t
            );
        }


        ///<summary>
        /// Return random element from given probabilites
        ///</summary>
        public static int RollDice(this List<int> probabilities)
        {
            var total = 0f;

            probabilities.ForEach(probability => total += probability);

            var randomProbability = StaticRandom.Instance.Next(0, (int)total);

            for (int i = 0; i < probabilities.Count; i++)
                if (randomProbability < probabilities[i])
                    return i;
                else
                    randomProbability -= probabilities[i];
            return -1;
        }

        public static int RollDice(this List<float> probabilities)
        {
            var total = 0f;

            probabilities.ForEach(probability => total += probability);

            var randomProbability = (float)StaticRandom.Instance.Next(0, (int)total);

            for (int i = 0; i < probabilities.Count; i++)
                if (randomProbability < probabilities[i])
                    return i;
                else
                    randomProbability -= probabilities[i];
            return -1;
        }

        public static GameObject CreateTestCube(Color color)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(cube.GetComponent<BoxCollider>());

            cube.GetComponent<MeshRenderer>().material.color = color;
            return cube;
        }

        public static GameObject CreateTestCube(Color color, Vector3 scale)
        {
            var cube = CreateTestCube(color);

            cube.transform.localScale = scale;

            return cube;
        }

        public static List<GameObject> GetAllChilds(this GameObject go)
        {
            var transform = go.transform;
            var list = new List<GameObject>(transform.childCount);

            for (int i = 0; i < transform.childCount; i++)
            {
                list.Add(transform.GetChild(i).gameObject);
            }

            return list;
        }
    }

    public static class IndexGiver
    {
        static int index;

        public static int GetIndex()
        {
            var prevIndex = index;
            index++;

            return prevIndex;
        }
    }
}