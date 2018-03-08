using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TypoOfficeDetailsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Admin")]
        [TestCategory("All")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void typoOfficeDetailsIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String filename = GetPathToFile() + "AZURA Bill of Sale.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TypoOfficeDetailsIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TypoOfficeDetailsIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TypoOfficeDetailsIssue", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("TypoOfficeDetailsIssue", "Redirect at pdf templates page.");
                VisitOffice("pdf_templates");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("TypoOfficeDetailsIssue", "Search uploaded pdf by name");
                pDFTemplate_ImportWizardHelper.TypeText("SearchName", "AZURA Bill of Sale");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("TypoOfficeDetailsIssue", "Click on edit mapping icon.");
                pDFTemplate_ImportWizardHelper.ClickElement("EditPDFMapping");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("TypoOfficeDetailsIssue", "Wait for locator to be present.");
                pDFTemplate_ImportWizardHelper.WaitForElementPresent("MapWithRule", 10);

                executionLog.Log("TypoOfficeDetailsIssue", "Select mapping method as rule set.");
                pDFTemplate_ImportWizardHelper.ClickElement("MapWithRule");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("TypoOfficeDetailsIssue", "Search pdf field vendor phone number.");
                pDFTemplate_ImportWizardHelper.TypeText("SearchPDFField", "Vender Phone Number");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

                executionLog.Log("TypoOfficeDetailsIssue", "Select rule type as office details.");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectRuleType", "Office Details");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("TypoOfficeDetailsIssue", "Text to be verified under the rule.");
                pDFTemplate_ImportWizardHelper.TypoError("Toll Free Phone Number");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(5000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TypoOfficeDetailsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Typo Office Details Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Typo Office Details Issue", "Bug", "Medium", "Pdf Import page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Typo Office Details Issue");
                        TakeScreenshot("TypoOfficeDetailsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TypoOfficeDetailsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TypoOfficeDetailsIssue");
                        string id = loginHelper.getIssueID("Typo Office Details Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TypoOfficeDetailsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Typo Office Details Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Typo Office Details Issue");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TypoOfficeDetailsIssue");
                executionLog.WriteInExcel("Typo Office Details Issue", Status, JIRA, "PDF Import");
            }
        }
    }
}