using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PegasusTests.PageHelper.Comm;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium;
using System.IO;

namespace Pegasus_Admin.Scripts
{

 //   [TestClass]

    public class DeleteExecutionResultFile
    {
        private static bool _isFileDeletedFirst;
        private IWebDriver Browser;

        [TestInitialize]
        [TestMethod]
        [TestCategory("DeleteExeResult")]
        [TestCategory("TS6")]
        public void DeleteFile()
        {
            if (_isFileDeletedFirst) return;

            string filePath = "Execution_Result_File.csv";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _isFileDeletedFirst = true;
        }
    }
}
