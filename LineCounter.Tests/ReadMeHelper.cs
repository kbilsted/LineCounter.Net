using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using KbgSoft.LineCounter;
using KbgSoft.LineCounter.Strategies;
using Xunit;

namespace LineCounter.Tests
{
    public class ReadMeHelper
    {
        [Fact]
        public void MutateReadme()
        {
            var basePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..","..","..");

            var stats = new LineCounting().CountFolder(Path.Combine(basePath, "LineCounter"));

            var shieldsRegEx = new Regex("<!--start-->.*<!--end-->", RegexOptions.Singleline);
            var githubShields = new WebFormatter().CreateGithubShields(stats.Total);

            var readmePath = Path.Combine(basePath, "README.md");
            var oldReadme = File.ReadAllText(readmePath);
            var newReadMe = shieldsRegEx.Replace(oldReadme, $"<!--start-->\r\n{githubShields}\r\n<!--end-->");

            if (oldReadme != newReadMe)
                File.WriteAllText(readmePath, newReadMe);
        }

        [Fact]
        public void CountExample()
        {
            string version1 = @"
    class Foo {
        public void Bar() {
            if(moons == 9) {
                 Planet = Pluto;
            } else {
                 Planet = Mars;
            }
        }
        public string Another() {
            Console.WriteLine("" * "")
        }
    }
";
            string version2 = @"    
class Foo 
{
    public void Bar() 
    {
        if(moons == 9) 
        {
            Planet = Pluto;
        } 
        else 
        {
            Planet = Mars;
        }
    }
        
    public string Another() 
    {
        Console.WriteLine("" * "")
    }
}
    ";

            var stat1 = new CSharpStrategy().Count(version1.Split('\n'));
            var stat2 = new CSharpStrategy().Count(version2.Split('\n'));

            Assert.Equal(8, stat1.CodeLines);
            Assert.Equal(8, stat2.CodeLines);
        }
    }
}
