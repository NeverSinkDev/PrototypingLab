using System;
using System.Threading;
using System.Threading.Tasks;
using CommonModules.Attributes;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    public class PrimitiveThreadPoolDemo : AbstractModule
    {
        public override string Description => "Cancellation Token Playground";

        public CancellationTokenSource SingleCancelationSource { get; set; } = new CancellationTokenSource();

        public int CurrentIdent { get; set; } = 1;

        [InputField]
        public int TaskNumber { get; set; } = 2;

        [InputField]
        public int SleepDurationMS { get; set; } = 200;

        public object Lock { get; set; } = new Object();

        public string GetIdentifier
        {
            get
            {
                lock(Lock)
                {
                    return this.CurrentIdent++.ToString();
                }
            }
        }

        [ActionPerformer]
        public async Task ThreadPoolCreateTasksAsync()
        {

            CurrentIdent = 1;
            // Start work
            SingleCancelationSource = new CancellationTokenSource();
            for (int i = 1; i <= this.TaskNumber; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(WorkRunner), SingleCancelationSource.Token);
            }

            await Task.Delay(this.SleepDurationMS);
            SingleCancelationSource.Cancel();
            SingleCancelationSource.Dispose();
        }

        public void WorkRunner(object obj)
        {
            var id = this.GetIdentifier;

            CancellationToken token = (CancellationToken)obj;

            for (int i = 0; i < 100000; i++)
            {
                if (token.IsCancellationRequested)
                {
                    this.LogMessage($"ID {id}: In iteration {i}, cancellation has been requested...");
                    break;
                }

                this.LogMessage($"ID {id}: In iteration {i}, business as usual");
                // Simulate some work.
                Thread.SpinWait(5000000);
            }
        }
    }
}
