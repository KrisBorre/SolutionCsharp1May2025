
// see solution SolutionCsharpGloVe3Sep2024 project LibraryCodeAnalysis30Nov2024
namespace LibraryCodeAnalysis1Jun2025
{
    public class ProjectManager26Nov2024
    {       
        public Conversation CreateNewConversation(Project9Oct2024 project)
        {
            var conversation = new Conversation
            {
                Number = project.Conversations.Count + 1
            };
            project.Conversations.Add(conversation);
            return conversation;
        }

        public SourceCodeIteration26Nov2024 CreateNewIteration(Conversation conversation)
        {
            var iteration = new SourceCodeIteration26Nov2024
            {
                Number = conversation.SourceCodeIterations.Count + 1
            };
            conversation.SourceCodeIterations.Add(iteration);
            return iteration;
        }



        public void Print(Project9Oct2024 project)
        {
            Console.WriteLine($"Project: {project.Name}");
            foreach (var conversation in project.Conversations)
            {
                Console.WriteLine($"conversation #{conversation.Number}");
                foreach (var sourceCodeIteration in conversation.SourceCodeIterations)
                {
                    Console.WriteLine($"iteration #{sourceCodeIteration.Number}");

                    if (sourceCodeIteration.IsCompiled)
                    {
                        if (sourceCodeIteration.CompilationSuccess)
                        {
                            Console.WriteLine($"{sourceCodeIteration.ExecutionOutput}");
                        }
                        else
                        {
                            Console.WriteLine($"{sourceCodeIteration.DiagnosticResults}");
                        }
                    }

                    foreach (var sourceCode in sourceCodeIteration.SourceCodes)
                    {
                        Console.WriteLine($"Code: {sourceCode.Code}");

                        if (sourceCode.IsCompiled)
                        {
                            if (sourceCode.CompilationSuccess)
                            {
                                Console.WriteLine($"Execution Output: {sourceCode.ExecutionOutput}");
                            }
                            else
                            {
                                Console.WriteLine($"Compilation Errors: {sourceCode.CompilationErrors}");
                            }
                        }
                    }
                }
            }
        }

       

    }
}
