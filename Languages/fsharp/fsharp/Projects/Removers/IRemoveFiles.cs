using System;
using fsharp.Versioning;
using fsharp.Files;
using fsharp.Projects;

namespace fsharp.Projects.Removers
{
	public interface IRemoveFiles
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		bool SupportsFile(IFile file);
		void Remove(Project project, IFile file);
	}
}

