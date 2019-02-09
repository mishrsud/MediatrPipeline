using System;

namespace PipesAndFilters.Pipelines
{
    public class PipelineError
    {
        public Exception Exception { get; set; }
        public string OcurredAtStep { get; set; }
        public string Message { get; set; }
    }
}