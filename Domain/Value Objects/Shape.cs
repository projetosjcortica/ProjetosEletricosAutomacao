using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class Shape
    {

        public bool IsDeletable { get; private set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Shape(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public void Delete()
        {
            IsDeletable = true;
        }
    }
}
