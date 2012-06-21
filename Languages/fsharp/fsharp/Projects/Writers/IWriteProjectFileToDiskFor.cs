using System;
using fsharp.Versioning;

namespace fsharp.Projects.Writers
{
	public interface IWriteProjectFileToDiskFor
	{
		bool SupportsProject<T>() where T : IAmProjectVersion;
		void Write(Project project);
	}
}

