using System;
using fsharp.Projects;
using fsharp.Projects.Readers;
using fsharp.Projects.Appenders;
using fsharp.Projects.Writers;
using fsharp.Files;
using fsharp.Projects.Removers;

namespace fsharp.Versioning
{
	public interface IProvideVersionedTypes
	{
		IReadProjectsFor Reader();
		IResolveFileTypes FileTypeResolver();
		IAppendFiles FileAppenderFor(IFile file);
		IRemoveFiles FileRemoverFor(IFile file);
		IWriteProjectFileToDiskFor Writer();
		IAddReference ReferencerFor(IFile file);
		IRemoveReference DereferencerFor(IFile file);
	}
}
