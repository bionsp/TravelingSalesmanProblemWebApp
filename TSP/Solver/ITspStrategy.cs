using TSP.Domain;

namespace TSP.Solver
{
    /// <summary>
    /// Interface which different type of solving strategies for the TSP problem will implement.
    /// </summary>
    public interface ITspStrategy
    {
        /// <summary>
        /// Parameters desired to be used for the solving strategy.
        /// </summary>
        ITspStrategyParameters Parameters { get; set; }
        /// <summary>
        /// Computes a possible answer for the given <see cref="Map"/>.
        /// </summary>
        /// <param name="map"><see cref="Map"/> for which a <see cref="MapPath"/> will be computed.</param>
        /// <param name="token"><see cref="CancellationToken"/> which can be checked periodically in order to stop an on-going computation.</param>
        /// <returns>A possible <see cref="MapPath"/> for the given <see cref="Map"/>.</returns>
        MapPath GetPath(Map map, CancellationToken token);
        /// <summary>
        /// This method will calculate the cost of a given possible <see cref="MapPath"/>.
        /// </summary>
        /// <param name="path"><see cref="MapPath"/> for which we want to calculate the cost.</param>
        /// <returns>The cost of the provided <see cref="MapPath"/>.</returns>
        double GetCost(MapPath path);
    }
}
