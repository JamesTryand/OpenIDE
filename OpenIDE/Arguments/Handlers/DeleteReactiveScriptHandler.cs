using System;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenIDE.Core.Language;
using OpenIDE.Core.RScripts;

namespace OpenIDE.Arguments.Handlers
{
	class DeleteReactiveScriptHandler : ICommandHandler
	{
		private PluginLocator _pluginLocator;

		public CommandHandlerParameter Usage {
			get {
					var usage = new CommandHandlerParameter(
						"All",
						CommandType.FileCommand,
						Command,
						"Deletes a reactive script");
					usage.Add("SCRIPT-NAME", "Reactive script name. Local are picked over global");
					return usage;
			}
		}
	
		public string Command { get { return "rscript-delete"; } }

		public DeleteReactiveScriptHandler(PluginLocator locator)
		{
			_pluginLocator = locator;
		}
	
		public void Execute(string[] arguments)
		{
			var scripts = new ReactiveScriptReader(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				() => { return _pluginLocator; })
				.Read(Environment.CurrentDirectory);
			var script = scripts.FirstOrDefault(x => x.Name.Equals(arguments[0]));
			if (script == null || arguments.Length < 1)
				return;
			File.Delete(script.File);
		}
	}
}
