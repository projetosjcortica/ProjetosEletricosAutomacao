using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Factories
{
    public static class FactoryPage
    {
        public static Page CreatePage(DescriptionPage descriptionPage, PageData pageData)
        {
            var Page = new Page(1, descriptionPage, pageData);
            return Page;
        }
    }
}
