﻿namespace King.Service.Data
{
    using King.Azure.Data;
    using King.Service.Timing;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Dequeue Batch Task for Queues
    /// </summary>
    public class DequeueBatchDynamic<T> : DequeueBatch<T>
    {
        #region Members
        /// <summary>
        /// Tracking
        /// </summary>
        protected readonly ITimingTracker tracker;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="processor">Processor</param>
        /// <param name="poller">Poller</param>
        /// <param name="tracker">Tracker</param>
        /// <param name="minimumPeriodInSeconds">Minimum, time in seconds</param>
        /// <param name="maximumPeriodInSeconds">Maximum, time in seconds</param>
        public DequeueBatchDynamic(IPoller<T> poller, IProcessor<T> processor, ITimingTracker tracker, int minimumPeriodInSeconds = BaseTimes.MinimumStorageTiming, int maximumPeriodInSeconds = BaseTimes.MaximumStorageTiming)
            : base(poller, processor, 1, minimumPeriodInSeconds, maximumPeriodInSeconds)
        {
            if (null == tracker)
            {
                throw new ArgumentNullException("tracker");
            }

            this.tracker = tracker;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Processing Method
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Process Task</returns>
        protected override async Task<bool> Process(T data)
        {
            var timing = new Stopwatch();
            timing.Start();
            
            var result = await this.processor.Process(data);

            timing.Stop();
            
            this.tracker.Digest(TimeSpan.FromTicks(timing.ElapsedTicks));

            return result;
        }

        /// <summary>
        /// Sets Batch Completed
        /// </summary>
        protected override void RunCompleted()
        {
            this.batchCount = this.tracker.BatchSize();
        }
        #endregion
    }
}