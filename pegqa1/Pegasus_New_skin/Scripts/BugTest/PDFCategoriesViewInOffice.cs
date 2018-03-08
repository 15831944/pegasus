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
    public class PDFCategoriesViewInOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void pDFCategoriesViewInOffice()
        {
            string[] username = null;
            string[] password = null;

            string[] username1 = null;
            string[] password1 = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_PDFTemplate_PDFCategoriesHelper = new CorpPDFTemplate_CategoriesHelper(GetWebDriver());

            // Variable random
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("PDFCategoriesViewInOffice", "Enter User name");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as "+username[0]+" / "+password[0]);

            executionLog.Log("PDFCategoriesViewInOffice", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("PDFCategoriesViewInOffice", "Redirect  Template");
            VisitCorp("pdf_templates/categories");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(1000);

            executionLog.Log("PDFCategoriesViewInOffice", "Click on create button");
            corp_PDFTemplate_PDFCategoriesHelper.ClickElement("Create");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(2000);

            executionLog.Log("PDFCategoriesViewInOffice", "Enter Name");
            corp_PDFTemplate_PDFCategoriesHelper.TypeText("EnterName", name);

            executionLog.Log("PDFCategoriesViewInOffice", "Click on Save Button");
            corp_PDFTemplate_PDFCategoriesHelper.ClickDisplayed("//a[@title='Save']");
          
            executionLog.Log("PDFCategoriesViewInOffice", "Verify text present");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForText("Category Created Successfully" ,10);
        
            executionLog.Log("PDFCategoriesViewInOffice", "Redirect To logout");
            VisitCorp("logout");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

            executionLog.Log("PDFCategoriesViewInOffice", "Login using office credentials");
            Login(username1[0], password[0]);
      
            executionLog.Log("PartnerAgentWithoutUserSuccessConfirm", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PDFCategoriesViewInOffice", "Redirect To Admin");
            VisitOffice("admin");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(4000);

            executionLog.Log("PDFCategoriesViewInOffice", "Reirect to PDF Category");
            VisitOffice("pdf_templates/categories");
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

            executionLog.Log("PDFCategoriesViewInOffice", "Verify category on office page. ");
            corp_PDFTemplate_PDFCategoriesHelper.VerifyPageText(name);
            corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(4000);

           }
           catch (Exception e)
           {
               executionLog.Log("Error", e.StackTrace);
               Status = "Fail";

               String counter = executionLog.readLastLine("counter");
               String Description = executionLog.GetAllTextFile("PDFCategoriesViewInOffice");
               String Error = executionLog.GetAllTextFile("Error");
               Console.WriteLine(Error);
               if (counter == "")
               {
                   counter = "0";
               }
               bool result = loginHelper.CheckExstingIssue("PDF Categories View In Office");
               if (!result)
               {
                   if (Int16.Parse(counter) < 9)
                   {
                       executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                       loginHelper.CreateIssue("PDF Categories View In Office", "Bug", "Medium", "PDF Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                       string id = loginHelper.getIssueID("PDF Categories View In Office");
                       TakeScreenshot("PDFCategoriesViewInOffice");
                       string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                       var location = directoryName + "\\PDFCategoriesViewInOffice.png";
                       loginHelper.AddAttachment(location, id);
                   }
               }
               else
               {
                   if (Int16.Parse(counter) < 9)
                   {
                       executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                       TakeScreenshot("PDFCategoriesViewInOffice");
                       string id = loginHelper.getIssueID("PDF Categories View In Office");
                       string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                       var location = directoryName + "\\PDFCategoriesViewInOffice.png";
                       loginHelper.AddAttachment(location, id);
                       loginHelper.AddComment(loginHelper.getIssueID("PDF Categories View In Office"), "This issue is still occurring");
                   }
               }
               JIRA = loginHelper.getIssueID("PDF Categories View In Office");
               //  executionLog.DeleteFile("Error");
               throw;

           }
           finally
           {
               executionLog.DeleteFile("PDFCategoriesViewInOffice");
               executionLog.WriteInExcel("PDF Categories View In Office", Status, JIRA, "Corp PDF Templates");
           }
        }
    }
} 