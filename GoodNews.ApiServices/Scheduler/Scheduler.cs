using System;
using System.Threading.Tasks;
using GoodNews.Core;
using Hangfire;

namespace GoodNews.ApiServices.Scheduler
{
    public class Scheduler : IScheduler
    {
        private readonly IParser _parser;

        public Scheduler(IParser parser)
        {
            _parser = parser;
        }
        public async Task Run()
        {
            await parseOnliner();
           // BackgroundJob.ContinueWith(context.BackgroundJob.Id, () => ChildFunc(args));
        }

        private async Task parseOnliner()
        {
            await _parser.Parse(@"https://people.onliner.by/feed");
        }
    }
}
