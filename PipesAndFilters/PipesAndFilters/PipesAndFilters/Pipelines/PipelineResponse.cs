namespace PipesAndFilters.Pipelines
{
    public class PipelineResponse
    {
        public string Result { get; }

        public bool HasErrors => PipelineError != null;

        public PipelineError PipelineError { get; }

        public PipelineResponse(string result, PipelineError pipelineError = null)
        {
            Result = result;
            PipelineError = pipelineError;
        }
    }
}