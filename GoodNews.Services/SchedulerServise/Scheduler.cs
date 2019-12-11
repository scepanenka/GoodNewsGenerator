using System;
using System.Threading.Tasks;
using GoodNews.Core;

namespace SchedulerServise
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
            await _parser.Parse();
        }
    }
}