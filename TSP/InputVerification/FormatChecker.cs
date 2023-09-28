using System.Text;
using System.Text.RegularExpressions;

namespace TSP.InputVerification
{
    /// <summary>
    /// Checks if a <see cref="IFormFile"/> is formatted in a specific way.
    /// </summary>
    public class FormatChecker
    {
        // Private instance variable to hold the single instance of the FormatChecker class.
        private static FormatChecker? _Instance;
        // Private instance variable to hold the single instance of the FormatChecker class.
        private FormatChecker() { }
        /// <summary>
        /// Gets the single instance of the FormatChecker class.
        /// </summary>
        public static FormatChecker Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FormatChecker();
                }
                return _Instance;
            }
        }
        /// <summary>
        /// This checks if the provided <see cref="IFormFile"/> contains two <see cref="double"/> numbers on each line (max. 2000 lines), separated by a space.
        /// </summary>
        /// <param name="text"><see cref="IFormFile"/> for which the check is performed.</param>
        /// <param name="maxNumberOfLines"><see cref="int"/>Maximum number of lines that the file can have.</param>
        /// <returns>True if format is respected, false otherwise.</returns>
        public async Task<bool> SimpleXY(IFormFile text, int maxNumberOfLines)
        {
            // Read content of the give text file.
            StreamReader streamReader = new StreamReader(text.OpenReadStream(), Encoding.UTF8);
            string fileContent = await streamReader.ReadToEndAsync();
            // Split the file content into individual lines.
            string[] line = fileContent.Split("\n");
            if (line.Length > maxNumberOfLines)
            {
                return false;
            }
            foreach (string s in line)
            {
                // Trim the current line.
                string trimmed = s.Trim();
                // Replace any contiguous segment of spaces to a single space character.
                string result = Regex.Replace(trimmed, @"\s+", " ");
                // Split the formatted line into multiple numbers.
                string[] numbers = result.Split(" ");
                // Check if there are exactly two numbers or not on the current line.
                if (numbers.Length != 2)
                {
                    return false;
                }
                // Check if each of the two numbers can be parsed as double.
                if (!(double.TryParse(numbers[0], out double res1) && double.TryParse(numbers[1], out double res2)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
