using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TeamBinary.LineCounter;

namespace LineCounter.Tests
{
    public class ReadMeHelper
    {
        [Test]
        public void MutateReadme()
        {
            var basePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..","..");

            var stats = new DirWalker().DoWork(Path.Combine(basePath, "LineCounter"));

            var shieldsRegEx = new Regex("<!--start-->.*<!--end-->", RegexOptions.Singleline);
            var githubShields = new WebFormatter().CreateGithubShields(stats);

            var readmePath = Path.Combine(basePath, "README.md");
            var oldReadme = File.ReadAllText(readmePath);
            var newReadMe = shieldsRegEx.Replace(oldReadme, "<!--start-->" + githubShields + "<!--end-->");

            if (oldReadme != newReadMe)
                File.WriteAllText(readmePath, newReadMe);
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

            var stat1 = new CSharpStrategy().Count(version1.Split('\n'));
            var stat2 = new CSharpStrategy().Count(version2.Split('\n'));

            Assert.AreEqual(8, stat1.CodeLines);
            Assert.AreEqual(8, stat2.CodeLines);
        }
    }
}
