using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using fsharp.FileSystem;
using System.Reflection;
using System.Threading;
using fsharp.Crawlers;

namespace fsharp.Tests.Crawlers
{
	[TestFixture]
	public class fsharpCrawlerTests
	{
		[Test]
		public void Should_crawl_this_project()
		{
			var cache = new Fake_CacheBuilder();
			new fsharpCrawler(cache)
				.Crawl(new CrawlOptions(new PathParser(string.Format("..{0}..{0}", Path.DirectorySeparatorChar)).ToAbsolute(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
			Thread.Sleep(1500);
			Assert.That(cache.Classes.Count, Is.GreaterThan(0));
			Assert.That(cache.Namespaces[0].Name, Is.EqualTo("fsharp.Tests.Crawlers"));
			Assert.That(cache.Namespaces[0].Line, Is.EqualTo(10));
			Assert.That(cache.Namespaces[0].Column, Is.EqualTo(11));
			Assert.That(cache.Classes[0].Name, Is.EqualTo("fsharpCommentParserTests"));
			Assert.That(cache.Classes[0].Signature, Is.EqualTo("fsharp.Tests.Crawlers.fsharpCommentParserTests"));
			Assert.That(cache.Classes[0].Line, Is.EqualTo(13));
			Assert.That(cache.Classes[0].Column, Is.EqualTo(15));
			
			Assert.That(cache.Classes.Exists(x => x.Name.Equals("Fake_CacheBuilder")), Is.True);
		}

		private void xPlatformAssert(int column, int expectedNix, int expectedWin)
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
				Assert.That(column, Is.EqualTo(expectedNix));
			else
				Assert.That(column, Is.EqualTo(expectedWin));
		}
	}
}

