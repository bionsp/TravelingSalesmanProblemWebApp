using TSP.Domain;

namespace TSP.Solver
{
    /// <summary>
    /// An implementation of <see cref="ITspStrategy"/> which uses a genetic evolutive algorithm.
    /// </summary>
    public class TspGeneticEvolution : ITspStrategy
    {
        /// <summary>
        /// Parameters used by this algorithm.
        /// </summary>
        public TspGeneticEvolutionParameters Parameters { get; set; }
        /// <inheritdoc/>
        ITspStrategyParameters ITspStrategy.Parameters
        {
            get { return Parameters; }
            set { Parameters = (TspGeneticEvolutionParameters)value; }
        }
        /// <summary>
        /// A collection of objects of type <see cref="MapPath"/> which represents the current population within the genetic algorithm.
        /// It will be shared state among different internal functions.
        /// </summary>
        private List<MapPath> Population;
        /// <summary>
        /// Initializes a new instance of the <see cref="TspGeneticEvolution"/> class with an empty set of parameters and an empty population.
        /// </summary>
        public TspGeneticEvolution()
        {
            Parameters = new TspGeneticEvolutionParameters();
            Population = new List<MapPath>();
        }
        /// <summary>
        /// Generates randomly a number of objects of type <see cref="MapPath"/> for the given <see cref="Map"/> equal to the set population size in the set of parameters.
        /// </summary>
        /// <param name="map"><see cref="Map"/> for which a number of objects of type <see cref="MapPath"/> should be randomly generated.</param>
        private void InitializePopulation(Map map)
        {
            // Create an empty collection of individuals.
            Population = new List<MapPath>();
            for (int i = 0; i < Parameters.PopulationSize; i++)
            {
                // Adds an individual equal to the provided Map.
                Population.Add(new MapPath(map.Points));
                // Randomly shuffles the recently added MapPath.
                Population[Population.Count - 1].Shuffle();
                // Computes the cost of the recently added MapPath.
                Population[Population.Count - 1].ManualCost = GetCost(Population[Population.Count - 1]);
            }
        }
        /// <summary>
        /// Creates a new <see cref="MapPath"/> which inherits the characteristics of two given objects (parents) of type <see cref="MapPath"/>.
        /// </summary>
        /// <param name="parent1">Parent 1.</param>
        /// <param name="parent2">Parent 2.</param>
        /// <returns>A new <see cref="MapPath"/> which inherits the properties from the given parents.</returns>
        private MapPath Crossover(MapPath parent1, MapPath parent2)
        {
            // Creates an empty list of points.
            List<Point?> points = new List<Point?>();
            for (int i = 0; i < parent1.Points.Count; i++)
            {
                points.Add(null);
            }
            // Randomly chooses a starting position and an ending position.
            // These positions represent the portion that the offspring will inherit from the first parent.
            Random random = new Random();
            int start = random.Next(0, points.Count - 1);
            int end = random.Next(start + 1, points.Count);
            // HashSet used for marking added points from the first parent.
            HashSet<Point> added = new HashSet<Point>();
            for (int i = start; i <= end; i++)
            {
                // Adds the marked points.
                points[i] = parent1.Points[i];
                // Remember the added points.
                added.Add(points[i]!);
            }
            // Variable which keeps track of the current position (indicates the current point) in the resulting offspring.
            int k = 0;
            // Iterate through the second parent's points and add the ones that weren't added from the first parent in positions outside of the inherited portion from the first parent.
            for (int i = 0; i < parent2.Points.Count; i++)
            {
                // If there was a point already added on this position for the offspring, move to the next one.
                while (k < points.Count() && points[k] != null)
                {
                    k++;
                }
                // Stopped at a position where the offspring doesn't have a point set.
                // If at the current iteration, the point from the second parent hasn't been inherited from the first parent, add it at this position for the offspring.
                if (!added.Contains(parent2.Points[i]))
                {
                    points[k] = parent2.Points[i];
                }
            }
            // Create a MapPath from the constructed list.
            MapPath offspring = new MapPath(points!);
            // Calculate its cost.
            offspring.ManualCost = GetCost(offspring);
            return offspring;
        }
        /// <summary>
        /// Represents the stage where crossovers are made in the population.
        /// </summary>
        /// <exception cref="Exception">Thrown when parameters for 'PopulationSize' or 'CrossoverScale' are null.</exception>
        private void Crossovers()
        {
            // Throws an exceptions if at least one of the mentioned parameters equal to null.
            if (Parameters.PopulationSize == null)
            {
                throw new Exception("Parameter 'PopulationSize' is null.");
            }
            if (Parameters.CrossoverScale == null)
            {
                throw new Exception("Parameter 'CrossoverScale' is null.");
            }
            // Calculate the number of crossovers by using the set 'PopulationSize' and 'CrossoverScale'.
            int numOfCrossovers = (int)((((Parameters.PopulationSize - 1) * Parameters.PopulationSize) / 2) * Parameters.CrossoverScale);
            Random random = new Random();
            for (int i = 0; i < numOfCrossovers; i++)
            {
                // For each crossover, there will be a tournament designed to select two parents.
                MapPath? winner1 = null;
                MapPath? winner2 = null;
                // There will be 'TournamentSize' candidates for each parent spot.
                // They will be randomly selected from the population, the ones with the minimal cost will win.
                for (int j = 0; j < Parameters.TournamentSize; j++)
                {
                    MapPath candidate1 = Population[random.Next(0, Population.Count)];
                    MapPath candidate2 = Population[random.Next(0, Population.Count)];
                    if (winner1 == null || candidate1.ManualCost < winner1.ManualCost)
                    {
                        winner1 = candidate1;
                    }
                    if (winner2 == null || candidate2.ManualCost < winner2.ManualCost)
                    {
                        winner2 = candidate1;
                    }
                }
                // Add the resulted offspring to the population.
                Population.Add(Crossover(winner1!, winner2!));
            }
        }
        /// <summary>
        /// Mutates a given <see cref="MapPath"/> by shuffling the position of the points of a random portion.
        /// Uses a variation of the Fisher-Yates Shuffle algorithm. 
        /// </summary>
        /// <param name="path">Given <see cref="MapPath"/> for which a mutation is wanted.</param>
        /// <returns>A mutated version of the given <see cref="MapPath"/>.</returns>
        private MapPath Mutate(MapPath path)
        {
            // Get some random starting and ending points which will represent a portion.
            Random random = new Random();
            int start = random.Next(0, path.Points.Count - 1);
            int end = random.Next(start, path.Points.Count);
            // Shuffle that portion by using a variation of the Fisher-Yates Shuffle algorithm.
            for (int i = end; i >= start; i--)
            {
                int k = random.Next(start, i + 1);
                Point aux = path.Points[i];
                path.Points[i] = path.Points[k];
                path.Points[k] = aux;
            }
            path.ManualCost = GetCost(path);
            return path;
        }
        /// <summary>
        /// Represents the mutation stage.
        /// </summary>
        /// <exception cref="Exception"> Thrown when parameters for 'PopulationSize' or 'CrossoverScale' are null.</exception>
        private void Mutations()
        {
            // Throws an exceptions if at least one of the mentioned parameters equal to null.
            if (Parameters.PopulationSize == null)
            {
                throw new Exception("Parameter 'PopulationSize' is null.");
            }
            if (Parameters.MutationScale == null)
            {
                throw new Exception("Parameter 'CrossoverScale' is null.");
            }
            // Calculate the number of mutations by using the set 'PopulationSize' and 'MutationScale'.
            int numOfMutations = (int)(Parameters.PopulationSize * Parameters.MutationScale);
            Random random = new Random();
            for (int i = 0; i < numOfMutations; i++)
            {
                // Add a mutated version of a random individual in the population.
                Population.Add(Mutate(Population[random.Next(0, Population.Count)]));
            }
        }
        /// <summary>
        /// Reduces the excess individuals, after population is increased by the crossover and mutation stages to <see cref="TspGeneticEvolutionParameters.PopulationSize"/>.
        /// Only the fittest individuals will remain.
        /// </summary>
        private void NaturalSelection()
        {
            // Sort in non-descending order the individuals by cost.
            Population.Sort((path1, path2) => GetCost(path1).CompareTo(GetCost(path2)));
            List<MapPath> newPopulation = new List<MapPath>();
            // Add only the first 'PopulationSize' individuals, since they are sorted in non-descending order by cost.
            for (int i = 0; i < Parameters.PopulationSize; i++)
            {
                newPopulation.Add(Population[i]);
            }
            // Replace the old population with the new one
            Population = newPopulation;
        }
        /// <summary>
        /// An epoch represents a crossover stage, followed by a mutation stage and ends with a natural selection stage.
        /// </summary>
        private void Epoch()
        {
            Crossovers();
            Mutations();
            NaturalSelection();
        }
        /// <inheritdoc/>
        public MapPath GetPath(Map map, CancellationToken token)
        {
            // Initialize the population for the given Map.
            InitializePopulation(map);
            for (int i = 0; i < Parameters.EpochCount; i++)
            {
                Epoch();
                // Throw OperationCanceledException Cancellation is signaled.
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException("Operation canceled.");
                }
            }
            // Return the individual with the lowest cost.
            return Population[0];
        }
        /// <inheritdoc/>
        public double GetCost(MapPath path)
        {
            double cost = 0;
            Point? last = null;
            foreach (Point point in path.Points)
            {
                if (last != null)
                {
                    cost += last.CostTo(point);
                }
                last = point;
            }
            cost += path.Points[0].CostTo(path.Points[path.Points.Count - 1]);
            return cost;
        }
    }
}