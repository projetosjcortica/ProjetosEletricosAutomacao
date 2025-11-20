namespace Domain.Value_Objects
{
    public class FilePath
    {
        private string _path;

        public FilePath(string path)
        {
            path = path.Replace(@"\", "/").Replace("\"", "");
            if (!path.StartsWith("C:/") && !path.StartsWith("//")) throw new ArgumentException("O caminho deve ser absoluto");

            _path = path;
        }

        public override string ToString()
        {
            return _path;
        }

        public string GetFileName()
        {
            var result = _path.Split('/');
            return result[result.Length - 1];
        }
    }
}
