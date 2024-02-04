using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using KbgSoft.LineCounter;
using KbgSoft.LineCounter.Strategies;
using NUnit.Framework;


namespace LineCounter.Tests
{
    public class ReadMeHelper
    {
        [Test]
        public void MutateReadme()
        {
            var basePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..","..","..");
			var linecounter = new LineCounting();
			linecounter.ReplaceWebshieldsInFile(basePath, Path.Combine(basePath, "README.md"));
        }

        [Test]
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

            var stat1 = new CSharpStrategy().Count(new MultiLineCommentFilterStream().ReadLines(new StringReader(version1)));
            var stat2 = new CSharpStrategy().Count(new MultiLineCommentFilterStream().ReadLines(new StringReader(version2)));

            Assert.AreEqual(8, stat1.CodeLines);
            Assert.AreEqual(8, stat2.CodeLines);
        }
    }
}
