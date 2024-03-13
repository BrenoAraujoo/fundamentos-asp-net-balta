namespace Blog.ViewModel
{
    public class ResultViewModel<T>
    {

        public ResultViewModel(T data, List<string> erros)
        {
            Data = data;
            Erros = erros;
        }
        public ResultViewModel(T data)
        {
            Data = data;
        }
        public ResultViewModel(List<string> erros)
        {
            Erros = erros;
        }
        public ResultViewModel(string error)
        {
            Erros.Add(error);
        }
        public T Data { get; set; }
        public List<string> Erros { get; private set; } = new();
    }
}
