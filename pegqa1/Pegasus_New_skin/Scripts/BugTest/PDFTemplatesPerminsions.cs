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
    public class PDFTemplatesPerminsions : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void pDFTemplatesPerminsions()
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
            var Corp_PDFTemplate_PDFTemplateHelper = new CorpPDFTemplate_TemplateHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFTemplatesPerminsions", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFTemplatesPerminsions", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PDFTemplatesPerminsions", "Redirect Pdf Template");
                VisitOffice("pdf_templates");

                executionLog.Log("PDFTemplatesPerminsions", "Click on PDF");
                Corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickPDF");

                executionLog.Log("PDFTemplatesPerminsions", "Wait for element present.");
                Corp_PDFTemplate_PDFTemplateHelper.WaitForElementPresent("ClickOnPermisions", 30);

                executionLog.Log("PDFTemplatesPerminsions", "Click On Permisions");
                Corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickOnPermisions");
                Corp_PDFTemplate_PDFTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFTemplatesPerminsions", "Click On None Of These");
                Corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickOnNoneOfThese");

                executionLog.Log("PDFTemplatesPerminsions", "Click on Update");
                Corp_PDFTemplate_PDFTemplateHelper.ClickElement("ClickOnSaveBtnPDFTemp");
                Corp_PDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFTemplatesPerminsions", "Verify Text");
                Corp_PDFTemplate_PDFTemplateHelper.VerifyPageText("Pdf Permissions Updated Successfully.");

            }    
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFTemplatesPerminsions");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Templates Perminsions");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Templates Perminsions", "Bug", "Medium", "PDF Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Templates Perminsions");
                        TakeScreenshot("PDFTemplatesPerminsions");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFTemplatesPerminsions.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFTemplatesPerminsions");
                        string id = loginHelper.getIssueID("PDF Templates Perminsions");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFTemplatesPerminsions.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Templates Perminsions"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Templates Perminsions");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFTemplatesPerminsions");
                executionLog.WriteInExcel("PDF Templates Perminsions", Status, JIRA, "Corp PDF Templates");
            }
        }
    }
}

