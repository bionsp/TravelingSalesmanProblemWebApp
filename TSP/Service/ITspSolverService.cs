using TSP.Domain;
using TSP.Solver;

namespace TSP.Service
{
    /// <summary>
    /// Provides functionalities for solving the TSP problem.
    /// </summary>
    public interface ITspSolverService
    {
        /// <summary>
        /// Returns the optimal path computed as a string.
        /// </summary>
        /// <param name="map">A <see cref="Map"/> containing points for which the path is to be calculated.</param>
        /// <param name="parameters">Parameters required by the algorithm.</param>
        /// <param name="token">A <see cref="CancellationToken"/> which will stop the algorithm if needed.</param>
        /// <returns>The optimal path formatted as a string decided by the algorithm.</returns>
        string GetPath(Map map, ITspStrategyParameters parameters, CancellationToken token);
    }
}
