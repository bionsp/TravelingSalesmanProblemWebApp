namespace TSP.Solver
{
    /// <summary>
    /// Parameters on which the genetic evolution algorithm depends.
    /// </summary>
    public class TspGeneticEvolutionParameters : ITspStrategyParameters
    {
        /// <summary>
        /// Number of iterations that the algorithm will run.
        /// </summary>
        public int? EpochCount { get; set; }
        /// <summary>
        /// The number of individuals that will be present in the population at each iteration.
        /// </summary>
        public int? PopulationSize { get; set; }
        /// <summary>
        /// The number of candidates in the population that could be selected as one of the parents when creating an offspring.
        /// </summary>
        public int? TournamentSize { get; set; }
        /// <summary>
        /// Represents how many offspring should be generated from a scale from 0 to 1 at the end of each iteration.
        /// Value 0 indicates that there will be no offspring.
        /// Value 1 indicates that there will be C(<see cref="PopulationSize"/>, 2) offspring.
        /// </summary>
        public double? CrossoverScale { get; set; }
        /// <summary>
        /// Represents how many mutations should be generated from a scale from 0 to 1.
        /// Value 0 indicated that there will be no mutations.
        /// Value 1 indicates that there will be <see cref="PopulationSize"/> mutations.
        /// </summary>
        public double? MutationScale { get; set; }
        /// <summary>
        /// Checks if there is at least one member which is equal to null;
        /// </summary>
        /// <returns>True if at least one of the members is equal to null, false otherwise.</returns>
        public Boolean HasNulls()
        {
            return EpochCount == null || PopulationSize == null || TournamentSize == null || CrossoverScale == null || MutationScale == null;
        }
    }
}
