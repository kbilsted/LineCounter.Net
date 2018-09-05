namespace KbgSoft.LineCounter {
	public class Statistics {
		public int CodeLines;
		public int TestCodeLines;
		public int DocumentationLines;
	    public int Files = 0;

	    public bool Any => CodeLines != 0 || DocumentationLines != 0;

        // example of linq usage
	    //public static Statistics operator -(Statistics s1, Statistics s2)
	    //{
	    //    return new Statistics()
	    //    {
	    //        CodeLines = s1.CodeLines - s2.CodeLines,
	    //        DocumentationLines = s1.DocumentationLines - s2.DocumentationLines,
	    //    };
	    //}
	}
}