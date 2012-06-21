using System;
using NUnit.Framework;
using fsharp.Files;
namespace fsharp.Tests.Files
{
	[TestFixture]
	public class ComplieFileTests
	{
		[Test]
		public void Should_know_fsharp_file_as_known_file_type()
		{
			Assert.That(new CompileFile().SupportsExtension(".cs"), Is.True);
		}
	}
}

