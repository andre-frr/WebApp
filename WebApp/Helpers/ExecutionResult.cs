namespace WebApp.Helpers
{
    public class ExecutionResult<T>
    {
        public bool status {  get; set; }
        public T results { get; set; }
        public List<string>? message { get; set; }

    }
}
