using System;
using fsharp.Versioning;

namespace fsharp.Projects.Readers
{
	public interface IReadProjectsFor
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		Project Read(string fullPath);
	}
}

