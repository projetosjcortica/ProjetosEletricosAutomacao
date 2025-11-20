namespace Domain.Value_Objects.Descricao.Handles
{
    public interface IDescricaoHandler
    {
        IDescricaoHandler? Next { get; set; }
        string Handle(string description);
        void SetNext(IDescricaoHandler next);
    }
}
