using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SystemPickListSalutation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void systemPickListSalutation()
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
            var system_PicklistsHelper = new System_PicklistsHelper(GetWebDriver());

            // Variable
            var AddressType = "Test" + RandomNumber(100, 500);
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("SystemPickListAddressType", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SystemPickListAddressType", "Redirect To PickList");
                VisitOffice("pick-lists");
                system_PicklistsHelper.WaitForWorkAround(5000);

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Picklists");

                executionLog.Log("SystemPickListAddressType", "Enter Salutation into field");
                system_PicklistsHelper.TypeText("SearchName", "Salutation");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListAddressType", "Enter corp into field");
                system_PicklistsHelper.TypeText("SearchModule", "Corporates");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListSalutation", "Click On Link Address Type");
                system_PicklistsHelper.ClickElement("Salutation");

                executionLog.Log("SystemPickListSalutation", "Click On Add New Item");
                system_PicklistsHelper.ClickElement("AddNewItem");

                executionLog.Log("SystemPickListSalutation", "Add New Item");
                system_PicklistsHelper.TypeText("PicklistItem", AddressType);

                executionLog.Log("SystemPickListSalutation", "Click on Save ");
                system_PicklistsHelper.ClickElement("Save");
                system_PicklistsHelper.WaitForWorkAround(2000);

                executionLog.Log("SystemPickListSalutation", "Click On cancel");
                system_PicklistsHelper.ClickJs("Cancel");
                system_PicklistsHelper.WaitForWorkAround(8000);

                executionLog.Log("SystemPickListSalutation", "Verfiy Text");
                system_PicklistsHelper.VerifyPageText(AddressType);

                executionLog.Log("SystemPickListSalutation", "Click delete Button");
                system_PicklistsHelper.ClickJs("DeletePick");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListSalutation", "Click on item to deleted");
                system_PicklistsHelper.DeletePickList(AddressType);

                executionLog.Log("SystemPickListSalutation", "Selelct replace with item.");
                system_PicklistsHelper.SelectText("ReplacePiclist", "Mr");

                executionLog.Log("SystemPickListSalutation", "Click PickList Save Button");
                system_PicklistsHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("SystemPickListSalutation", "Accept alert message.");
                system_PicklistsHelper.AcceptAlert();
                system_PicklistsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SystemPickListSalutation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("System Pick List Salutation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("System Pick List Salutation", "Bug", "Medium", "System Pick page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("System Pick List Salutation");
                        TakeScreenshot("SystemPickListSalutation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListSalutation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SystemPickListSalutation");
                        string id = loginHelper.getIssueID("System Pick List Salutation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListSalutation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("System Pick List Salutation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("System Pick List Salutation");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SystemPickListSalutation");
                executionLog.WriteInExcel("System Pick List Salutation", Status, JIRA, "Pick List Management");
            }
        }
    }
}
