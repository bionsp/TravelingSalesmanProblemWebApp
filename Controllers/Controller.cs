using Microsoft.AspNetCore.Mvc;
using TSP.Service;
using TSP.CancellationTokenManager;
using TSP.InputVerification;
using TSP.Solver;
using TSP_Application.Models;
using System.Configuration;

namespace TSP_Application.Controllers
{
    /// <summary>
    /// Controller used by the TSP Application.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        /// <summary>
        /// A cancellation token manager used for organizing new / existing requests.
        /// </summary>
        private readonly ICancellationTokenManager<string> CancellationTokenManager;
        /// <summary>
        /// Maximum number of nodes that a file can contain in a request.
        /// </summary>
        private int MaxNodesAllowed;
        /// <summary>
        /// Creates a new instance of <see cref="Controller"/>.
        /// </summary>
        /// <param name="CancellationTokenManager">A cancellation token manager required for organizing requests.</param>
        public Controller(InMemoryCancellationTokenManager<string> CancellationTokenManager, IConfiguration configuration)
        {
            this.CancellationTokenManager = CancellationTokenManager;
            if (!int.TryParse(configuration["AppSettings:MaxNodesAllowed"], out MaxNodesAllowed))
            {
                throw new ConfigurationErrorsException("Invalid MaxNodesAllowed value in 'appsettings.json.'");
            }
        }
        /// <summary>
        /// Computes an answer for the TSP problem with given inputs.
        /// </summary>
        /// <param name="requestParameters">Inputs submitted by client-side.</param>
        /// <returns>The answer to the TSP problem for the given inputs.</returns>
        /// <exception cref="Exception">Throws exceptions in case of invalid inputs / failed attempts to initiate the computation.</exception>
        [HttpPost("genetic-algorithm-computation-request")]
        public async Task<OperationResult<string>> GeneticAlgorithmComputation([FromForm] UserInputGeneticEvolution requestParameters)
        {
            Console.WriteLine(MaxNodesAllowed);
            // Invalid uploaded text file.
            if (requestParameters.TextFile == null)
            {
                throw new Exception("Null file.");
            }
            // Invalid parameters.
            if(requestParameters.TspParameters == null || requestParameters.TspParameters.HasNulls() == true)
            {
                throw new Exception("Null parameters.");
            }
            // Text file content has wrong format.
            if (await FormatChecker.Instance.SimpleXY(requestParameters.TextFile, MaxNodesAllowed) == false) { 
                throw new Exception("Wrong format.");
            }
            // Key for identifying an user, for now we will use the user's IP address.
            // !!! Will be changed in the future !!!
            string? key = HttpContext.Connection.RemoteIpAddress?.ToString();
            // Invalid IP address.
            if (key == null)
            {
                return new OperationResult<string>(false, null, "Invalid IP Address.");
            }
            try
            {
                CancellationToken token = CancellationTokenManager.CreateCancellationToken(key);
                try
                {
                    ITspSolverService service = new TspSolverService(new TspGeneticEvolution());
                    // Computes a path.
                    string path = service.GetPath(await Converter.Instance.SimpleXY(requestParameters.TextFile), requestParameters.TspParameters, token);
                    // Gets rids of CancellationTokenSource for our key once operation is finished.
                    CancellationTokenManager.RemoveCancellationToken(key);
                    return new OperationResult<string>(true, path, null);
                }
                catch (OperationCanceledException)
                {
                    // Gets rids of CancellationTokenSource for our key once operation is canceled.
                    CancellationTokenManager.RemoveCancellationToken(key);
                    return new OperationResult<string>(true, "Operation canceled.", null);
                }
                catch (Exception ex)
                {
                    return new OperationResult<string>(false, "", ex.Message);
                }
            }
            catch(Exception ex)
            {
                // If an attempt is made to create a CancellationTokenSource with a key that is already in-use, this will signal the canceling of the on-going operation that uses the said key.
                CancellationTokenManager.Signal(key);
                return new OperationResult<string>(true, "Waiting for the operation to be cancelled...", ex.Message);
            }
        }
        /// <summary>
        /// Used for retrieving maximum number of nodes.
        /// </summary>
        /// <returns>Maximum number of nodes (lines allowed) according to the configuration file.</returns>
        [HttpGet("max-line-count")]
        public int GetMaximumLineCount()
        {
            return MaxNodesAllowed;
        }
    }
}