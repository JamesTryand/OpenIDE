using System;
using fsharp.Versioning;
using fsharp.Files;
namespace fsharp.Projects.Appenders
{
	public interface IAppendFiles
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		bool SupportsFile(IFile file);
		void Append(Project project, IFile file);
	}
}

