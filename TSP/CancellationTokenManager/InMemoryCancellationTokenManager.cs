using System.Collections.Concurrent;
using System.Threading;

namespace TSP.CancellationTokenManager
{
    /// <summary>
    /// An in-memory implementation of <see cref="ICancellationTokenManager{T}"/>.
    /// Uses a <see cref="ConcurrentDictionary{TKey, TValue}"/> for storing key / values.
    /// </summary>
    /// <typeparam name="T">The type of key. Must implement <see cref="IEquatable{T}"/> for key comparisons.</typeparam>
    public class InMemoryCancellationTokenManager<T> : ICancellationTokenManager<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Concurrent dictionary which will store the key and a <see cref="CancellationTokenSource"/>
        /// </summary>
        private readonly ConcurrentDictionary<T, CancellationTokenSource> TokenDictionary;
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCancellationTokenManager{T}"/> class with an empty dictionary.
        /// </summary>
        public InMemoryCancellationTokenManager()
        {
            TokenDictionary = new ConcurrentDictionary<T, CancellationTokenSource>();
        }
        /// <inheritdoc/>
        /// <exception cref="Exception">Thrown when the key is already in use.</exception>
        public CancellationToken CreateCancellationToken(T key)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            if (!TokenDictionary.TryAdd(key, tokenSource))
            {
                throw new Exception("Key is already in use, token cannot be created.");
            }
            return tokenSource.Token;
        }
        /// <inheritdoc/>
        public bool KeySaved(T key)
        {
            return TokenDictionary.ContainsKey(key);
        }
        /// <inheritdoc/>
        /// <exception cref="Exception">Thrown when the key is not being in use.</exception>
        public void Signal(T key)
        {
            if (!KeySaved(key))
            {
                throw new Exception("Key is not being in use.");
            }
            TokenDictionary[key].Cancel();
        }
        /// <inheritdoc/>
        /// <exception cref="Exception">Thrown when the key is not being in use.</exception>
        public void RemoveCancellationToken(T key)
        {
            CancellationTokenSource? removedTokenSource;
            if (TokenDictionary.TryRemove(key, out removedTokenSource))
            {
                if (removedTokenSource != null)
                {
                    removedTokenSource.Dispose();
                }
            }
            else
            {
                throw new Exception("Key is not in use.");
            }
        }
    }
}
