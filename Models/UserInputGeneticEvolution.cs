using TSP.Solver;

namespace TSP_Application.Models
{
    /// <summary>
    /// Represents the input data needed for running the genetic evolution algorithm.
    /// </summary>
    public class UserInputGeneticEvolution
    {
        /// <summary>
        /// A text file which will hold the collection of points sent from the client-side.
        /// </summary>
        public IFormFile? TextFile { get; set; }
        /// <summary>
        /// The parameters required for the genetic evolution algorithm sent from the client-side.
        /// </summary>
        public TspGeneticEvolutionParameters? TspParameters { get; set; }
    }
}
