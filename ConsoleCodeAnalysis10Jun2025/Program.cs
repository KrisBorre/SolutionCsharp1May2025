using LibraryCodeAnalysis1Jun2025;

// See SolutionCsharpGloVe3Sep2024 project ConsoleCodeAnalysis24Nov2024
namespace ConsoleCodeAnalysis10Jun2025
{
    /// <summary>
    /// Precondition: Anaconda AI Navigator runs a local LLM.
    /// </summary>
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var apiClient = new ApiClient12Nov2024("http://localhost:8084");

            string initialContext = @"You are a coding assistant specializing in C# development. Your task is to write a C# program that demonstrates design patterns. The program should:

Be self-explanatory without comments, using descriptive variable and method names.
Include // TODO for any incomplete sections.
Adhere to object-oriented programming best practices.
Focus solely on generating code, with no explanatory text.";

            var project1 = new Project9Oct2024 { Name = "Design Patterns" };
            project1.Prompts.Add(@"Write a C# class implementing the Singleton design pattern. Ensure it prevents instantiation outside of the class and provides a single, globally accessible instance. Include a GetInstance method.");
            project1.Prompts.Add(@"Add a Factory Method design pattern to the program. Create a base interface IShape and two implementations: Circle and Square. Implement a ShapeFactory class to create specific shapes based on input.");
            project1.Prompts.Add(@"Introduce the Adapter design pattern. Create a class that adapts an existing class, such as LegacyPrinter, to work with a new interface IPrinter.");
            project1.Prompts.Add(@"Implement the Decorator design pattern. Create a base interface IReport and two concrete classes for generating reports. Add decorators to enhance the report with additional details dynamically.");
            project1.Prompts.Add(@"Add the Observer design pattern. Implement a class WeatherStation that notifies observers, such as Display and Logger, whenever the weather data changes.");
            project1.Prompts.Add(@"Incorporate the Strategy design pattern. Create an interface IPaymentStrategy with different payment methods, such as CreditCard and PayPal. Allow clients to choose the strategy at runtime.");
            project1.Prompts.Add(@"Introduce the Composite design pattern. Create a file system where folders and files can be treated uniformly as IFileSystemComponent, and implement methods to add, remove, and display components.");
            project1.Prompts.Add(@"Implement the Command design pattern. Create a set of commands, such as TurnOn and TurnOff, and a RemoteControl class to execute and undo commands on devices like TV and Radio.");
            project1.Prompts.Add(@"Write a Main method to demonstrate the integrated functionality of all implemented design patterns. Instantiate and use Singleton, Factory, Adapter, Decorator, Observer, Strategy, Composite, and Command patterns cohesively.");

            var apiClientHandler = new ApiClientHandler12Nov2024(apiClient);
            var projectPromptHandler = new ProjectPromptHandler1Jun2025(apiClientHandler);

            ProjectManager26Nov2024 projectManager = new ProjectManager26Nov2024();

            if (await apiClient.IsServerHealthyAsync())
            {
                for (int attempt = 1; attempt <= 2; attempt++)
                {
                    projectManager.CreateNewConversation(project1);
                    await projectPromptHandler.HandlePromptsAsync(project1, initialContext);
                }
            }
            else
            {
                Console.WriteLine("Server is not ready for requests.");
            }

            projectManager.Print(project1);

            /*
             
             */

            Console.ReadLine();
        }
    }
}