namespace TSP_Application.Models
{   
    /// <summary>
    /// Used for packing data about an operation that the server will send back to client-side.
    /// </summary>
    /// <typeparam name="T">Type of data that needs to be transferred.</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Specifies if the operation was finished successfully.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Data that needs to be transferred.
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// A message about a possible encountered error.
        /// </summary>
        public string? Error { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult{T}"/> class with specified data about an operation.
        /// </summary>
        /// <param name="Success">Specifies if the operation was finished successfully.</param>
        /// <param name="Data">Data that needs to be transferred.</param>
        /// <param name="Error">Error message (if an error exists).</param>
        public OperationResult(bool Success, T? Data, string? Error)
        {
            this.Success = Success;
            this.Data = Data;
            this.Error = Error;
        }
    }
}
