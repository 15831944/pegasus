using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreatePickList : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createPickList()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var system_PicklistsHelper = new System_PicklistsHelper(GetWebDriver());

            // Variables
            var Picklist = "Pick" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreatePickList", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreatePickList", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePickList", "Go to picklist page");
                VisitOffice("pick-lists");

                executionLog.Log("CreatePickList", "Verify title");
                VerifyTitle("Picklists");

                executionLog.Log("CreatePickList", " Open the first picklist");
                system_PicklistsHelper.ClickElement("PickList1");

                for (int i = 1; i < 20; i++)
                {
                    var loc = "//table[@class='table table-bordered']//tr[" + i + "]//td[1]/span";

                    if (loc.Contains("Personal"))
                    {
                        executionLog.Log("CreatePickList", "Click on Add button");
                        system_PicklistsHelper.ClickElement("AddPick");
                        system_PicklistsHelper.WaitForWorkAround(2000);

                        executionLog.Log("CreatePickList", "Enter name");
                        system_PicklistsHelper.TypeText("PickType", Picklist);

                        executionLog.Log("CreatePickList", "Click on Save button");
                        system_PicklistsHelper.ClickElement("SavePicklist");

                        executionLog.Log("CreatePickList", "Click on Cancel");
                        system_PicklistsHelper.ClickElement("Cancel");
                        system_PicklistsHelper.WaitForWorkAround(4000);

                        executionLog.Log("CreatePickList", "Verfiy Text");
                        system_PicklistsHelper.VerifyPageText(Picklist);

                        executionLog.Log("CreatePickList", "Click delete Button");
                        system_PicklistsHelper.ClickElement("DeletePick");
                        system_PicklistsHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreatePickList", "Click on item to deleted");
                        system_PicklistsHelper.DeletePickList(Picklist);
                        system_PicklistsHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreatePickList", "Selelct replace with item.");
                        system_PicklistsHelper.SelectText("ReplacePiclist", "Personal");

                        executionLog.Log("CreatePickList", "Click PickList Save Button");
                        system_PicklistsHelper.ClickElement("PickListSaveBtn");

                        executionLog.Log("CreatePickList", "Accept alert message.");
                        system_PicklistsHelper.AcceptAlert();
                        system_PicklistsHelper.WaitForWorkAround(4000);
                        break;
                    }
                    else
                    {
                        executionLog.Log("CreatePickList", " Open the first picklist");
                        //  system_PicklistsHelper.ClickElement("PickList1");

                        executionLog.Log("CreatePickList", "Click on Add button");
                        system_PicklistsHelper.ClickElement("AddPick");
                        system_PicklistsHelper.WaitForWorkAround(2000);

                        executionLog.Log("CreatePickList", "Enter name");
                        system_PicklistsHelper.TypeText("PickType", "Personal");

                        executionLog.Log("CreatePickList", "Click on Save button");
                        system_PicklistsHelper.ClickElement("SavePicklist");
                        system_PicklistsHelper.WaitForWorkAround(5000);

                        executionLog.Log("CreatePickList", "Click on Cancel button");
                        system_PicklistsHelper.ClickJs("Cancel");

                        executionLog.Log("CreatePickList", "Click on Add button");
                        system_PicklistsHelper.ClickElement("AddPick");
                        system_PicklistsHelper.WaitForWorkAround(2000);

                        executionLog.Log("CreatePickList", "Enter name");
                        system_PicklistsHelper.TypeText("PickType", Picklist);

                        executionLog.Log("CreatePickList", "Click on Save button");
                        system_PicklistsHelper.ClickElement("SavePicklist");

                        executionLog.Log("CreatePickList", "Click on Cancel");
                        system_PicklistsHelper.ClickElement("Cancel");
                        system_PicklistsHelper.WaitForWorkAround(4000);

                        executionLog.Log("CreatePickList", "Click delete Button");
                        system_PicklistsHelper.ClickElement("DeletePick");
                        system_PicklistsHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreatePickList", "Click on item to deleted");
                        system_PicklistsHelper.DeletePickList(Picklist);
                        system_PicklistsHelper.WaitForWorkAround(3000);

                        executionLog.Log("CreatePickList", "Selelct replace with item.");
                        system_PicklistsHelper.SelectText("ReplacePiclist", "Personal");

                        executionLog.Log("CreatePickList", "Click PickList Save Button");
                        system_PicklistsHelper.ClickElement("PickListSaveBtn");

                        executionLog.Log("CreatePickList", "Accept alert message.");
                        system_PicklistsHelper.AcceptAlert();
                        system_PicklistsHelper.WaitForWorkAround(4000);
                        break;
                    }

                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePickList");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Pick List");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Pick List", "Bug", "Medium", "Pick List page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Pick List");
                        TakeScreenshot("CreatePickList");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePickList.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePickList");
                        string id = loginHelper.getIssueID("Create Pick List");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePickList.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Pick List"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Pick List");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePickList");
                executionLog.WriteInExcel("Create Pick List", Status, JIRA, "System picklist");
            }
        }
    }
}