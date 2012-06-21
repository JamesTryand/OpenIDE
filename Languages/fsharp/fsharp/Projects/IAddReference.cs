using System;
using fsharp.Versioning;
using fsharp.Files;

namespace fsharp.Projects
{
	public interface IAddReference
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		bool SupportsFile(IFile file);
		void Reference(Project project, IFile file);	
	}
}
