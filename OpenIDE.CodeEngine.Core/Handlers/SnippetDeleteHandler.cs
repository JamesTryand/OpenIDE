using System;
using System.IO;
using System.Reflection;
using OpenIDE.Core.Config;
using OpenIDE.CodeEngine.Core.Caching;
using OpenIDE.CodeEngine.Core.Commands;

namespace OpenIDE.CodeEngine.Core.Handlers
{
	class SnippetDeleteHandler : IHandler
	{
		private ICacheBuilder _cache;
		private string _keyPath;

		public SnippetDeleteHandler(ICacheBuilder cache, string keyPath)
		{
			_cache = cache;
			_keyPath = keyPath;
		}

		public bool Handles(CommandMessage message)
		{
			return message.Command.Equals("snippet-delete");
		}

		public void Handle(Guid clientID, CommandMessage message)
		{
			var arguments = message.Arguments.ToArray();
			if (arguments.Length < 2)
				return;
			var language = new PluginFinder(_cache).FindLanguage(arguments[0]);
			if (language == null)
				return;
			var file = getLocal(arguments);
			if (!File.Exists(file))
				file = getGlobal(arguments);

			if (!File.Exists(file))
				return;
			File.Delete(file);
		}

		private string getGlobal(string[] arguments)
		{
				return getPath(
					Path.GetDirectoryName(
						Path.GetDirectoryName(
							Assembly.GetExecutingAssembly().Location)), arguments);
		}

		private string getLocal(string[] arguments)
		{
				return getPath(Path.GetDirectoryName(
					Configuration.GetConfigFile(_keyPath)), arguments);
		}

		private string getPath(string path, string[] arguments)
		{
			return Path.Combine(
				path,
				Path.Combine(
					"Languages",
					Path.Combine(
						new PluginFinder(_cache).FindLanguage(arguments[0]),
						Path.Combine("snippets", arguments[1] + ".snippet"))));
		}
	}
}
