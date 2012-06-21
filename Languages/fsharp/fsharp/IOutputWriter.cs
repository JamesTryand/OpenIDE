using System;
using fsharp.Projects;
using fsharp.Crawlers;

namespace fsharp
{
	public interface IOutputWriter
	{
		void AddProject(Project project);
		void AddFile(string file);
		void AddNamespace(Namespace ns);
		void AddClass(Class cls);
		void AddInterface(Interface iface);
		void AddStruct(Struct str);
		void AddEnum(EnumType enu);
		void Error(string description);
	}

	class OutputWriter : IOutputWriter
	{
		public void AddProject(Project project)
		{
			Console.WriteLine("project|" + project.File + "|filesearch");
		}

		public void AddFile(string file)
		{
			Console.WriteLine("file|" + file + "|filesearch");
		}
		
		public void AddNamespace(Namespace ns)
		{
			writeSignature("namespace", ns);
		}

		public void AddClass(Class cls)
		{
			writeSignature("class", cls, new[] { "typesearch" });
		}

		public void AddInterface(Interface iface)
		{
			writeSignature("interface", iface, new[] { "typesearch" });
		}

		public void AddStruct(Struct str)
		{
			writeSignature("struct", str, new[] { "typesearch" });
		}

		public void AddEnum(EnumType enu)
		{
			writeSignature("enum", enu, new[] { "typesearch" });
		}

		public void Error(string description)
		{
			Console.WriteLine("error|" + description);
		}

		private void writeSignature(string type, ICodeReference coderef)
		{
			writeSignature(type, coderef, new string[] {});
		}

		private void writeSignature(string type, ICodeReference coderef, string[] additional)
		{
			var additionalArguments = "";
			foreach (var argument in additional)
				additionalArguments += "|" + argument;
			Console.WriteLine("signature|{0}|{1}|{2}|{3}|{4}|{5}{6}",
				coderef.Signature,
				coderef.Name,
				type,
				coderef.Offset,
				coderef.Line,
				coderef.Column,
				additionalArguments);
		}
	}
}
