using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SystemPickListeAddressLabelSocialMedia : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void systemPickListeAddressLabelSocialMedia()
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
                executionLog.Log("SystemPickListAddressType", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SystemPickListAddressType", "Redirect To PickList");
                VisitOffice("pick-lists");

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Picklists");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click On Link Address Type");
                system_PicklistsHelper.ClickElement("eAddressLabelForSocialMedia");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click On Add New Item");
                system_PicklistsHelper.ClickElement("AddNewItem");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Add New Item");
                system_PicklistsHelper.TypeText("PicklistItem", AddressType);

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click on Save ");
                system_PicklistsHelper.ClickElement("Save");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click on Residual Income tab");
                system_PicklistsHelper.ClickElement("Cancel");
                system_PicklistsHelper.WaitForWorkAround(4000);

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Verfiy Text");
                system_PicklistsHelper.VerifyPageText(AddressType);

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click delete Button");
                system_PicklistsHelper.ClickElement("DeletePick");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click on item to deleted");
                system_PicklistsHelper.DeletePickList(AddressType);

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Selelct replace with item.");
                system_PicklistsHelper.SelectText("ReplacePiclist", "Facebook");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Click PickList Save Button");
                system_PicklistsHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("SystemPickListeAddressLabelSocialMedia", "Accept alert message.");
                system_PicklistsHelper.AcceptAlert();
                system_PicklistsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SystemPickListeAddressLabelSocialMedia");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("System Pick Liste Address Label Social Media");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("System Pick Liste Address Label Social Media", "Bug", "Medium", "System Pick page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("System Pick Liste Address Label Social Media");
                        TakeScreenshot("SystemPickListeAddressLabelSocialMedia");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressLabelSocialMedia.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SystemPickListeAddressLabelSocialMedia");
                        string id = loginHelper.getIssueID("System Pick Liste Address Label Social Media");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressLabelSocialMedia.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("System Pick Liste Address Label Social Media"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("System Pick Liste Address Label Social Media");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SystemPickListeAddressLabelSocialMedia");
                executionLog.WriteInExcel("System Pick Liste Address Label Social Media", Status, JIRA, "Pick List Management");
            }
        }
    }
}

