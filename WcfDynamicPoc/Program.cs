using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace WcfDynamicPoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assembly = GenerateAssemblyFromWsdl("https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente?wsdl");

            var foundType = assembly.GetTypes().FirstOrDefault(type1 => type1.BaseType == typeof(SoapHttpClientProtocol));

            dynamic atendeCliente = Activator.CreateInstance(foundType);

            var consultaCep = atendeCliente.consultaCEP("60020270");
            Console.WriteLine(consultaCep);
        }

        private static Assembly GenerateAssemblyFromWsdl(string wsdlUrl)
        {
            var importer = new ServiceDescriptionImporter();

            using (var webClient = new WebClient())
            {
                var wsdl = webClient.DownloadString(wsdlUrl);
                var serviceDescription = ServiceDescription.Read(new StringReader(wsdl));
                importer.AddServiceDescription(serviceDescription, null, null);
            }
            importer.Style = ServiceDescriptionImportStyle.Client;

            importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

            var @namespace = new CodeNamespace();
            var compilationUnit = new CodeCompileUnit();
            compilationUnit.Namespaces.Add(@namespace);
            importer.Import(@namespace, compilationUnit);
            var cSharpProvider = CodeDomProvider.CreateProvider("C#");

            var parms = new CompilerParameters(new[]
                {"System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll"})
            {
                GenerateInMemory = true
            };
            var results = cSharpProvider.CompileAssemblyFromDom(parms, compilationUnit);
            var assembly = results.CompiledAssembly;
            if (results.Errors.Count > 0)
            {
            }
            return assembly;
        }
    }
}