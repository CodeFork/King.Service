﻿namespace King.Service.Tests
{

    public class AdaptiveHelper : AdaptiveTask
    {
        public bool Work
        {
            get;
            set;
        }
        public override void Run(out bool workWasDone)
        {
            workWasDone = this.Work;
        }
    }
}
