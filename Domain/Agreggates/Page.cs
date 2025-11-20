using Domain.Value_Objects;

namespace Domain.Agreggates
{
    public class Page
    {
        public int Id { get; set; }
        public string Panel { get; private set; }
        private PageData PageData { get; set; }
        private Dictionary<string, string> _CachePageData = new Dictionary<string, string>();
        public DescriptionPage DescriptionPage { get; private set; }
        public List<Shape> Shapes { get; private set; }
        public int PageNumber { get; private set; }

        public Page(int id, DescriptionPage descriptionPage, PageData pageData)
        {
            Id = id;
            PageData = pageData;
            DescriptionPage = descriptionPage;
            Shapes = new List<Shape>();
        }

        public void AddShape(Shape shape) { Shapes.Add(shape); }

        public Dictionary<string, string> GetPageData()
        {
            if (_CachePageData.Count != 0)
                return _CachePageData;

            var PagesTooMany = new List<string>() { "P-SENS-3"}; 

            var DataResult = new Dictionary<string, string>();

            foreach (var item in PageData.Data)
            {
                foreach (var kvp in item.GetData())
                {
                    if (!DataResult.ContainsKey(kvp.Key))
                    {
                        DataResult.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        DataResult[kvp.Key] = kvp.Value;
                    }
                }
            }

            DataResult["pagina"] = PageNumber.ToString();

            _CachePageData = DataResult;
            return DataResult;
        }

        public bool IsComandoPage()
        {
            return DescriptionPage.Nomenclatura.Value.Contains("COMANDO");
        }

        public bool IsSoftStarterPage()
        {
            return DescriptionPage.Nomenclatura.IsSoftStarter();
        }

        public bool IsInversorPage()
        {
            return DescriptionPage.Nomenclatura.IsInversor();
        }

        public bool IsFreioElevadorPage()
        {
            return DescriptionPage.Nomenclatura.Value.Contains("FR-EL");
        }

        public bool IsElevadorPage()
        {
            return DescriptionPage.Nomenclatura.Value.StartsWith("EL-") || DescriptionPage.Nomenclatura.Value.StartsWith("SS-EL-");
        }

        public bool IsReversaoPage()
        {
            return DescriptionPage.Nomenclatura.Value.Contains("CAR");
        
        }

        public bool IsFonteAtuadorPage()
        {
            return DescriptionPage.Nomenclatura.Value.Contains("FDC");
        }

        public bool IsAtuadorPage()
        {
            return DescriptionPage.Nomenclatura.Value.Contains("AT");
        }

        public void SetPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
        }

        public void SetPanel(string panel)
        {
            Panel = panel;
        }

        public string GetNomenclatura()
        {
            return DescriptionPage.Nomenclatura.Value;
        }

        public int GetNumeroDeAcionamentos()
        {
            return PageData.Data.Count;
        }
        public int GetNumeroDeReconhecimentos()
        {
            return PageData.Data.Count;
        }

    }
}
