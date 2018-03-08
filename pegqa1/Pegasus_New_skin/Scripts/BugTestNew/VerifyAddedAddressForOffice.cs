using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyAddedAddressForOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Delete")]
        public void verifyAddedAddressForOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            String JIRA = "";
            String Status = "Pass";

            //        try
            //      {

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Login with valid credential  Username");
            Login("newthemecorp", "pegasus");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Redirect at offices page.");
            VisitCorp("offices");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Enter Invalid phone Number");
            corpOffice_OfficeHelper.TypeText("EnterNameToSearch", "Aslam Office");
            corpOffice_OfficeHelper.WaitForWorkAround(4000);

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Wait for locator to be present.");
            corpOffice_OfficeHelper.ClickElement("EditOffice");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("ClickAddAddressBtn");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.TypeText("ZipCode2", "test");
            corpOffice_OfficeHelper.WaitForWorkAround(3000);

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.TypeText("AddressLine2", "test");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("EditLink");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.VerifyText("AddressLine1", "test");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("RemoveAddress");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");
        }
    }
}
/*          catch (Exception e)
          {
              executionLog.Log("Error", e.StackTrace);
              Status = "Fail";

              String counter = executionLog.readLastLine("counter");
              String Description = executionLog.GetAllTextFile("CorpOfficeSearchWithInvalidPhone");
              String Error = executionLog.GetAllTextFile("Error");
              if (counter == "")
              {
                  counter = "0";
              }
              bool result = loginHelper.CheckExstingIssue("Corp Office Search With Invalid Phone");
              if (!result)
              {
                  if (Int16.Parse(counter) < 5)
                  {
                      executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                      loginHelper.CreateIssue("Corp Office Search With Invalid Phone", "Bug", "Medium", "Corp Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                      string id = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
                      TakeScreenshot("CorpOfficeSearchWithInvalidPhone");
                      string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                      var location = directoryName + "\\CorpOfficeSearchWithInvalidPhone.png";
                      loginHelper.AddAttachment(location, id);
                  }
              }
              else
              {
                  if (Int16.Parse(counter) < 5)
                  {
                      executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                      TakeScreenshot("CorpOfficeSearchWithInvalidPhone");
                      string id = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
                      string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                      var location = directoryName + "\\CorpOfficeSearchWithInvalidPhone.png";
                      loginHelper.AddAttachment(location, id);
                      loginHelper.AddComment(loginHelper.getIssueID("Corp Office Search With Invalid Phone"), "This issue is still occurring");
                  }
              }
              JIRA = loginHelper.getIssueID("Corp Office Search With Invalid Phone");
              executionLog.DeleteFile("Error");
              throw;

          }
          finally
          {
              executionLog.DeleteFile("CorpOfficeSearchWithInvalidPhone");
              executionLog.WriteInExcel("Corp Office Search With Invalid Phone", Status, JIRA, "Corp Profile.");
          }
      }
  }
}
*/
