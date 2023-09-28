namespace TSP.CancellationTokenManager
{
    /// <summary>
    /// Provides operations of a <see cref="CancellationToken"/> for a provided key.
    /// </summary>
    /// <typeparam name="T">Type of key.</typeparam>
    public interface ICancellationTokenManager<T>
    {
        /// <summary>
        /// Creates a <see cref="CancellationToken"/> for the provided key.
        /// </summary>
        /// <param name="key">Key for which the <see cref="CancellationToken"/> will be created.</param>
        /// <returns><see cref="CancellationToken"/> for the provided key.</returns>
        CancellationToken CreateCancellationToken(T key);
        /// <summary>
        /// Checks if a <see cref="CancellationToken"/> for the provided key.
        /// </summary>
        /// <param name="key">Key for which the check is needed.</param>
        /// <returns>True if there exists a <see cref="CancellationToken"/> for this specific key, false otherwise.</returns>
        bool KeySaved(T key);
        /// <summary>
        /// Signals the existing <see cref="CancellationToken"/> for the key provided.
        /// </summary>
        /// <param name="key">Key for which the associated <see cref="CancellationToken"/> will be signaled.</param>
        void Signal(T key);
        /// <summary>
        /// Removes the association between the given key and its respective <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="key">Key for which removing the association is needed.</param>
        void RemoveCancellationToken(T key);
    }
}
