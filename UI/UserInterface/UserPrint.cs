using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public class UserPrint
    {
        public async void PrintTickInfo(object sender, TickInfoEventArgs e)
        {
            await Task.Run(() =>
            {

            });
        }
        public async void PrintDayInfo(object sender, DayInfoEventArgs e)
        {
            await Task.Run(() =>
            {

            });
        }
        public async void PrintSimulationSummary(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() =>
            {

            });
        }
    }
}
