using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGeneratorAndValidator
    {
        private static CodeGeneratorAndValidator instance;
        private static string characterSet = "ACDEFGHKLMNPRTXYZ234579";     // Indicates the valid character set
        private string currentCharacterSet;
        private static int codeLength = 8;                                  //Indicates the number of digits the code will have. Possible code count is [Math.Pow(charGroupLength, codeLength)]
        private static int charGroupLength = 3;                             //Indicates the number of possible values that a single digit of the code can take     
        private List<List<char>> charGroupList = new List<List<char>>();
        private readonly Random random;
        private Regex validationRegex;                                      //Validation regex for generated codes.
        private string validationRegexPattern;                              //Validation regex pattern
        private int counter;                                                //Indicates the how many codes generated.
        private CodeGeneratorAndValidator()
        {
            random = new Random();
            Initialize();
        }
        public static CodeGeneratorAndValidator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CodeGeneratorAndValidator();
                }
                return instance;
            }
        }
        private void Initialize()
        {
            InitializeCharGroupList();
            InitializeValidationRegex();
        }

        //Initializes the possible characters that can be used for each digit of the code.
        private void InitializeCharGroupList()
        {
            for (int i = 0; i < codeLength; i++)
            {
                List<char> characterList = GenerateRandomCharGroup();

                charGroupList.Add(characterList);
            }
        }

        //Creates a character list which by determining the possible characters that can be used for a specific digit of the code.
        private List<char> GenerateRandomCharGroup()
        {
            List<char> charGroup = new List<char>();  //A char list includes possible values of specific digit of the code.

            for (int i = 0; i < charGroupLength; i++)
            {
                char randomCharacter;
                int randomIndex;
                do
                {
                    /* The characters to be included in the code are randomly selected. 
                     * However, in order to make use of all valid characters that can be generated in the code as much as possible,
                     * the randomly selected character is removed from the string. After all the characters have been used, 
                     * the "currentCharacterSet" string is re-initialized from main "characterSet" string.
                     * Randomly selecting characters for each digit reduces the likelihood of predicting future codes,
                     * as it introduces an element of randomness and makes the patterns less predictable.
                     */

                    if (string.IsNullOrEmpty(currentCharacterSet))
                    {
                        currentCharacterSet = characterSet;
                    }
                    randomIndex = random.Next(currentCharacterSet.Length);

                    randomCharacter = currentCharacterSet[randomIndex];                 
                                                                                        
                } while (charGroup.Contains(randomCharacter));                          // Ensures that the elements within each character group are unique to avoid the possibility of generating duplicate codes.

                charGroup.Add(randomCharacter);
                currentCharacterSet=currentCharacterSet.Remove(randomIndex,1);
            }
            return charGroup; 
        }

        //Initializes the Regex and Regex pattern 
        private void InitializeValidationRegex()
        {
            List<string> patternParts = new List<string>();

            for (int i = 0; i < charGroupList.Count; i++)
            {
                List<char> charGroup = charGroupList[i];

                string charGroupPattern = string.Join("|", charGroup);

                patternParts.Add($"({charGroupPattern})");
            }

            validationRegexPattern = $"^{string.Join("", patternParts)}$";        
            validationRegex = new Regex(validationRegexPattern);                  
        }

        //Validates the code with exist Regex Pattern
        public void ValidateCode(string code)
        {
            bool isValid = validationRegex.IsMatch(code);

            Console.WriteLine($"Code isValid: {isValid}");
        }

        //Generates Code 
        public string GenerateCode()
        {
            string code = string.Empty;
            int index = counter;

            for (int i = 0; i < codeLength; i++)
            {
                int charGroupIndex = index % charGroupLength;
                code += charGroupList[i][charGroupIndex];
                index /= charGroupLength;
            }

            counter++;
            return code;
        }

        // Prints the generated CodeGeneratorAndValidator instance information.
        public void PrintSummary()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Code validation regex pattern : ");
            Console.WriteLine(validationRegexPattern);
            foreach (var charGroup in charGroupList)
            {
                Console.Write($"{(charGroupList.IndexOf(charGroup) + 1).ToString()}.digit possible values : ");
                foreach (var c in charGroup)
                {
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Total possible code count : {Math.Pow(charGroupLength, codeLength)}");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

    }
}
