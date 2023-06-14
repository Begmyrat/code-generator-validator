using CodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {

        
        CodeGeneratorAndValidator codeGeneratorAndValidator = CodeGeneratorAndValidator.Instance; //A singleton instance was created to keep the code generation in the same way, even if it is used in different places.
        codeGeneratorAndValidator.PrintSummary(); //Prints the instance informations.

        var maxCodeCount = 30; //Indicates the how many code will be generated.
        var counter = 0;       //Counter for generated codes.

        while (counter < maxCodeCount)
        {
            string code = codeGeneratorAndValidator.GenerateCode();     //Generates codes.
            Console.WriteLine($"Generated code: {code}");               //Prints generated code
            codeGeneratorAndValidator.ValidateCode(code);               //Validates code and prints result
            counter++;
        }
    }
}

