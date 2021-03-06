using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using OpenIDE.Core.CommandBuilding;

namespace OpenIDE.Core.Language
{
	public class LanguagePlugin
	{
		private string _path;
		private Func<string, string, IEnumerable<string>> _execute;
		private Action<string> _dispatch;

		public LanguagePlugin(
			string path,
			Func<string, string, IEnumerable<string>> execute,
			Action<string> dispatch)
		{
			_path = path;
			_execute = execute;
			_dispatch = dispatch;
		}

		public string GetLanguage()
		{
			return Path.GetFileNameWithoutExtension(_path);
		}

		public IEnumerable<BaseCommandHandlerParameter> GetUsages()
		{
			return getUsages();
		}

		public string GetCrawlFileTypes()
		{
			return ToSingleLine("crawl-file-types");
		}

		public IEnumerable<string> Crawl(IEnumerable<string> filesAndFolders)
		{
			var file = Path.GetTempFileName();
			File.WriteAllLines(file, filesAndFolders.ToArray());
			foreach (var line in run(string.Format("crawl-source \"{0}\"", file)))
				yield return line;
			File.Delete(file);
		}

		public SignatureLocation SignatureFromPosition(FilePosition position)
		{
			try {
				var lines = run(
					string.Format(
						"signature-from-position \"{0}\"",
						position.ToCommand())).ToArray();
				if (lines.Length != 4)
					return null;
				return new SignatureLocation(
					lines[0],
					lines[1],
					new Position(lines[2]),
					new Position(lines[3]));
			} catch {
				return null;
			}
		}

		public string[] RetrieveMembersFromSignature(string signature)
		{
			return run("members-from-signature \"" + signature + "\"").ToArray();
		}

		public void Run(string[] arguments)
		{
			var sb = new StringBuilder();
			arguments.ToList()
				.ForEach(x => sb.Append(" \"" + x + "\""));
			foreach (var line in run(sb.ToString()))
				_dispatch(line);
		}
		
		private IEnumerable<BaseCommandHandlerParameter> getUsages()
		{
			var commands = new List<BaseCommandHandlerParameter>();
			var usage = getUsage();
			new UsageParser(usage)
				.Parse().ToList()
					.ForEach(y =>
						{
							var cmd = new BaseCommandHandlerParameter(
								y.Name,
								y.Description,
								CommandType.FileCommand);
							y.Parameters.ToList()
								.ForEach(p => cmd.Add(p));
							if (!y.Required)
								cmd.IsOptional();
							commands.Add(cmd);
						});
			return commands;
		}
		
		private string getUsage()
		{
			return ToSingleLine("get-command-definitions");
		}

		private string ToSingleLine(string arguments)
		{
			var sb = new StringBuilder();
			run(arguments).ToList()
				.ForEach(x => 
					{
						sb.Append(x);
					});
			return sb.ToString();
		}

		private IEnumerable<string> run(string arguments)
		{
			return _execute(_path, arguments);
		}
	}

	public class SignatureLocation
	{
		public string File { get; private set; }
		public string Signature { get; private set; }
		public Position Start { get; private set; }
		public Position End { get; private set; }

		public SignatureLocation(string file, string signature, Position start, Position end)
		{
			File = file;
			Signature = signature;
			Start = start;
			End = end;
		}
	}
}
