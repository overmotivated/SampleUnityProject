using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Gongulus
{

    public static class Get
    {
        static Dictionary<float, WaitForSeconds> delays = new Dictionary<float, WaitForSeconds>(100);
        public static WaitForEndOfFrame EndOfFrame { get; } = new WaitForEndOfFrame();
        public static WaitForFixedUpdate FixedUpdate { get; } = new WaitForFixedUpdate();

        public static IObservable<long> Delay(float delay) => Observable.Interval(TimeSpan.FromSeconds(delay)).Take(1);

        public static WaitForSeconds DelayInSeconds(float delay)
        {
            if (delays.TryGetValue(delay, out var value))
                return value;

            var newDelay = new WaitForSeconds(delay);
            delays.Add(delay, newDelay);

            return newDelay;
        }
    }
}