using System;
using fsharp.Files;
using fsharp.Versioning;

namespace fsharp.Files
{
	public interface IResolveFileTypes
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		IFile Resolve(string fullPath);
	}
}

