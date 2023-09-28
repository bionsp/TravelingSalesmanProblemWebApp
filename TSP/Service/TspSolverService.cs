using TSP.Domain;
using TSP.Solver;

namespace TSP.Service
{
    /// <summary>
    /// An implementation of <see cref="ITspSolverService"/>.
    /// Uses internally an <see cref="ITspStrategy"/> in order to compute an answer.
    /// </summary>
    public class TspSolverService : ITspSolverService
    {
        /// <summary>
        /// A TSP solver used for computing the answer.
        /// </summary>
        private ITspStrategy TspSolverStrategy;
        /// <summary>
        /// Initializes a new instance of the <see cref="TspSolverService"/> with the provided <see cref="ITspStrategy"/>.
        /// </summary>
        /// <param name="tspSolverStrategy">An instance of <see cref="ITspStrategy"/> which will be used to compute an answer.</param>
        public TspSolverService(ITspStrategy tspSolverStrategy)
        {
            TspSolverStrategy = tspSolverStrategy;
        }
        /// <inheritdoc/>
        public string GetPath(Map map, ITspStrategyParameters parameters, CancellationToken token)
        {
            TspSolverStrategy.Parameters = parameters;
            MapPath path = TspSolverStrategy.GetPath(map, token);
            // Creates a string of the computed path.
            string ret = "";
            ret += "Cost: " + path.ManualCost.ToString() + "\n";
            for (int i = 0; i < path.Points.Count; i++)
            {
                ret += path.Points[i].X.ToString() + " " + path.Points[i].Y.ToString();
                if(i !=  path.Points.Count - 1)
                {
                    ret += "\n";
                }
            }
            return ret;
        }
    }
}
