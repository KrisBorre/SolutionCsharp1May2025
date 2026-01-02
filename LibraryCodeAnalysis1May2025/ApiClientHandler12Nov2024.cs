namespace LibraryCodeAnalysis1May2025
{
    public class ApiClientHandler12Nov2024
    {
        private readonly ApiClient12Nov2024 _apiClient;
        private double _temperature;

        public ApiClientHandler12Nov2024(ApiClient12Nov2024 apiClient, double temperature = 0.8)
        {
            _apiClient = apiClient;
            _temperature = temperature;
        }

        public async Task<string> SendPromptAsync(string context, string prompt)
        {
            return await _apiClient.PostCompletionAsync(prompt: context + $"\nUser: {prompt}\nAssistant:", temperature: _temperature);
        }

    }

}
