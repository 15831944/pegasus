using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OfficeFieldDictionary : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void officeFieldDictionary()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects5
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldDictionary_SectionsHelper = new Office_FieldDictionary_SectionsHelper(GetWebDriver());
            var office_FieldDictionary_TabsHelper = new Office_FieldDictionary_TabsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var TabNameFDic = "Test" + RandomNumber(99, 99999);
            var FDNAME = "Test" + RandomNumber(99, 99999) + "te";
            var idsc = "1" + RandomNumber(1, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OfficeFieldDictionary", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("OfficeFieldDictionary", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OfficeFieldDictionary", "Goto Office Tab");
                VisitOffice("tabs");

                executionLog.Log("OfficeFieldDictionary", "Click Add New Tab");
                office_FieldDictionary_TabsHelper.ClickElement("Create");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeFieldDictionary", "Enter Tab Name");
                office_FieldDictionary_TabsHelper.TypeText("Name", TabNameFDic);

                executionLog.Log("OfficeFieldDictionary", "Click on Save");
                office_FieldDictionary_TabsHelper.ClickOnDisplayed("Save");

                executionLog.Log("OfficeFieldDictionary", "Confirmation");
                office_FieldDictionary_TabsHelper.WaitForText("Tab Created Successfully", 10);

                executionLog.Log("OfficeFieldDictionary", "Goto Sections");
                VisitOffice("sections");

                executionLog.Log("OfficeFieldDictionary", "Click on Create Button");
                office_FieldDictionary_SectionsHelper.ClickElement("Create");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(4000);

                executionLog.Log("OfficeFieldDictionary", "Select Tab Name");
                office_FieldDictionary_SectionsHelper.SelectByText("TabName", TabNameFDic);

                executionLog.Log("OfficeFieldDictionary", "Enter Section Name");
                office_FieldDictionary_SectionsHelper.TypeText("Name", FDNAME);

                executionLog.Log("OfficeFieldDictionary", "Click Save");
                office_FieldDictionary_SectionsHelper.ClickOnDisplayed("Save");

                executionLog.Log("OfficeFieldDictionary", "Verify Alert text.");
                office_FieldDictionary_SectionsHelper.VerifyAlertText("Section Created Successfully");
                office_FieldDictionary_TabsHelper.AcceptAlert();
                office_FieldDictionary_TabsHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeFieldDictionary", "Goto Tabs");
                VisitOffice("tabs");

                executionLog.Log("OfficeFieldDictionary", "Click Edit");
                office_FieldDictionary_TabsHelper.ClickElement("ClickOnEditTabIcon");
                office_FieldDictionary_TabsHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeFieldDictionary", "Click Save Button");
                office_FieldDictionary_TabsHelper.Click("//*[@id='dialog_tabedit']//div[3]/button[1]");

                executionLog.Log("OfficeFieldDictionary", "Wait for Confirmation");
                office_FieldDictionary_TabsHelper.WaitForText("Tab Updated Successfully", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeFieldDictionary");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("OfficeFieldDictionary");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("OfficeFieldDictionary", "Bug", "Medium", "Field Dictionary page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("OfficeFieldDictionary");
                        TakeScreenshot("OfficeFieldDictionary");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeFieldDictionary");
                        string id = loginHelper.getIssueID("OfficeFieldDictionary");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeFieldDictionary.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("OfficeFieldDictionary"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("OfficeFieldDictionary");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeFieldDictionary");
                executionLog.WriteInExcel("OfficeFieldDictionary", Status, JIRA, "Office Field Dictionary");
            }
        }
    }
}
