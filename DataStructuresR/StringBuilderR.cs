using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR
{
    public class StringBuilderR
    {
        private char[] characters;
        public int Length { get; private set; } = 0;
        public int MaxCapacity => int.MaxValue;
        private const int DefaultCapacity = 16;
        private int capacity;
        public int Capacity { 
            get
            {
                return capacity;
            }
            set
            {
                if (value < Length)
                    throw new ArgumentException("The capacity cannot below the current length.");

                if (value > MaxCapacity)
                    throw new ArgumentException("The capacity cannot be above the maximum capacity.");

                capacity = value;
            }
                
        }

        #region Private Methods

        private char[] InitializCapacityGetCharArray(int? capacity)
        {
            if (capacity.HasValue)
                this.Capacity = capacity.Value;
            else
                this.Capacity = DefaultCapacity;

            return new char [this.Capacity];
        }

        private void IncreaseCapacity()
        {
            capacity = capacity * 2;
        }

        private void AddCharacters(char[] newCharacters)
        {
            // Record the current length know when to start appending the passed
            // in newCharacters to the characters array.
            int oldLength = Length;


            // TODO: Possibly need to remove all empty strings from the newCharacters array.
            Length = Length + newCharacters.Length;

            if (Length >= Capacity)
            {
                while (Length >= Capacity)
                    IncreaseCapacity();

                char[] largerStorage = new char[Capacity];

                for (int i = 0; i < this.characters.Length; i++)
                    largerStorage[i] = this.characters[i];

                this.characters = largerStorage;
            }

            for (int i = 0, j = oldLength; i < newCharacters.Length; i++, j++)
                this.characters[j] = newCharacters[i];
        }

        #endregion Private Methods

        #region Constructors
        public StringBuilderR() 
        {
            this.characters = InitializCapacityGetCharArray(null);
        }

        public StringBuilderR(int capacity)
        {
            this.characters = InitializCapacityGetCharArray(capacity);
        }

        public StringBuilderR(string? str)
        {
            this.characters = InitializCapacityGetCharArray(null);

            if (str != null )
                this.AddCharacters(str.ToCharArray());
        }

        public StringBuilderR(string? str, int capacity)
        {
            this.characters = InitializCapacityGetCharArray(capacity);

            if (str != null)
                this.AddCharacters(str.ToCharArray());
        }

        public StringBuilderR(char[] str)
        {
            this.characters = InitializCapacityGetCharArray(null);

            if (str.Length != 0)
                this.AddCharacters(str);
        }

        public StringBuilderR(char[] str, int capacity)
        {
            this.characters = InitializCapacityGetCharArray(capacity);

            if (str.Length != 0)
                this.AddCharacters(str);
        }
        #endregion Constructors

        /// <summary>
        /// Converts the value of this instance to a string.
        /// </summary>
        /// <returns>
        /// Returns a string that is comprised of the characters stored in this instance.
        /// </returns>
        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < Length; i++)
                str = str + characters[i];
            
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            Array.Clear(characters);
            Length = 0;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            if (sourceIndex < 0)
                throw new ArgumentOutOfRangeException("sourceIndex", "The sourceIndex cannot be below 0.");

            if (sourceIndex >= this.Length)
                throw new ArgumentOutOfRangeException("sourceIndex", "The sourceIndex is larger than length of the current length.");

            if (sourceIndex + count >= this.Length)
                throw new ArgumentOutOfRangeException("count", "The specified number of character to copy over from the source index excedes the length of the source array.");

            if (destinationIndex >= destination.Length)
                throw new ArgumentOutOfRangeException("destinationIndex", "The destinationIndex is large than the length of the destination array.");

            if (destinationIndex + count >= destination.Length)
                throw new ArgumentOutOfRangeException("count", "The specified number of character to copy over to the destination index excedes the length of the destination array.");

            for (int i = 0, j = sourceIndex, h = destinationIndex; i < count; i++, j++, h++)
                destination[h] = characters[j];
        }

        #region Append Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR Append(char a)
        {
            AddCharacters(new char[] { a });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR AppendLine(char a)
        {
            AddCharacters(new char[] { a });
            AddCharacters(new char[] { '\n' });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR Append(char[] str)
        {
            AddCharacters(str);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR AppendLine(char[] str)
        {
            AddCharacters(str);
            AddCharacters(new char[] { '\n' });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR Append(string str)
        {
            AddCharacters(str.ToArray());

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR AppendLine(string str)
        {
            AddCharacters(str.ToArray());
            AddCharacters(new char[] { '\n' });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        public StringBuilderR AppendFormat(string str, params object[] args)
        {
            string formattedStr = string.Format(str, args);

            AddCharacters(formattedStr.ToArray());

            return this;
        }

        #endregion Append Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns>
        /// Returns a reference this instance after the operation is complete
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public StringBuilderR Remove(int start, int length)
        {

            if (start < 0)
                throw new ArgumentOutOfRangeException("start", "The starting index must be 0 or greater.");

            if (start >= Length)
                throw new ArgumentOutOfRangeException("start", "The starting position is beyond the length of the array.");

            if (length > this.Length)
                throw new ArgumentOutOfRangeException("length", "The length of characters specified to be removed is long the length of the character array.");

            if (length > this.Length - start)
                throw new ArgumentOutOfRangeException("length", "The length of characters specified to be removed is longer length from the starting index to the end of the character array.");

            int newLength = this.Length - length;

            // shift any characters that come after the segment being move removed down
            // by the length of the segment being removed
            for (int i = start, j = start + length; j < this.Length; i++, j++)
                characters[i] = characters[j];

            // null out the character values from the new length to the old length
            for (int i = newLength; i < length; i++)
                characters[i] = '\0';

            this.Length = newLength;

            return this;
        }

        public StringBuilderR Replace(char oldChar, char newChar)
        {
            // Call the substring version of the method because 
            // the full length of the current string is just the larges substring
            return Replace(oldChar, newChar, 0, this.Length);
        }

        public StringBuilderR Replace(string oldValue, string? newValue)
        {
            // Call the substring version of the method because 
            // the full length of the current string is just the larges substring
            return Replace(oldValue, newValue, 0, this.Length);
        }

        public StringBuilderR Replace(char oldChar, char newChar, int startIndex, int length)
        {
            // Return the current instance because there is nothing to replace.
            if (this.Length == 0)
                return this;

            if (startIndex >= this.characters.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "The start index cannot larger than the current length of the screen.");

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "The start index cannot be less than 0.");

            if (startIndex + length >= this.characters.Length)
                throw new ArgumentOutOfRangeException(nameof(length), "The specified length of the substring exceededs the current string's length from the specified index.");

            for (int i = startIndex, limit = startIndex + length; i < limit; i++)
            {
                if (characters[i] == oldChar)
                    characters[i] = newChar;
            }

            return this;
        }

        public StringBuilderR Replace(string oldValue, string? newValue, int startIndex, int length)
        {
            // Return the current instance because there is nothing to replace.
            if (this.Length == 0)
                return this;

            if (startIndex >= this.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "The start index cannot be greater than the length of the currents instance's string.");

            if (startIndex + length >= this.Length)
                throw new ArgumentOutOfRangeException(nameof(length), "The specified substring is beyond the length of the instance's string.");

            if (string.IsNullOrEmpty(oldValue))
                throw new ArgumentException("The value being replaced cannot be an empty string.", nameof(oldValue));

            // There will be no match of the oldValue if it is longer than substring.
            // Just return the current instance without doing work.
            if (length < oldValue.Length)
                return this;

            // If the oldValue and the newValue are both a single character,
            // then call the equivalent method to replace a single character with another.
            if (oldValue.Length == 1 && newValue != null && newValue.Length == 1)
                return Replace(oldValue[0], newValue[0], startIndex, length);


            // Find matches within the substring.
            List<int> foundMatches = new List<int>();
            int prevMatchCount;

            for (int i = startIndex, limit = startIndex + length;
                    // Terminate the loop if i is greater than the leng of the specified substring
                    // or if the remaing characters in the substring are less than the length of the oldValue.
                    i < limit && length - i >= oldValue.Length; 
                    // If an additional match of the old value is found, 
                    // then increment by the length of the oldValue. Otherwise increment by 1.
                    i = i + (foundMatches.Count > prevMatchCount ? oldValue.Length : 1))
            {
                prevMatchCount = foundMatches.Count;

                for (int j = 0; i + j < limit && j <= oldValue.Length; j++)
                {
                    // If j reached the length of the oldValue then we found the match of the old value.
                    if (j == oldValue.Length)
                        foundMatches.Add(i);
                    else if (characters[i + j] != oldValue[j])
                        j = oldValue.Length;
                }
            }

            // If any instances of the oldValue was found in the substring,
            // then copy over the existing characters replacing any instances of the oldValue with newValue
            if (foundMatches.Count > 0)
            {
                int newValueLength = 0;
                char[]? newValueChars = null;

                if (!string.IsNullOrEmpty(newValue))
                {
                    newValueLength = newValue.Length;
                    newValueChars = newValue.ToCharArray();
                }

                int newLength = characters.Length - (oldValue.Length * foundMatches.Count) + (newValueLength * foundMatches.Count);

                while (newLength > this.Capacity)
                    IncreaseCapacity();

                char[] newChars = new char[this.Capacity];

                // Copy over any characters from before the start index.
                Array.Copy(this.characters, 0, newChars, 0, startIndex);

                int oldArrayIndex = startIndex,
                    newArrayIndex = startIndex;

                foreach (int foundIndex in foundMatches)
                {
                    // Get the difference between the value of the oldArrayIndex and the current 
                    int charsBeforeMatch = foundIndex - oldArrayIndex;
                    // Copy any characters before the index of the match that is currently selected.
                    Array.Copy(this.characters, oldArrayIndex, newChars, newArrayIndex, charsBeforeMatch);

                    // Add the sum of the number of characters before the current match and the length of the oldValue
                    // This sets the index up for the next time characters need to be copied over, so nothing gets over written.
                    oldArrayIndex = oldArrayIndex + charsBeforeMatch + oldValue.Length;

                    // Add the characters copied before the match to the index to move it to correct position to copy the newValue to.
                    newArrayIndex = newArrayIndex + charsBeforeMatch;

                    // Copy the new value over to the new array if there are any characters to copy over.
                    if (newValueChars != null)
                    {
                        Array.Copy(newValueChars, 0, newChars, newArrayIndex, newValueLength);

                        // Need to increament the oldArray index by the length of the new value.
                        // This sets the index up to copy over next set of characters, so nothing gets over written.
                        newArrayIndex = newArrayIndex + newValueLength;
                    }
                }

                if (oldArrayIndex < this.Length)
                    Array.Copy(this.characters, oldArrayIndex, newChars, newArrayIndex, this.Length - oldArrayIndex);

                this.characters = newChars;
                this.Length = newLength;
            }

            return this;
        }
    }
}
