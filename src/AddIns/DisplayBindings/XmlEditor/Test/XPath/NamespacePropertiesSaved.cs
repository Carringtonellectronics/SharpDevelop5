﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.Linq;
using ICSharpCode.Core;
using ICSharpCode.XmlEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace XmlEditor.Tests.XPath
{
	[TestFixture]
	public class NamespacePropertiesSaved
	{
		List<XmlNamespace> expectedNamespaces;
		string[] actualNamespaces;
		
		[SetUp]
		public void SetUpFixture()
		{
			using (XPathQueryControl queryControl = new XPathQueryControl()) {
				expectedNamespaces = new List<XmlNamespace>();
				expectedNamespaces.Add(new XmlNamespace("f", "http://foo.com"));
				expectedNamespaces.Add(new XmlNamespace("s", "http://sharpdevelop.com"));
				
				foreach (XmlNamespace xmlNamespace in expectedNamespaces) {
					queryControl.AddNamespace(xmlNamespace.Prefix, xmlNamespace.Name);
				}
				// Blank prefix and uris should be ignored.
				queryControl.AddNamespace(String.Empty, String.Empty);
				
				// Null cell values ignored.
				queryControl.NamespacesDataGridView.Rows.Add(new object[] {null, null});

				Properties p = queryControl.CreateMemento();
				actualNamespaces = p.GetList<string>("Namespaces").ToArray();
			}
		}
		
		[Test]
		public void TwoNamespacesAdded()
		{
			Assert.AreEqual(2, actualNamespaces.Length);
		}
		
		[Test]
		public void NamespacesAdded()
		{
			for (int i = 0; i < expectedNamespaces.Count; ++i) {
				XmlNamespace expectedNamespace = expectedNamespaces[i];
				string actualNamespace = actualNamespaces[i];
				Assert.AreEqual(expectedNamespace.ToString(), actualNamespace);
			}
		}
	}
}
