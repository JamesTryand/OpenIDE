using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreExtensions;

namespace OpenIDE.Core.RScripts
{
	public class ReactiveScript
	{
		private string _file;
		private string _keyPath;
		private List<string> _events = new List<string>();	

		public string Name { get { return Path.GetFileNameWithoutExtension(_file); } }
		public string File { get { return _file; } }

		public ReactiveScript(string file, string keyPath)
		{
			_file = file;
			_keyPath = keyPath;
			getEvents();
		}

		public bool ReactsTo(string @event)
		{
			foreach (var reactEvent in _events)
				if (wildcardmatch(@event, reactEvent))
					return true;
			return false;
		}

		public void Run(string message)
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix &&
				Environment.OSVersion.Platform != PlatformID.MacOSX)
			{
				message = message
						  	.Replace("\"", "^\"")
							.Replace(" ", "^ ")
							.Replace("|", "^|")
							.Replace("%", "^&")
							.Replace("&", "^&")
							.Replace("<", "^<")
							.Replace(">", "^>");
			}
			message = "\"" + message + "\"";
			var process = new Process();
			process.Run(_file, message, false, _keyPath);
		}

		private void getEvents()
		{
			_events.Clear();
			_events.AddRange(
				new Process()
					.Query(_file, "reactive-script-reacts-to", false, _keyPath)
					.Where(x => x.Length > 0)
					.Select(x => x.Trim(new[] {'\"'})));
		}
		
		private bool wildcardmatch(string str, string pattern)
		{
			var rgx = new Regex(
				"^" + Regex.Escape(pattern)
					.Replace( "\\*", ".*" )
					.Replace( "\\?", "." ) + "$");
			return rgx.IsMatch(str);
		}
	}
}
