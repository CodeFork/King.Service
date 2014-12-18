﻿namespace King.Service.Data
{
    using King.Azure.Data;
    using King.Service.Data.Model;
    using King.Service.Scalability;
    using King.Service.Timing;
    using System.Collections.Generic;

    /// <summary>
    /// Storage Queue AutoScaler
    /// </summary>
    /// <typeparam name="T">Processor Type</typeparam>
    public class StorageQueueAutoScaler<T> : QueueAutoScaler<IQueueSetup>
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="count">Count</param>
        /// <param name="messagesPerScaleUnit">Messages Per-Scale Unit</param>
        /// <param name="setup">Setup</param>
        /// <param name="minimum">Minimum Scale</param>
        /// <param name="maximum">Maximmum Scale</param>
        /// <param name="checkScaleInMinutes">Check Scale Every</param>
        public StorageQueueAutoScaler(IQueueCount count, IQueueSetup setup, ushort messagesPerScaleUnit = QueueScaler<T>.MessagesPerScaleUnitDefault, byte minimum = 1, byte maximum = 2, byte checkScaleInMinutes = BaseTimes.ScaleCheck)
            : base(count, messagesPerScaleUnit, setup, minimum, maximum, checkScaleInMinutes)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Scale Unit
        /// </summary>
        /// <param name="setup">Setup</param>
        /// <returns>Scalable Task</returns>
        public override IEnumerable<IScalable> ScaleUnit(IQueueSetup setup)
        {
            var processor = setup.Get<T>();
            var minimumPeriodInSeconds = BaseTimes.MinimumStorageTiming;
            var maximumPeriodInSeconds = BaseTimes.MaximumStorageTiming;
            switch (setup.Priority)
            {
                case QueuePriority.High:
                    minimumPeriodInSeconds = 1;
                    maximumPeriodInSeconds = BaseTimes.MinimumStorageTiming;
                    break;
                case QueuePriority.Medium:
                    minimumPeriodInSeconds /= 2;
                    maximumPeriodInSeconds /= 2;
                    break;
            }

            var dequeue = new StorageDequeueBatchDynamic<T>(setup.Name, setup.ConnectionString, processor, minimumPeriodInSeconds, maximumPeriodInSeconds);

            switch (setup.Priority)
            {
                case QueuePriority.High:
                    yield return new BackoffRunner(dequeue, Strategy.Linear);
                    break;
                case QueuePriority.Medium:
                    yield return new BackoffRunner(dequeue, Strategy.Exponential);
                    break;
                default:
                    yield return new AdaptiveRunner(dequeue);
                    break;
            }
        }
        #endregion
    }
}