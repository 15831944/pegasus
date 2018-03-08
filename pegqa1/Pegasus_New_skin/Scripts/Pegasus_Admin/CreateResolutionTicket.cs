using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Admin
{
    [TestClass]
    public class CreateResolutionTicket : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createResolutionTicket()
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
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateResolutionTicket", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateResolutionTicket", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateResolutionTicket", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreateResolutionTicket", "Redirect To Ticket");
                VisitOffice("tickets/masterdata/resolution");
                tickets_MasterDataHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateResolutionTicket", "Verfiy title");
                VerifyTitle("Master Data");

                var loc = "//a[text()='Closed']";
                var text = "//a[text()='Resolved']";
                var text1 = "//a[text()='Reopened']";
                var text2 = "//a[text()='Fixed']";
                for (int i = 1; i <= 5; i++)
                {

                    if (tickets_MasterDataHelper.IsElementNotPresent(loc))
                    {
                        executionLog.Log("CreateResolutionTicket", " Click On Create");
                        tickets_MasterDataHelper.ClickElement("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Verify title");
                        VerifyTitle("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Enter Name");
                        tickets_MasterDataHelper.TypeText("Name", "Closed");

                        executionLog.Log("CreateResolutionTicket", "cLICK on Save  ");
                        tickets_MasterDataHelper.ClickElement("Save");

                        executionLog.Log("CreateResolutionTicket", "Wait for Confirmation");
                        tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);
                    }
                    else if (tickets_MasterDataHelper.IsElementNotPresent(text))
                    {
                        executionLog.Log("CreateResolutionTicket", " Click On Create");
                        tickets_MasterDataHelper.ClickElement("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Verify title");
                        VerifyTitle("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Enter Name");
                        tickets_MasterDataHelper.TypeText("Name", "Resolved");

                        executionLog.Log("CreateResolutionTicket", "cLICK on Save  ");
                        tickets_MasterDataHelper.ClickElement("Save");

                        executionLog.Log("CreateResolutionTicket", "Wait for Confirmation");
                        tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);
                    }
                    else if (tickets_MasterDataHelper.IsElementNotPresent(text1))
                    {
                        executionLog.Log("CreateResolutionTicket", " Click On Create");
                        tickets_MasterDataHelper.ClickElement("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Verify title");
                        VerifyTitle("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Enter Name");
                        tickets_MasterDataHelper.TypeText("Name", "Reopened");

                        executionLog.Log("CreateResolutionTicket", "cLICK on Save  ");
                        tickets_MasterDataHelper.ClickElement("Save");

                        executionLog.Log("CreateResolutionTicket", "Wait for Confirmation");
                        tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);
                    }
                    else if (tickets_MasterDataHelper.IsElementNotPresent(text2))
                    {
                        executionLog.Log("CreateResolutionTicket", " Click On Create");
                        tickets_MasterDataHelper.ClickElement("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Verify title");
                        VerifyTitle("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Enter Name");
                        tickets_MasterDataHelper.TypeText("Name", "Fixed");

                        executionLog.Log("CreateResolutionTicket", "cLICK on Save  ");
                        tickets_MasterDataHelper.ClickElement("Save");

                        executionLog.Log("CreateResolutionTicket", "Wait for Confirmation");
                        tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);
                    }
                    else
                    {
                        executionLog.Log("CreateResolutionTicket", " Click On Create");
                        tickets_MasterDataHelper.ClickElement("Create");
                        tickets_MasterDataHelper.WaitForWorkAround(5000);

                        executionLog.Log("CreateResolutionTicket", "Verify title");
                        VerifyTitle("Create");

                        executionLog.Log("CreateResolutionTicket", "Enter Name");
                        tickets_MasterDataHelper.TypeText("Name", name);

                        executionLog.Log("CreateResolutionTicket", "cLICK on Save  ");
                        tickets_MasterDataHelper.ClickElement("Save");

                        executionLog.Log("CreateResolutionTicket", "Wait for Confirmation");
                        tickets_MasterDataHelper.WaitForText("Masterdata created successfully", 30);

                        executionLog.Log("CreateResolutionTicket", "Click on delete item.");
                        tickets_MasterDataHelper.ClickElement("DeleteItem");
                        tickets_MasterDataHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreateResolutionTicket", "Click on resolution to be deleted");
                        tickets_MasterDataHelper.DeleteCategory(name);
                        tickets_MasterDataHelper.WaitForWorkAround(1000);

                        executionLog.Log("CreateResolutionTicket", "Select value to be replace with");
                        tickets_MasterDataHelper.SelectByText("ReplaceWith", "Closed");

                        executionLog.Log("CreateResolutionTicket", "Confirm delete by clicking save.");
                        tickets_MasterDataHelper.ClickElement("SaveDelete");
                        tickets_MasterDataHelper.AcceptAlert();
                        tickets_MasterDataHelper.WaitForWorkAround(4000);
                        tickets_MasterDataHelper.VerifyPageText("Resolution deleted successfully.");
                        break;
                    }
                }
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateResolutionTicket");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Resolution Ticket");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Resolution Ticket", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Resolution Ticket");
                        TakeScreenshot("CreateResolutionTicket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateResolutionTicket.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateResolutionTicket");
                        string id = loginHelper.getIssueID("Create Resolution Ticket");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateResolutionTicket.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Resolution Ticket"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Resolution Ticket");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateResolutionTicket");
                executionLog.WriteInExcel("Create Resolution Ticket", Status, JIRA, "Ticketing System");
            }
        }
    }
}
