using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library
{
    public class StrategySet
    {
        public StrategySet(IEnumerable<Strategy> strategies)
        {
            this.Strategies = strategies;
        }

        public IEnumerable<Strategy> Strategies { get; }

        /// <summary>
        /// Returns all states of all stategies. Return values can repeat.
        /// </summary>
        public IEnumerable<Commands> States
            => this.Strategies.Select(strategy => strategy.State);

        /// <summary>
        /// Returns first strategy, that has 'command' as available state
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Strategy GetStrategyByCommand(Commands command)
        {
            // Is Commands.Wait a problem?
            return this.Strategies
                .FirstOrDefault(strategy =>
                                    strategy.AvailableStates.Contains(command));
        }
    }
}
