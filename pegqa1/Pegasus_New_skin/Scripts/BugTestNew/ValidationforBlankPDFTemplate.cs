using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationforBlankPDFTemplate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void validationforBlankPDFTemplate()
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
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());

            // Random Variables
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ValidationforBlankPDFTemplate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ValidationforBlankPDFTemplate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ValidationforBlankPDFTemplate", "Redirected at Pdf Templates page.");
                VisitOffice("pdf_templates");

                executionLog.Log("ValidationforBlankPDFTemplate", "Verify Page title");
                VerifyTitle("PDF Templates");

                executionLog.Log("ValidationforBlankPDFTemplate", "Click on edit icon");
                pDFTemplate_PDFTemplateHelper.ClickElement("EditFirstTemplate");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(1000);

                executionLog.Log("ValidationforBlankPDFTemplate", "Clear Pdf Name field.");
                pDFTemplate_PDFTemplateHelper.ClearTextBoxValue("//*[@id='PdfTemplateName']");

                executionLog.Log("ValidationforBlankPDFTemplate", "Click on save button.");
                pDFTemplate_PDFTemplateHelper.ClickElement("SavebuttonEDit");

                executionLog.Log("ValidationforBlankPDFTemplate", "Verify Validation for mandatory field.");
                pDFTemplate_PDFTemplateHelper.VerifyText("NameError", "This field is required.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ValidationforBlankPDFTemplate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Validation for Blank PDF Template");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Validation for Blank PDF Template", "Bug", "Medium", "PDF Templates page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Validation for Blank PDF Template");
                        TakeScreenshot("ValidationforBlankPDFTemplate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationforBlankPDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ValidationforBlankPDFTemplate");
                        string id = loginHelper.getIssueID("Validation for Blank PDF Template");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationforBlankPDFTemplate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Validation for Blank PDF Template"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Validation for Blank PDF Template");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ValidationforBlankPDFTemplate");
                executionLog.WriteInExcel("Validation for Blank PDF Template", Status, JIRA, "Office PDF Templates");
            }
        }
    }
}