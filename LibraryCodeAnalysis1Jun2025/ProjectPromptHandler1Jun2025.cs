namespace LibraryCodeAnalysis1Jun2025
{
    public class ProjectPromptHandler1Jun2025
    {
        private readonly ApiClientHandler12Nov2024 _apiClientHandler;

        private readonly CodeProcessing26Nov2024 _codeProcessing;

        public ProjectPromptHandler1Jun2025(ApiClientHandler12Nov2024 apiClientHandler)
        {
            _apiClientHandler = apiClientHandler;
            _codeProcessing = new CodeProcessing26Nov2024();
        }

        public async Task HandlePromptsAsync(Project9Oct2024 project, string initialContext)
        {
            string context = initialContext;

            foreach (var prompt in project.Prompts)
            {
                Console.WriteLine($"User: {prompt}");
                string assistantResponse = await GetAssistantResponseAsync(context, prompt);
                context = ContextManager1Dec2024.UpdateContext(context, prompt, assistantResponse);

                if (!EnsureContextWithinTokenLimit(ref context))
                {
                    Console.WriteLine("Warning: Context condensed due to token limit.");
                }

                if (!ValidateCodeBlocks(ref context, assistantResponse))
                {
                    Console.WriteLine("I'm terminating this project.");
                    break;  // terminate this project immediately
                }

                Conversation conversation = project.Conversations.Last();

                ProcessAssistantResponse(project, conversation, ref context, assistantResponse);
            }
        }

        private async Task<string> GetAssistantResponseAsync(string context, string prompt)
        {
            string response = await _apiClientHandler.SendPromptAsync(context, prompt);
            Console.WriteLine($"Assistant: {response}");
            return response;
        }

        private bool EnsureContextWithinTokenLimit(ref string context)
        {
            TokenManager tokenManager = new TokenManager();
            int tokenCount = tokenManager.EstimateTokenCount(context);
            if (!tokenManager.CheckTokenLimit(tokenCount))
            {
                context = ContextManager1Dec2024.CondenseContextAsync(context, _apiClientHandler).Result;
                return false;
            }
            return true;
        }

        private bool ValidateCodeBlocks(ref string context, string assistantResponse)
        {
            if (CodeValidator.IsCsharpBlockProperlyClosed(assistantResponse)) return true;

            Console.WriteLine("The code has improper `csharp` block closures.");
            string instruction = "Complete the last C# code you wrote.";
            assistantResponse = _apiClientHandler.SendPromptAsync(context, instruction).Result;
            Console.WriteLine($"Assistant: {assistantResponse}");

            context = ContextManager1Dec2024.UpdateContext(context, instruction, assistantResponse);
            return CodeValidator.IsCsharpBlockProperlyClosed(assistantResponse);
        }

        private void ProcessAssistantResponse(Project9Oct2024 project, Conversation conversation, ref string context, string assistantResponse)
        {
            List<string> extractedCodeBlocks = CodeValidator.ExtractCSharpCode(assistantResponse);

            ProjectManager26Nov2024 projectManager = new ProjectManager26Nov2024();
            SourceCodeIteration26Nov2024 iteration = projectManager.CreateNewIteration(conversation);

            foreach (string codeBlock in extractedCodeBlocks)
            {
                //_codeProcessing.ProcessCodeBlock(iteration, codeBlock); // TODO
            }

            if (extractedCodeBlocks.Count == 1)
            {
                // TODO: the iteration is one code block, so the info of the code block needs to be copied to the iteration.
            }

            if (extractedCodeBlocks.Count == 2)
            {
                //_codeProcessing.ProcessTwoCodeBlocks(iteration, codeBlock1: extractedCodeBlocks[0], codeBlock2: extractedCodeBlocks[1]); // TODO
            }

            if (extractedCodeBlocks.Count == 3)
            {
                //_codeProcessing.ProcessThreeCodeBlocks(iteration, codeBlock1: extractedCodeBlocks[0], codeBlock2: extractedCodeBlocks[1], codeBlock3: extractedCodeBlocks[2]); // TODO
            }

            // TODO: What if one of the 3 code blocks is redundant residue?

            DisplayManagement1Jun2025 displayManagement = new DisplayManagement1Jun2025();
            displayManagement.DisplayIterationResults(project, conversation, iteration);

            SourceCodeIterationManager sourceCodeIterationManager = new SourceCodeIterationManager();
            sourceCodeIterationManager.Iterations = conversation.SourceCodeIterations;
            sourceCodeIterationManager.PreviousIterationIndex = iteration.Number - 1;
            sourceCodeIterationManager.CurrentIterationIndex = iteration.Number;
            sourceCodeIterationManager.NextIterationIndex = iteration.Number + 1;


            sourceCodeIterationManager.CreateNewKnowledgeBase(_codeProcessing);


            HandleFailedCompilation(project, ref context, conversation, iteration);
        }


        private void HandleFailedCompilation(Project9Oct2024 project, ref string context, Conversation conversation, SourceCodeIteration26Nov2024 iteration)
        {
            if (iteration.CompilationSuccess || iteration.SourceCodes.Count != 1) return;

            string promptForFix = ConstructFixPrompt(iteration);
            Console.WriteLine(promptForFix);

            string assistantFixResponse = _apiClientHandler.SendPromptAsync(context, promptForFix).Result;
            Console.WriteLine($"Assistant: {assistantFixResponse}");

            context = ContextManager1Dec2024.UpdateContext(context, promptForFix, assistantFixResponse);

            if (!ValidateCodeBlocks(ref context, assistantFixResponse)) return;

            List<string> newCodeBlocks = CodeValidator.ExtractCSharpCode(assistantFixResponse);

            ProjectManager26Nov2024 projectManager = new ProjectManager26Nov2024();
            SourceCodeIteration26Nov2024 newIteration = projectManager.CreateNewIteration(conversation);

            foreach (string newCodeBlock in newCodeBlocks)
            {
                //_codeProcessing.ProcessCodeBlock(newIteration, newCodeBlock); // TODO
            }

            DisplayManagement1Jun2025 displayManagement = new DisplayManagement1Jun2025();
            displayManagement.DisplayIterationResults(project, conversation, newIteration);
        }

        private string ConstructFixPrompt(SourceCodeIteration26Nov2024 iteration)
        {
            return $"We get the following compilation messages:{Environment.NewLine}" +
                   $"{iteration.DiagnosticResults}{Environment.NewLine}{Environment.NewLine}" +
                   "```csharp" + Environment.NewLine +
                   $"{iteration.SourceCodes[0].Code}" + Environment.NewLine +
                   "```";
        }
    }
}
