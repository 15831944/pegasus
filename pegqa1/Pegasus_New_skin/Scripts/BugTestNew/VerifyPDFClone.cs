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
    public class VerifyPDFClone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyPDFClone()
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
            var pdfTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());

            // Variable 
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyPDFClone", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPDFClone", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyPDFClone", "Redirect to All PDF Templates page.");
                VisitOffice("pdf_templates");
                pdfTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPDFClone", "Open a pdf");
                pdfTemplate_PDFTemplateHelper.ClickElement("PDF1");
                pdfTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPDFClone", "Click on Clone button");
                pdfTemplate_PDFTemplateHelper.ClickElement("Clone");
                pdfTemplate_PDFTemplateHelper.AcceptAlert();
                pdfTemplate_PDFTemplateHelper.WaitForText("PDF Template is cloned successfully", 05);

                executionLog.Log("VerifyPDFClone", "Click on Delete button");
                pdfTemplate_PDFTemplateHelper.ClickElement("Delete");
                pdfTemplate_PDFTemplateHelper.AcceptAlert();
                pdfTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPDFClone");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify PDF Clone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify PDF Clone", "Bug", "Medium", "PDF Templates", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify PDF Clone");
                        TakeScreenshot("VerifyPDFClone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPDFClone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPDFClone");
                        string id = loginHelper.getIssueID("Verify PDF Clone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPDFClone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify PDF Clone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify PDF Clone");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPDFClone");
                executionLog.WriteInExcel("Verify PDF Clone", Status, JIRA, "PDF Templates");
            }
        }
    }
}
