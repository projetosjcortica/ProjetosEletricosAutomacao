namespace Domain.Agreggates
{
    public class PageData
    {
        public List<IDataPage> Data { get; set; } = new List<IDataPage>();
        public PageData(List<IDataPage> data)
        {
            Data = data;
        }

        public PageData()
        { }

        public void InsertPageData(IDataPage data)
        {
            Data.Add(data);
        }
    }
}
