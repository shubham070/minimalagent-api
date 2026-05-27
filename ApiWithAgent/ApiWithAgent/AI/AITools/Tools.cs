using System.Reflection;

namespace ApiWithAgent.AI.AITools
{
	public class Tools
	{
		public static string GetAuthor() => "Shubham Gaikwad";

		public static string FormatStory(string title, string author, string story) => $"Title : {title}   Author:{author}   Story:{story}";
	}
}
