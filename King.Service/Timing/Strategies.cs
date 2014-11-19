﻿namespace King.Service.Timing
{
    using System;

    public static class Strategies
    {
        /// <summary>
        /// Get Calculate Timing
        /// </summary>
        /// <param name="strategy">Strategy</param>
        /// <param name="minimumPeriodInSeconds">Minimum Period In Seconds</param>
        /// <param name="maximumPeriodInSeconds">Maximum Period In Seconds</param>
        /// <returns></returns>
        public static ICalculateTiming Get(Strategy strategy, int minimumPeriodInSeconds, int maximumPeriodInSeconds)
        {
            switch (strategy)
            {
                case Strategy.Exponential:
                    return new ExponentialTiming(minimumPeriodInSeconds, maximumPeriodInSeconds);
                case Strategy.Linear:
                    return new LinearTiming(minimumPeriodInSeconds, maximumPeriodInSeconds);
                default:
                    throw new InvalidOperationException("Unknown timing strategy.");
            }
        }

        public static IDynamicTiming Adaptive(Strategy strategy, int minimumPeriodInSeconds, int maximumPeriodInSeconds)
        {
            return new AdaptiveTiming(Strategies.Get(strategy, minimumPeriodInSeconds, maximumPeriodInSeconds));
        }

        public static IDynamicTiming Backoff(Strategy strategy, int minimumPeriodInSeconds, int maximumPeriodInSeconds)
        {
            return new BackoffTiming(Strategies.Get(strategy, minimumPeriodInSeconds, maximumPeriodInSeconds));
        }
    }
}