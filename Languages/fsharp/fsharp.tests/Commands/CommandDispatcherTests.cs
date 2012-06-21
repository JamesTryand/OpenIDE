using System;
using NUnit.Framework;
using fsharp.Commands;
using Rhino.Mocks;
namespace fsharp.Tests.Commands
{
	[TestFixture]
	public class CommandDispatcherTests
	{
		private FakeHandler _handler;
		private Dispatcher _execute;
		
		[SetUp]
		public void Setup()
		{
			_handler = new FakeHandler();
			_execute = new Dispatcher();
			_execute.Register(_handler);
		}
		
		[Test]
		public void Should_find_named_handler()
		{
			_execute.GetHandler("MyCommand").Execute(new string[] {});
			_handler.WasCalled();
		}
		
		[Test]
		public void When_no_matching_handlers_it_should_return_null()
		{
			Assert.That(_execute.GetHandler("MyOtherCommand"),  Is.Null);
			_handler.WasNotCalled();
		}
	}
	
	class FakeHandler : ICommandHandler
	{
		private bool _wasExecuted = false;
		
		public string Usage {
			get {
				return null;
			}
		}

		public string Command { get { return "MyCommand"; } }
		
		public void Execute(string[] arguments)
		{
			_wasExecuted = true;
		}
		
		public void WasCalled()
		{
			Assert.That(_wasExecuted, Is.True);
		}
		
		public void WasNotCalled()
		{
			Assert.That(_wasExecuted, Is.False);
		}
	}
}

