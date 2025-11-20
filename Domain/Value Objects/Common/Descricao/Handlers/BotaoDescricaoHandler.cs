namespace Domain.Value_Objects.Descricao.Handles
{
    public class BotaoDescricaoHandler : IDescricaoHandler
    {
        public IDescricaoHandler? Next { get ; set; }

        public void SetNext(IDescricaoHandler next)
        {
            Next = next; 
        }
        public string Handle(string description)
        {
            var isButton = description.Contains("Botão") || description.Contains("Botao") || description.Contains("Botão Emergência");
            if (!isButton) return Next.Handle(description);

            var isBotaoEmergencia = description.Contains("Botão Emergência");
            if (isBotaoEmergencia)
                return description.Replace("Botão Emergência ", "Botão\r\n").Replace("Botao Emergência ", "Botao\r\n");

            return description.Replace("Botão ", "Botão\r\n").Replace("Botao ", "Botao\r\n");
        }    
    }
}
