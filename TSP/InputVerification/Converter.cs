using System.Text.RegularExpressions;
using System.Text;
using TSP.Domain;

namespace TSP.InputVerification
{
    /// <summary>
    /// Converts from <see cref="IFormFile"/> to a specific format.
    /// </summary>
    public class Converter
    {
        // Private instance variable to hold the single instance of the Converter class.
        private static Converter? instance;
        // Private instance variable to hold the single instance of the Converter class.
        private Converter() { }
        /// <summary>
        /// Gets the single instance of the Converter class.
        /// </summary>
        public static Converter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Converter();
                }
                return instance;
            }
        }
        /// <summary>
        /// Converts the content from <see cref="IFormFile"/> to a <see cref="Map"/>.
        /// The text file is expected to contain two <see cref="double"/> numbers on each line, separated by a space.
        /// This method may produce unexpected results if the input format is not respected.
        /// </summary>
        /// <param name="text"><see cref="IFormFile"/> to convert from.</param>
        /// <returns>Result of converting <see cref="IFormFile"/> to a <see cref="Map"/>.</returns>
        public async Task<Map> SimpleXY(IFormFile text)
        {
            // Read content of the give text file.
            StreamReader streamReader = new StreamReader(text.OpenReadStream(), Encoding.UTF8);
            string fileContent = await streamReader.ReadToEndAsync();
            // Split the file content into individual lines.
            string[] line = fileContent.Split("\n");
            Map map = new Map();
            foreach (string s in line)
            {
                // Trim the current line.
                string trimmed = s.Trim();
                // Replace any contiguous segment of spaces to a single space character.
                string formattedLine = Regex.Replace(trimmed, @"\s+", " ");
                // Split the formatted line into multiple numbers.
                string[] numbers = formattedLine.Split(" ");
                // Try to parse the numbers.
                double.TryParse(numbers[0], out double res1);
                double.TryParse(numbers[1], out double res2);
                // Create a Point from the resulted numbers and add it to the Map.
                map.Add(new Point(res1, res2));
            }
            // Return the resulted Map.
            return map;
        }
    }
}
