using System;

namespace fsharp.Commands
{
	public interface ICommandHandler
	{
		string Usage { get; }
		string Command { get; }
		void Execute(string[] arguments);
	}
}

