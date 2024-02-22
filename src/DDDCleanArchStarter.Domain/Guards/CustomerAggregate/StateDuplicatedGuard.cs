using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class StateGuardExtensions
    {
        public static void DuplicateState(this IGuardClause guardClause, IEnumerable<State> existingStates, State newState, string parameterName)
        {
            if (existingStates.Any(a => a.StateId == newState.StateId))
            {
                throw new DuplicateStateException("Cannot add duplicate state.", parameterName);
            }
        }
    }
}
