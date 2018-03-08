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
    public class PDFEmailFromClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void pDFEmailFromClient()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFEmailFromClient", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFEmailFromClient", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
             
                executionLog.Log("PDFEmailFromClient", "Redirect To Client");
                VisitOffice("clients");
               
                executionLog.Log("PDFEmailFromClient", "Click on client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFEmailFromClient", "Click On PDF Tabs");
                office_ClientsHelper.ClickElement("PDFTab");

                executionLog.Log("PDFEmailFromClient", "Wait for element to present.");
                office_ClientsHelper.WaitForElementPresent("ClickeMAILtOsEND", 10);

                executionLog.Log("PDFEmailFromClient", "Click on Share Link");
                office_ClientsHelper.ClickElement("ClickeMAILtOsEND");

                executionLog.Log("PDFEmailFromClient", "Wait for element to present.");
                office_ClientsHelper.WaitForElementPresent("EmterEmailAddressPDF", 10);

                executionLog.Log("PDFEmailFromClient", "Enter sender email address.");
                office_ClientsHelper.TypeText("EmterEmailAddressPDF", "test@yopmail.com");

                executionLog.Log("PDFEmailFromClient", "Enter Reciepent Name");
                office_ClientsHelper.TypeText("RecipinetName", "Test Recipinet");

                executionLog.Log("PDFEmailFromClient", "Click on Share Agreement ");
                office_ClientsHelper.ClickViaJavaScript("//button[@title='Confirm Options and Continue']");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFEmailFromClient", "Click on Accept To Share");
                office_ClientsHelper.ClickElement("ClickOnSendPDFEmail");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFEmailFromClient", "Verify the message");
                office_ClientsHelper.WaitForText("E-Mail Sent Successfully." ,10);
               
            }    
           catch (Exception e)
            {
                 executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFEmailFromClient");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Email From Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Email From Client", "Bug", "Medium", "PDF Template page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Email From Client");
                        TakeScreenshot("PDFEmailFromClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFEmailFromClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFEmailFromClient");
                        string id = loginHelper.getIssueID("PDF Email From Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFEmailFromClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Email From Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Email From Client");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFEmailFromClient");
                executionLog.WriteInExcel("PDF Email From Client", Status, JIRA, "Client Management");
            }
        }
    }
} 