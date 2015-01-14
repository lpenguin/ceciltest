using System;
using Mono.Cecil;

namespace ceciltest
{
	class MainClass
	{
		private static void log(string message){
			Console.WriteLine (message);
		}

		private void deleteRef(string[] args){
			log ("Delete");
			var ass = getAssembly(args[1]);
			var assName = args [2];
			var outName = args [3];
			AssemblyNameReference targetRef = null;
			foreach (var refer in ass.MainModule.AssemblyReferences) {
				if (refer.Name == assName) {
					targetRef = refer;
				}
			}
			log ("Found ref: " + targetRef.FullName);
			ass.MainModule.AssemblyReferences.Remove (targetRef);
			ass.Write (outName);
		}

		private void addRef(string[] args){
			log ("Add ref");
			var ass = getAssembly (args [1]);
			var assName = args [2];
			var assVersion = args [3];
			var outName = args [4];
			Version version = new Version (assVersion);
			AssemblyNameReference assRef = new AssemblyNameReference (assName, version);
			ass.MainModule.AssemblyReferences.Add (assRef);
			log ("Added ref: " + assRef.FullName);
			ass.Write (outName);
		}

		private void listRef(string[] args){
			var ass = getAssembly (args [1]);

			foreach (var refer in ass.MainModule.AssemblyReferences) {
				log (refer.FullName);
			}
		}

		private AssemblyDefinition getAssembly(string fileName){
			var ass = AssemblyDefinition.ReadAssembly (fileName);
			return ass;
		}

		public static void Main (string[] args)
		{
			var app = new MainClass();
			if (args.Length == 0) {
				log ("Error: empty args");
				return;
			}

			var action = args [0];
			switch (action) {
			case "delete":
				app.deleteRef (args);
				break;
			case "add":
				app.addRef (args);
				break;
			case "list":
				app.listRef (args);
				break;
			}

//			//var fileName = args [0];
//			var fileName = "/home/nikita/downloads/Underrail Alpha Demo/underrail.exe";
//			Console.WriteLine ("Reading: " + fileName);
//			var ass = AssemblyDefinition.ReadAssembly (fileName);
//			var module = ass.MainModule;
//			//module.AssemblyReferences.Remove
//
//			AssemblyNameReference targetRef = null;
//			foreach (var refer in ass.MainModule.AssemblyReferences) {
//				Console.WriteLine (refer.Name);
//				if (refer.Name == "Microsoft.Xna.Framework.Game") {
//					targetRef = refer;
//				}
//			}
//			module.AssemblyReferences.Remove (targetRef);
//			foreach (var refer in ass.MainModule.AssemblyReferences) {
//				Console.WriteLine (refer.Name);
//
//			}
//			ass.Write(
//			Console.WriteLine (targetRef);

		}


	}
}
