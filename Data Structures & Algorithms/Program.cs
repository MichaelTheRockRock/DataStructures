// See https://aka.ms/new-console-template for more information

using DataStructure.Attributes;
using System.Text;

try
{
    #region Build Example Menu

    StringBuilder instructionMenu = new StringBuilder("");

    instructionMenu.AppendLine("Choose a data structure to choose example functions that use it or type \"exit\" to end the program.");

    string[] dataStructures = Enum.GetValues<DataStructures>()
            .Select(x => x.GetDescription())
            .Where(x => x != null)
            .ToArray(); // Get a list of dataStructures for the data structures that have example functions built.

    if (dataStructures.Length > 0)
    {
        for (int i = 0; i < dataStructures.Length; i++)
        {
            instructionMenu.AppendLine(string.Format("  {0}. {1}", i + 1, dataStructures[i]));
        }

        instructionMenu.AppendLine("");
        instructionMenu.Append("Enter Data Structure: ");
    }
    else
    {
        instructionMenu.AppendLine("There are no data structures implemented.");
        instructionMenu.AppendLine("Press any key to exit the program");
    }

    #endregion Build Example Menu

    bool endProgram = false;
    string exitCommand = "exit";
    string instruction;

    do
    {
        Console.Write(instructionMenu.ToString());

        if (dataStructures == null || dataStructures.Length <= 0)
        {
            instruction = exitCommand;
            Console.Read();
        }
        else
        {
            instruction = Console.ReadLine() ?? "";
            Console.WriteLine(""); //For spacing and readability
        }


        if (instruction.ToLower() == exitCommand)
        {
            endProgram = true;
        }
        else
        {
            Console.WriteLine("Examples would have been run.\n");
        }

    } while (!endProgram);

    Console.WriteLine("Exiting the Data Structures program.");

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    return;
}