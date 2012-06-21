using System;
using fsharp.Versioning;
using fsharp.Files;

namespace fsharp.Projects
{
	public interface IRemoveReference
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		bool SupportsFile(IFile file);
		void Dereference(Project project, IFile file);	
	}
}
