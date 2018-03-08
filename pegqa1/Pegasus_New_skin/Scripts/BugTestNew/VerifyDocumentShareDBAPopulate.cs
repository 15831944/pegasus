using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyDocumentShareDBAPopulate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyDocumentShareDBAPopulate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            
            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_clientsHelper = new Office_ClientsHelper(GetWebDriver());
            var yopmail_Helper = new YopMailHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(1, 9999);
            var OwnerFirst = "Owner" + RandomNumber(1, 9999);
            var OwnerLast = "Test" + RandomNumber(1, 9999);
            var DBAVerify = "Merchant Portal - " + DBA + " You have documents waiting for review";

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                VisitOffice("clients/create");
                office_clientsHelper.WaitForWorkAround(2000);
                Console.WriteLine("Redirected to Create Merchant page");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Bussiness DBA name");
                office_clientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter First Name");
                office_clientsHelper.TypeText("FirstName", "Test");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Last Name");
                office_clientsHelper.TypeText("LastName", "Client");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select Status");
                office_clientsHelper.SelectByText("Status", "New");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select Responsibility");
                office_clientsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Save button");
                office_clientsHelper.ClickElement("Save");
                office_clientsHelper.WaitForWorkAround(4000);
                Console.WriteLine("Merchant Created");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Go to Owners tab");
                office_clientsHelper.ClickElement("OwnerTab");
                office_clientsHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to Owners tab");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Owner First Name");
                office_clientsHelper.TypeText("OwnerFirstName", OwnerFirst);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Owner Last Name");
                office_clientsHelper.TypeText("OwnerLastName", OwnerLast);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Owner Title");
                office_clientsHelper.TypeText("TitleOwner", "NewTitle");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select eAddress Type");
                office_clientsHelper.SelectByText("OwnereAddressType", "E-Mail");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter eAddress");
                office_clientsHelper.TypeText("OwnereAddressEnter", "owner.pegasus@yopmail.com");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Save button");
                office_clientsHelper.ClickElement("OwnerSave");
                Console.WriteLine("Owner Information saved");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Go to PDFs tab");
                office_clientsHelper.ClickElement("PDFtab");
                office_clientsHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to PDFs tab");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on [Share] link");
                office_clientsHelper.ClickElement("ShareLink");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Default Password radio button");
                office_clientsHelper.ClickElement("DefaultPwdRadioBtn");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Create button");
                office_clientsHelper.ClickElement("CreateBtn");
                office_clientsHelper.WaitForWorkAround(3000);

                GetWebDriver().SwitchTo().Alert().Accept();
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Close Create User Popup");
                office_clientsHelper.ClickElement("CrossIcon");
                Console.WriteLine("Client User Created");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Go to Info tab");
                office_clientsHelper.ClickElement("InfoTab");
                office_clientsHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to Info tab");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Create/Manage button");
                office_clientsHelper.ClickForce("ManageClientUser");
         
                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Manage option");
                office_clientsHelper.ClickElement("ManageOption");
                office_clientsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select User");
                office_clientsHelper.ClickElement("ClientChkBox");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Activate Selected User button");
                office_clientsHelper.ClickElement("ActivateBtn");
                office_clientsHelper.WaitForWorkAround(4000);

                GetWebDriver().SwitchTo().Alert().Accept();
                office_clientsHelper.WaitForWorkAround(4000);
                Console.WriteLine("Client User Activated");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Go to PDFs tab");
                office_clientsHelper.ClickElement("PDFtab");
                office_clientsHelper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to PDFs tab");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on [Share] link");
                office_clientsHelper.ClickElement("ShareLink");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select Owners Signature Required check box");
                office_clientsHelper.ClickElement("OwnerSignReq");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Select Documents Required check box");
                office_clientsHelper.ClickElement("DocReq");
                office_clientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Share button");
                office_clientsHelper.ClickElement("ShareBtn");
                office_clientsHelper.WaitForWorkAround(3000);

                GetWebDriver().SwitchTo().Alert().Accept();
                Console.WriteLine("File Shared successfully");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Go to yopmail.com");
                GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com/en/");
                yopmail_Helper.WaitForWorkAround(3000);
                Console.WriteLine("Redirected to yopmail.com");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Enter Email");
                yopmail_Helper.TypeText("Yopmail", "owner.pegasus");
                yopmail_Helper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Click on Check Inbox button");
                yopmail_Helper.ClickElement("YopmailClick");
                Console.WriteLine("Entered into inbox");

                executionLog.Log("VerifyDocumentShareDBAPopulate", "Verify DBA Name present in Subject of email");
                yopmail_Helper.switchFrame("ifmail");
                yopmail_Helper.VerifyText("SubjectFirstMail", DBA);
                Console.WriteLine("DBA Name is populated in Subject if email");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDocumentShareDBAPopulate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Document Share DBA Populate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Document Share DBA Populate", "Bug", "Medium", "Office Merchant", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Admin Ticket Master Delete Confirmation");
                        TakeScreenshot("VerifyDocumentShareDBAPopulate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentShareDBAPopulate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDocumentShareDBAPopulate");
                        string id = loginHelper.getIssueID("Verify Document Share DBA Populate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentShareDBAPopulate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Document Share DBA Populate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Document Share DBA Populate");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDocumentShareDBAPopulate");
                executionLog.WriteInExcel("Verify Document Share DBA Populate", Status, JIRA, "Office Merchant");
            }
           
        }
    }
}
