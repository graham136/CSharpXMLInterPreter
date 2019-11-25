using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlFileName = "Brazil.xml";            
            XDocument doc;            

            IEnumerable<XElement> xmlElements;

            // Xml formatting errors

            try
            {
                doc = XDocument.Load(xmlFileName);
                xmlElements = from el in doc.Elements() select el;

            }
            catch (Exception xe)
            {
                Console.WriteLine(xe.Message);
                return;
            }


            // Defined errors

            // Check first for country
            if (!xmlElements.Any())
            {
                Console.WriteLine("No country exists");
                return;
            }

            if (!xmlElements.Attributes("name").Any())
            {
                Console.WriteLine("No name for country");
            }

            xmlElements = xmlElements.Descendants();

            // Check second for process
            if (!xmlElements.Any())
            {
                Console.WriteLine("No process exists");
                return;
            }

            if (!xmlElements.Attributes("name").Any())
            {
                Console.WriteLine("No name for process");
                return;
            }


            xmlElements = xmlElements.Descendants();

            foreach (var xmlElement in xmlElements)
            {
                var name = xmlElement.Attributes("name");
                if (!name.Any())
                {
                    Console.WriteLine("{0} does not have a name or identifier", xmlElement.Name);
                    return;
                }
            }
          
            IEnumerable<XElement> countries = from el in doc.Elements() select el;

            foreach (var country in countries)
            {
                Console.WriteLine("Starting Country {0} ", country.Attribute("name"));
                IEnumerable<XElement> processes = countries.Descendants("Process");

                foreach (var process in processes)
                {
                    Console.WriteLine("Starting Process {0}: Type: {1} ", process.Attribute("name"), process.Name);
                    Console.WriteLine();
                    IEnumerable<XElement> operations = process.Descendants();

                    foreach (var operation in operations)
                    {
                        Console.WriteLine("Starting operation {0}: Type: {1} ", operation.Attribute("name"), operation.Name);

                        switch (operation.Name.ToString())
                        {
                            case "AssignmentFromMethod":
                                AssignmentFromMethod(operation);
                                break;
                            case "AssignmentFromMethodWithParameters":
                                AssignmentFromMethodWithParameters(operation);
                                break;
                            case "AssignmentValue":
                                AssignmentValue(operation);
                                break;
                            case "AssignmentVariable":
                                AssignmentVariable(operation);
                                break;                            
                            case "Condition":
                                Condition(operation);
                                break;
                            case "ConditionalAssignmentVariable":
                                ConditionalAssignmentVariable(operation);
                                break;
                            case "ConditionalAssignmentFromMethodWithParameters":
                                ConditionalAssignmentFromMethodWithParameters(operation);
                                break;
                            case "Formula":
                                Formula(operation);
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine();

                    }

                }


            }
            countries = doc.Root.Elements("Country");
        }


        static void Condition(XElement element)
        {
            var requiredAttributes = new[] { "LeftVariable", "RightVariable", "Operator", "ReturnVariable", "Operator" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }
            
            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Evaluating if {0} {1} {2} as types {3} ",
                element.Attribute("LeftVariable").Value,
                element.Attribute("Operator").Value,
                element.Attribute("RightVariable").Value,
                element.Attribute("Types").Value);
        }

        static void ConditionalAssignmentVariable(XElement element)
        {
            var requiredAttributes = new[] { "ReturnVariable", "ReturnType", "AssignVariable", "ConditionVariable" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Evaluating Condition ({0}) {1}  = {2} as types {3} ",
                element.Attribute("ConditionVariable").Value,
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignVariable").Value,
                element.Attribute("ReturnType").Value);
        }

        static void AssignmentValue(XElement element)
        {
            var requiredAttributes = new[] { "ReturnVariable", "AssignValue" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Assigning Value {0}  = {1} of type {2}",
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignValue").Value,
                element.Attribute("ReturnType").Value);

        }

        static void AssignmentVariable(XElement element)
        {
            var requiredAttributes = new[] { "ReturnVariable", "AssignVariable","ReturnType" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Assigning Value {0}  = {1} of type {2}",
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignVariable").Value,
                element.Attribute("ReturnType").Value);

        }

        static void Formula(XElement element)
        {
            var requiredAttributes = new[] { "ReturnVariable", "FirstValue" , "SecondValue" , "Operation" , "ReturnType" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Formula {0}  = {1} {2} {3} of type {4}",
                element.Attribute("ReturnVariable").Value,
                element.Attribute("FirstValue").Value,
                element.Attribute("Operation").Value,
                element.Attribute("SecondValue").Value,
                element.Attribute("ReturnType").Value);

        }

        static void AssignmentFromMethod(XElement element)
        {

            var requiredAttributes = new[] { "ReturnVariable", "AssignMethod", "ReturnType" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("AssignmentFromMethod {0}  = {1}() of type {2}",
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignMethod").Value,                
                element.Attribute("ReturnType").Value);

        }

        static void AssignmentFromMethodWithParameters(XElement element)
        {

            var requiredAttributes = new[] { "ReturnVariable", "AssignMethod", "ReturnType", "ParameterValues" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }            

            Console.WriteLine("AssignmentFromMethodParameters {0}  = {1}{2} of type {3}",
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignMethod").Value,
                element.Attribute("ParameterValues").Value,
                element.Attribute("ReturnType").Value);

        }

        static void ConditionalAssignmentFromMethodWithParameters(XElement element)
        {

            var requiredAttributes = new[] { "ReturnVariable", "AssignMethod", "ReturnType", "ParameterValues", "ConditionVariable" };

            if (!element.Attributes("name").Any())
            {
                Console.WriteLine("{0} does not have a name or identifier", element.Name);
                return;
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!element.Attributes(requiredAttribute).Any())
                {
                    Console.WriteLine("Missing Attribute {0} in element {1}", requiredAttribute, element.Name);
                    return;
                }
            }

            Console.WriteLine("Conditional Assignment From Method with Parameters if({0}) {1}  = {2}{3} of type {4}",
                element.Attribute("ConditionVariable").Value,
                element.Attribute("ReturnVariable").Value,
                element.Attribute("AssignMethod").Value,
                element.Attribute("ParameterValues").Value,
                element.Attribute("ReturnType").Value);

        }
    }

}

