﻿namespace King.Azure.BackgroundWorker
{
    using System;

    /// <summary>
    /// Timing Maths
    /// </summary>
    public class Timing : ITiming
    {
        #region Methods
        /// <summary>
        /// Exponential Backoff strategy, within bounds
        /// </summary>
        /// <param name="attempts">attempts</param>
        /// <param name="max">upper bound</param>
        /// <param name="min">lower bound</param>
        /// <returns>timing</returns>
        public double Exponential(ulong attempts, int max, int min = 1)
        {
            if (0 == attempts)
            {
                return min;
            }

            var percent = Math.Pow(2, attempts) * .1d;
            var current = (percent * min) + min;
            return current < max ? current : max;
        }
        #endregion
    }
}