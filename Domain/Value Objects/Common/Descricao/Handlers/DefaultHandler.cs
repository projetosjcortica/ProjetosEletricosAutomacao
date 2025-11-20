namespace Domain.Value_Objects.Descricao.Handles
{
    public class DefaultHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get; set; }

        public string Handle(string description)
        {
            return description;
        }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next;
        }
    }
}
