using System;
namespace fsharp.Versioning
{
	public interface IResolveProjectVersion
	{
		IProvideVersionedTypes ResolveFor(string fullPath);
	}
}

