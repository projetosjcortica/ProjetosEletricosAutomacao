using Domain.Value_Objects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FilePathTest
    {
        [Fact]
        public void Sucess()
        {
            var FilePath = new FilePath(@"C:/Usuarios/Maquina/Ola Mundo.txt");

            FilePath.ToString().Should().Be("C:/Usuarios/Maquina/Ola Mundo.txt");
            FilePath.GetFileName().Should().Be("Ola Mundo.txt");
        }

        [Fact]
        public void FilePathWithBackslash()
        {
            var FilePath = new FilePath(@"C:\Usuarios\Maquina\Ola Mundo.txt");

            FilePath.ToString().Should().Be("C:/Usuarios/Maquina/Ola Mundo.txt");
            FilePath.GetFileName().Should().Be("Ola Mundo.txt");
        }
        [Fact]
        public void ErrorNotAbsolutePath()
        {
            Action act = () => new FilePath(@"\Usuarios\Maquina\Ola Mundo.txt");

            var exception = Assert.Throws<ArgumentException>(act);

            exception.Message.Should().Contain("O caminho deve ser absoluto");
        }
    }
}
