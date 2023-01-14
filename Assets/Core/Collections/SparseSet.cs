using System.Collections;
using System.Collections.Generic;

namespace Core.Collections
{
    /// <summary>
    /// Represents an unordered sparse set of natural numbers, and provides constant-time operations on it.
    /// </summary>
    public sealed class SparseSet : IEnumerable<int>
    {
        private readonly int _max; // maximal value the set can contain

        // _max = 100; implies a range of [0..99]
        private int _n; // current size of the set
        private readonly int[] _d; // dense array
        private readonly int[] _s; // sparse array

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseSet"/> class.
        /// </summary>
        /// <param name="maxValue">The maximal value the set can contain.</param>
        public SparseSet(int maxValue)
        {
            this._max = maxValue + 1;
            this._n = 0;
            this._d = new int[this._max];
            this._s = new int[this._max];
        }

        /// <summary>
        /// Adds the given value.
        /// If the value already exists in the set it will be ignored.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(int value)
        {
            if (value >= 0 && value < this._max && !Contains(value))
            {
                this._d[this._n] = value; // insert new value in the dense array...
                this._s[value] = this._n; // ...and link it to the sparse array
                this._n++;
            }
        }

        /// <summary>
        /// Removes the given value in case it exists.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Remove(int value)
        {
            if (Contains(value))
            {
                this._d[this._s[value]] = this._d[this._n - 1]; // put the value at the end of the dense array
                // into the slot of the removed value
                this._s[this._d[this._n - 1]] = this._s[value]; // put the link to the removed value in the slot
                // of the replaced value
                this._n--;
            }
        }

        public int IndexOf(int value) => this._s[value];

        /// <summary>
        /// Determines whether the set contains the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the set contains the given value; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int value)
        {
            if (value >= this._max || value < 0)
                return false;
            else
                return this._s[value] < this._n && this._d[this._s[value]] == value; // value must meet two conditions:
            // 1. link value from the sparse array
            // must point to the current used range
            // in the dense array
            // 2. there must be a valid two-way link
        }

        /// <summary>
        /// Removes all elements from the set.
        /// </summary>
        public void Clear()
        {
            this._n = 0; // simply set n to 0 to clear the set; no re-initialization is required
        }

        /// <summary>
        /// Gets the number of elements in the set.
        /// </summary>
        public int Count
        {
            get { return this._n; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through all elements in the set.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<int> GetEnumerator()
        {
            var i = 0;
            while (i < this._n)
            {
                yield return this._d[i];
                i++;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through all elements in the set.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}