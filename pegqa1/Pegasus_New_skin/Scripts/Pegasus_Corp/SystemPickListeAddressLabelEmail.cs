using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SystemPickListeAddressLabelEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void systemPickListeAddressLabelEmail()
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
            var AddressType = "Test" + RandomNumber(1, 99);
            var Test = "New" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("SystemPickListeAddressLabelEmail", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SystemPickListeAddressLabelEmail", "Redirect To PickList");
                VisitOffice("pick-lists");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Verify Page title");
                VerifyTitle("Picklists");

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click On Link eAddress label");
                system_PicklistsHelper.ClickElement("eAddressLabelEmail");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click On Add New Item");
                system_PicklistsHelper.ClickElement("AddNewItem");
                system_PicklistsHelper.WaitForWorkAround(1000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Add New Item");
                system_PicklistsHelper.TypeText("PicklistItem", AddressType);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click on Save ");
                system_PicklistsHelper.ClickElement("Save");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click on cancel ");
                system_PicklistsHelper.ClickElement("Cancel");
                system_PicklistsHelper.WaitForWorkAround(2000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Verfiy Text");
                system_PicklistsHelper.VerifyPageText(AddressType);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click delete Button");
                system_PicklistsHelper.ClickElement("DeletePick");
                system_PicklistsHelper.WaitForWorkAround(1000);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click on item to deleted");
                system_PicklistsHelper.DeletePickList(AddressType);

                executionLog.Log("SystemPickListeAddressLabelEmail", "Selelct replace with item.");
                system_PicklistsHelper.SelectText("ReplacePiclist", "Work");

                executionLog.Log("SystemPickListeAddressLabelEmail", "Click PickList Save Button");
                system_PicklistsHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("SystemPickListeAddressLabelEmail", "Accept alert message.");
                system_PicklistsHelper.AcceptAlert();
                system_PicklistsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SystemPickListeAddressLabelEmail");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("System Pick Liste Address Label Email");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("System Pick Liste Address Label Email", "Bug", "Medium", "System pickpage", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("System Pick Liste Address Label Email");
                        TakeScreenshot("SystemPickListeAddressLabelEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressLabelEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SystemPickListeAddressLabelEmail");
                        string id = loginHelper.getIssueID("System Pick Liste Address Label Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressLabelEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("System Pick Liste Address Label Email"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("System Pick Liste Address Label Email");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SystemPickListeAddressLabelEmail");
                executionLog.WriteInExcel("System Pick Liste Address Label Email", Status, JIRA, "Pick List Management");
            }
        }
    }
}