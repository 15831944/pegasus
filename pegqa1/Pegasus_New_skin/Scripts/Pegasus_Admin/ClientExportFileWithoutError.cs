using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientExportFileWithoutError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void clientExportFileWithoutError()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_clientHelper = new Office_ClientsHelper(GetWebDriver());


            // Random Variable
            String JIRA = "";
            String Status = "Pass";
            var Proname = "Product" + GetRandomNumber();

            try
            {

                executionLog.Log("ClientExportFileWithoutError", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientExportFileWithoutError", " Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientExportFileWithoutError", " Redirect to client page");
                VisitOffice("clients");
                office_clientHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientExportFileWithoutError", " Verify Title of the page");
                VerifyTitle();

                executionLog.Log("ClientExportFileWithoutError", " Export as CSV");
                office_clientHelper.ExportAs("CSV");
                office_clientHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientExportFileWithoutError", " Verify No error displayed");
                office_clientHelper.VerifyTextNotPresent("Some issues occured in this operation, Contact technical support for help");

                executionLog.Log("ClientExportFileWithoutError", " Export as Excel");
                office_clientHelper.ExportAs("Excel");
                office_clientHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientExportFileWithoutError", " Verify No error displayed");
                office_clientHelper.VerifyTextNotPresent("Some issues occured in this operation, Contact technical support for help");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientExportFileWithoutError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Export File Without Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Export File Without Error", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Export File Without Error");
                        TakeScreenshot("ClientExportFileWithoutError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientExportFileWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientExportFileWithoutError");
                        string id = loginHelper.getIssueID("Client Export File Without Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientExportFileWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Export File Without Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Export File Without Error");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ClientExportFileWithoutError");
                executionLog.WriteInExcel("Client Export File Without Error", Status, JIRA, "Client Management");
            }
        }
    }
}