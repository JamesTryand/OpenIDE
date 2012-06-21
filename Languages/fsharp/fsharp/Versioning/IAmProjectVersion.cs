using System;
namespace fsharp.Versioning
{
	public interface IAmProjectVersion
	{
		bool IsValid(string projecFile);
	}
}

