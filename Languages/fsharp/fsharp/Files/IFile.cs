using System;
namespace fsharp.Files
{
	public interface IFile
	{
		string Fullpath { get; }
		IFile New(string fullPath);
		bool SupportsExtension(string extension);
	}
}

